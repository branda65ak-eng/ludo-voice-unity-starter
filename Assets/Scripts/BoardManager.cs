using UnityEngine;

// BoardManager: holds board tile positions (assign in inspector)
public class BoardManager : MonoBehaviour
{
    public Transform[] tiles; // set these in inspector in correct order

    public Vector3 GetTilePosition(int index)
    {
        if (tiles == null || tiles.Length == 0) return Vector3.zero;
        index = Mathf.Clamp(index, 0, tiles.Length - 1);
        return tiles[index].position;
    }

    public int TileCount => tiles != null ? tiles.Length : 0;
}
