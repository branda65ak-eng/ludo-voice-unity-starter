using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// GameManager: manages turns, simple rules, integrates with Photon for basic sync
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    public BoardManager board;
    public Dice dice;
    public List<Piece> playerPieces; // one piece per player for starter (expandable)
    public int localPlayerIndex = 0; // assign per player
    private int currentTurn = 0;
    private bool isRolling = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Simple init: place pieces at start
        for (int i = 0; i < playerPieces.Count; i++)
        {
            playerPieces[i].Init(board, i * 2); // example: different start tiles
        }
    }

    // Called by UI or input to roll dice
    public void OnRollButtonPressed()
    {
        if (isRolling) return;
        // enforce turn locally: only current player can roll
        if (!IsMyTurn()) return;
        StartCoroutine(DoRoll());
    }

    bool IsMyTurn()
    {
        // localPlayerIndex must match currentTurn modulo players count
        return (currentTurn % playerPieces.Count) == localPlayerIndex;
    }

    IEnumerator DoRoll()
    {
        isRolling = true;
        if (PhotonNetwork.IsConnected)
        {
            // If using Photon, let master client generate authoritative roll and RPC to others
            if (PhotonNetwork.IsMasterClient)
            {
                int val = dice.Roll();
                photonView.RPC(nameof(RPC_OnDiceRolled), RpcTarget.AllBuffered, val, currentTurn);
            }
            else
            {
                // request master to roll (could be RPC); for simplicity call local roll and send request in a real app
                PhotonNetwork.RaiseEvent(1, "requestRoll", RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
            }
        }
        else
        {
            // Local only
            yield return StartCoroutine(dice.RollWithDelay(val =>
            {
                StartCoroutine(ApplyRoll(val));
            }));
        }
        isRolling = false;
    }

    // Called on all clients by RPC (master authoritative model)
    [PunRPC]
    void RPC_OnDiceRolled(int val, int turn, PhotonMessageInfo info)
    {
        // ensure consistent turn ordering
        StartCoroutine(ApplyRoll(val));
    }

    IEnumerator ApplyRoll(int val)
    {
        // Move current player's piece by val steps
        int playerIdx = currentTurn % playerPieces.Count;
        Piece p = playerPieces[playerIdx];
        yield return StartCoroutine(p.MoveSteps(val));
        // next turn
        currentTurn++;
        yield return null;
    }
}
