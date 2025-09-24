using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GridAgent : MonoBehaviour
{
    public Vector2Int GridPos;
    public Facing Facing = Facing.E;
    [Range(0.05f, 1f)] public float StepDuration = 0.2f;
    public float TileSize = 1f;

    public WorldGrid world; // NEW

    Animator _anim;

    void Awake() => _anim = GetComponent<Animator>();

    public Vector3 GridToWorld(Vector2Int gp) => new(gp.x * TileSize, 0f, gp.y * TileSize);
    public void SnapToGrid() => transform.position = GridToWorld(GridPos);

    public void TurnLeft()  { Facing = (Facing)(((int)Facing + 3) % 4); _anim.Play("TurnL", 0, 0); }
    public void TurnRight() { Facing = (Facing)(((int)Facing + 1) % 4); _anim.Play("TurnR", 0, 0); }

    public IEnumerator StepForward()
    {
        var target = GridPos + Facing.ToDir();

        // VALIDATION against world tiles
        if (world && !world.CanEnter(target))
        {
            Debug.LogWarning($"Move blocked to {target} (WATER_TOUCH or SOLID)");
            _anim.Play("Idle");
            yield break;
        }

        var from = transform.position;
        var to = GridToWorld(target);
        _anim.Play("Walk", 0, 0);

        float t = 0;
        while (t < StepDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(from, to, t / StepDuration);
            yield return null;
        }
        transform.position = to;
        GridPos = target;
        _anim.Play("Idle");
    }
}
