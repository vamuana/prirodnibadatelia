using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class FoxController : MonoBehaviour
{
    [Header("Movement (grid)")]
    [SerializeField] float stepLength = 1f;
    [SerializeField] float stepDuration = 0.35f;
    [SerializeField] float turnDuration = 0.2f;

    [Header("Collision Check")]
    [SerializeField] LayerMask obstacleMask;   // nastav na vrstvu Obstacle
    [SerializeField] float probeDistance = 0.6f;
    [SerializeField] float groundY = 0f;       // výška “dlaždíc”

    [Header("Forward reference")]
    [SerializeField] Transform forwardRef;     // <— sem v Inspectore potiahni Armature

    // používaný smer dopredu (ak forwardRef nie je zadaný, padne späť na parent)
    Vector3 Fwd => forwardRef ? forwardRef.forward : transform.forward;

    public bool IsBusy { get; private set; }
    public float CurrentSpeed01 { get; private set; }

    Vector3 _spawnPos;
    Quaternion _spawnRot;

    void Awake()
    {
        _spawnPos = transform.position;
        _spawnRot = transform.rotation;
    }

    public void ResetToSpawn()
    {
        StopAllCoroutines();
        IsBusy = false;
        CurrentSpeed01 = 0f;
        transform.SetPositionAndRotation(_spawnPos, _spawnRot);
    }

    public bool HasObstacleAhead()
    {
        var origin = transform.position + Vector3.up * 0.2f;
        return Physics.Raycast(origin, Fwd, probeDistance, obstacleMask, QueryTriggerInteraction.Ignore);
    }

    public IEnumerator MoveForwardOneTile()
    {
        if (IsBusy) yield break;
        if (HasObstacleAhead()) yield break;

        IsBusy = true;

        var start = transform.position;
        var target = start + Fwd * stepLength;   // <— tu
        target.y = groundY;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / stepDuration;
            transform.position = Vector3.Lerp(start, target, Mathf.SmoothStep(0f, 1f, t));
            CurrentSpeed01 = 1f;
            yield return null;
        }

        transform.position = target;
        CurrentSpeed01 = 0f;
        IsBusy = false;
    }

    public IEnumerator TurnLeft90()  => TurnBy(-90f);
    public IEnumerator TurnRight90() => TurnBy( 90f);

    IEnumerator TurnBy(float degrees)
    {
        if (IsBusy) yield break;
        IsBusy = true;

        var start = transform.rotation;
        var target = Quaternion.Euler(0f, transform.eulerAngles.y + degrees, 0f);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / turnDuration;
            transform.rotation = Quaternion.Slerp(start, target, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        transform.rotation = target;
        IsBusy = false;
    }

    public IEnumerator DoInteract(float minDuration = 0.35f)
    {
        if (IsBusy) yield break;
        IsBusy = true;
        yield return new WaitForSeconds(minDuration);
        IsBusy = false;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var origin = transform.position + Vector3.up * 0.2f;
        Gizmos.DrawLine(origin, origin + (forwardRef ? forwardRef.forward : transform.forward) * probeDistance);
    }
#endif
}
