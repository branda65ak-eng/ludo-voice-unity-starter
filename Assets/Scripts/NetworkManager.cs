using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// NetworkManager: simple Photon connect / create/join room
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;
    public string gameVersion = "1";
    public string roomPrefix = "LudoRoom_";

    void Awake() => Instance = this;

    void Start()
    {
        Connect();
    }

    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.AutomaticallySyncScene = false;
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master");
    }

    public void CreateOrJoinRoom(string roomName = null)
    {
        if (!PhotonNetwork.IsConnected) Connect();
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };
        string name = string.IsNullOrEmpty(roomName) ? roomPrefix + Random.Range(1000, 9999) : roomName;
        PhotonNetwork.JoinOrCreateRoom(name, options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        // instantiate player avatar / assign localPlayerIndex etc.
        // Example: use PhotonNetwork.LocalPlayer.ActorNumber to derive player index
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Join room failed: " + message);
    }
}
