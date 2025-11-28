using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] FoxProgramRuntime runtime;

    public void OnRun()   => runtime.RunProgram();
    public void OnStop()  => runtime.StopProgram();
    public void OnReset() => runtime.ResetToSpawn();
}
