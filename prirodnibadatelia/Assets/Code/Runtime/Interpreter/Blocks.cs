using System;
using System.Collections.Generic;

[Serializable] public class ProgramData { public string character = "RABBIT"; public List<Block> blocks = new(); }
[Serializable] public class Block { public string op; public int arg = 0; } // FORWARD, LEFT, RIGHT, WAIT(ms), TAKE

public static class Ops
{
    public const string FORWARD = "FORWARD";
    public const string LEFT    = "LEFT";
    public const string RIGHT   = "RIGHT";
    public const string WAIT    = "WAIT";   // arg in ms
    public const string TAKE    = "TAKE";   // placeholder for later
}
