using UnityEngine;

public class ProgramBootstrap : MonoBehaviour
{
    public Interpreter interpreter;

    void Reset()
    {
        interpreter.program = new ProgramData {
            character = "RABBIT",
            blocks = {
                new Block{ op = Ops.FORWARD },
                new Block{ op = Ops.FORWARD },
                new Block{ op = Ops.FORWARD },
                new Block{ op = Ops.FORWARD }
            }
        };
    }
}
