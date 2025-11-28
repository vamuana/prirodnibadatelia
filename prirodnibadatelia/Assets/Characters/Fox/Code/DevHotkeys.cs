using UnityEngine;

[RequireComponent(typeof(FoxController))]
[RequireComponent(typeof(FoxAnimatorBridge))]
public class DevHotkeys : MonoBehaviour
{
    FoxController c; FoxAnimatorBridge b;

    void Awake(){ c = GetComponent<FoxController>(); b = GetComponent<FoxAnimatorBridge>(); }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) StartCoroutine(c.MoveForwardOneTile());
        if (Input.GetKeyDown(KeyCode.J)) StartCoroutine(c.TurnLeft90());
        if (Input.GetKeyDown(KeyCode.K)) StartCoroutine(c.TurnRight90());
        if (Input.GetKeyDown(KeyCode.I)) { b.PlayInteract(); StartCoroutine(c.DoInteract()); }
        if (Input.GetKeyDown(KeyCode.R)) c.ResetToSpawn();
    }
}
