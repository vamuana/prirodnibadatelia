using UnityEngine;

public class FoxKeyboardDebug : MonoBehaviour
{
    [SerializeField] private FoxAnimatorBridge fox;

    private void Reset()
    {
        if (fox == null) fox = GetComponent<FoxAnimatorBridge>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) fox.Step();
        if (Input.GetKeyDown(KeyCode.A)) fox.TurnLeft();
        if (Input.GetKeyDown(KeyCode.D)) fox.TurnRight();
        if (Input.GetKeyDown(KeyCode.G)) fox.Grab();
        if (Input.GetKeyDown(KeyCode.F)) fox.Fall();
        if (Input.GetKeyDown(KeyCode.C)) fox.Celebrate();
        if (Input.GetKeyDown(KeyCode.S)) fox.ShouldersUp();
        if (Input.GetKeyDown(KeyCode.I)) fox.PlayIdle();
    }
}
