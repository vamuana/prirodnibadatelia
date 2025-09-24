using System.Collections;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    public GridAgent agent;
    public ProgramData program;

    public IEnumerator Run()
    {
        if (!agent || program == null) yield break;

        foreach (var b in program.blocks)
        {
            switch (b.op)
            {
                case Ops.FORWARD: yield return agent.StepForward(); break;
                case Ops.LEFT:    agent.TurnLeft();  yield return null; break;
                case Ops.RIGHT:   agent.TurnRight(); yield return null; break;
                case Ops.WAIT:    yield return new WaitForSeconds(Mathf.Max(0, b.arg) / 1000f); break;
                case Ops.TAKE:    /* inventory later */ yield return null; break;
                default:          Debug.LogWarning($"Unknown op {b.op}"); break;
            }
        }
    }
}
