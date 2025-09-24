using System.Collections.Generic;
using UnityEngine;

public class BlockListUI : MonoBehaviour
{
    public Interpreter interpreter;
    Vector2 _scroll;

    void Awake()
    {
        if (!interpreter) interpreter = FindObjectOfType<Interpreter>();
        if (interpreter.program == null) interpreter.program = new ProgramData { blocks = new List<Block>() };
    }

    void OnGUI()
    {
        const int w = 300;
        GUILayout.BeginArea(new Rect(10, 10, w, Screen.height - 20), GUI.skin.window);
        GUILayout.Label("Program Blocks");

        // Add buttons
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("+ FORWARD")) Add("FORWARD");
        if (GUILayout.Button("+ LEFT")) Add("LEFT");
        if (GUILayout.Button("+ RIGHT")) Add("RIGHT");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("+ WAIT 500")) Add("WAIT", 500);
        if (GUILayout.Button("+ TAKE")) Add("TAKE");
        GUILayout.EndHorizontal();

        // List
        _scroll = GUILayout.BeginScrollView(_scroll, GUILayout.Height(280));
        var blocks = interpreter.program.blocks;
        for (int i = 0; i < blocks.Count; i++)
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.Label($"{i+1}. {blocks[i].op} {(blocks[i].op=="WAIT" ? blocks[i].arg.ToString() : "")}");
            if (GUILayout.Button("▲", GUILayout.Width(28)) && i>0) (blocks[i-1], blocks[i]) = (blocks[i], blocks[i-1]);
            if (GUILayout.Button("▼", GUILayout.Width(28)) && i<blocks.Count-1) (blocks[i+1], blocks[i]) = (blocks[i], blocks[i+1]);
            if (GUILayout.Button("✕", GUILayout.Width(28))) { blocks.RemoveAt(i); i--; }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();

        GUILayout.Space(6);
        if (GUILayout.Button("Run Program")) GetComponent<ProgramRunner>()?.RunOnce();
        if (GUILayout.Button("Clear")) blocks.Clear();

        GUILayout.EndArea();
    }

    void Add(string op, int arg = 0)
    {
        interpreter.program.blocks.Add(new Block { op = op, arg = arg });
    }
}
