using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoxProgramRuntime : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] FoxController controller;
    [SerializeField] FoxAnimatorBridge bridge;

    [Header("Program (vyplň v Inspectore alebo UI)")]
    public List<CommandType> Program = new();

    [Header("Poistky")]
    [SerializeField] int maxSteps = 200;

    Coroutine _runner;

    void Reset()
    {
        controller = GetComponent<FoxController>();
        bridge     = GetComponent<FoxAnimatorBridge>();
    }

    public void RunProgram()
    {
        StopProgram();
        _runner = StartCoroutine(RunRoutine());
    }

    public void StopProgram()
    {
        if (_runner != null) StopCoroutine(_runner);
        _runner = null;
    }

    public void ResetToSpawn()
    {
        StopProgram();
        controller.ResetToSpawn();
    }

    IEnumerator RunRoutine()
    {
        int steps = 0;

        foreach (var cmd in Program)
        {
            if (++steps > maxSteps)
            {
                bridge.PlayFail();
                yield break;
            }

            switch (cmd)
            {
                case CommandType.Forward:
                    if (controller.HasObstacleAhead())
                    {
                        bridge.PlayFail();
                        yield break;
                    }
                    yield return controller.MoveForwardOneTile();
                    break;

                case CommandType.TurnLeft:
                    yield return controller.TurnLeft90();
                    break;

                case CommandType.TurnRight:
                    yield return controller.TurnRight90();
                    break;

                case CommandType.Interact:
                    bridge.PlayInteract();
                    yield return controller.DoInteract();
                    // sem môžeme doplniť pickup/goal logiku (nižšie)
                    break;

                case CommandType.Stop:
                    yield break;
            }

            yield return null;
        }
    }
}
