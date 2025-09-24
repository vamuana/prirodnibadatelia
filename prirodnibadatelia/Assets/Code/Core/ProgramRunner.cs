using System.Collections;
using UnityEngine;

public class ProgramRunner : MonoBehaviour
{
    public Interpreter interpreter;

    [ContextMenu("Run Program")]
    public void RunOnce() => StartCoroutine(RunCo());

    IEnumerator RunCo()
    {
        yield return interpreter.Run();
        var anim = interpreter.agent.GetComponent<Animator>();
        if (anim) anim.Play("Celebrate", 0, 0);
    }
}
