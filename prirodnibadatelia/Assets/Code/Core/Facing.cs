using UnityEngine;

public enum Facing { N, E, S, W }

public static class FacingUtil
{
    public static Vector2Int ToDir(this Facing f) =>
        f switch { Facing.N => new Vector2Int(0, 1), Facing.E => new Vector2Int(1, 0),
                   Facing.S => new Vector2Int(0, -1), _ => new Vector2Int(-1, 0) };
}
