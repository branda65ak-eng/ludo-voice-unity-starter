using System.Collections;
using UnityEngine;

// Piece: moves along board tiles (indices)
public class Piece : MonoBehaviour
{
    public int tileIndex = 0; // current tile index
    public BoardManager boardManager;
    public float moveSpeed = 4f;

    public void Init(BoardManager board, int startIndex = 0)
    {
        boardManager = board;
        tileIndex = startIndex;
        transform.position = boardManager.GetTilePosition(tileIndex);
    }

    public IEnumerator MoveSteps(int steps)
    {
        int target = tileIndex + steps;
        for (int i = tileIndex + 1; i <= target; i++)
        {
            int idx = i % boardManager.TileCount;
            Vector3 targetPos = boardManager.GetTilePosition(idx);
            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            tileIndex = idx;
            yield return new WaitForSeconds(0.08f); // small pause between steps
        }
    }
}
