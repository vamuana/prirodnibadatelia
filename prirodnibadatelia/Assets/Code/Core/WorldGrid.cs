using System.Collections.Generic;
using UnityEngine;

public enum TileType { Ground, Water, Bridge, Solid }

public class WorldGrid : MonoBehaviour
{
    public HashSet<Vector2Int> water = new();
    public HashSet<Vector2Int> bridge = new();
    public HashSet<Vector2Int> solid  = new();

    public bool IsWater(Vector2Int p)  => water.Contains(p);
    public bool IsBridge(Vector2Int p) => bridge.Contains(p);
    public bool IsSolid(Vector2Int p)  => solid.Contains(p);

    public bool CanEnter(Vector2Int p)
    {
        if (IsSolid(p)) return false;
        if (IsWater(p) && !IsBridge(p)) return false; // water is blocked unless bridge exists here
        return true; // ground or bridge-over-water
    }
}
