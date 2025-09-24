using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class Int2 { public int x, y; }
[Serializable] public class SimpleLevel
{
    public int cols = 5, rows = 3;
    public Vector2Int start = new(0, 1);
    public Facing facing = Facing.E;
    public Vector2Int goal = new(4, 1);

    public List<Int2> water = new();   // NEW
    public List<Int2> bridge = new();  // NEW
    public List<Int2> solid = new();   // NEW
}

public class LevelLoader : MonoBehaviour
{
    public string levelResourcePath = "Levels/FOX_01_MEADOW";
    public GridAgent agent;
    public WorldGrid world;                // NEW
    public Transform goalFlagPrefab;

    void Start()
    {
        var ta = Resources.Load<TextAsset>(levelResourcePath);
        if (!ta) { Debug.LogError($"Missing level JSON at Resources/{levelResourcePath}.json"); return; }
        var lvl = JsonUtility.FromJson<SimpleLevel>(ta.text);

        // Populate world
        if (world == null) world = FindObjectOfType<WorldGrid>();
        if (!world) world = new GameObject("World").AddComponent<WorldGrid>();

        world.water.Clear(); world.bridge.Clear(); world.solid.Clear();
        foreach (var c in lvl.water)  world.water.Add(new Vector2Int(c.x, c.y));
        foreach (var c in lvl.bridge) world.bridge.Add(new Vector2Int(c.x, c.y));
        foreach (var c in lvl.solid)  world.solid.Add(new Vector2Int(c.x, c.y));

        // Setup agent
        agent.world = world;
        agent.GridPos = lvl.start;
        agent.Facing = lvl.facing;
        agent.SnapToGrid();

        if (goalFlagPrefab != null)
        {
            var flag = Instantiate(goalFlagPrefab);
            flag.position = agent.GridToWorld(lvl.goal) + Vector3.up * 0.01f;
        }
    }
}
