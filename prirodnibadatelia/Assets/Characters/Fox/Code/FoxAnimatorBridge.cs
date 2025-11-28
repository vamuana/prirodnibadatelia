using UnityEngine;

[RequireComponent(typeof(FoxController))]
public class FoxAnimatorBridge : MonoBehaviour
{
    [SerializeField] Animator anim;
    FoxController controller;

    static readonly int SpeedHash    = Animator.StringToHash("Speed");
    static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    static readonly int InteractHash = Animator.StringToHash("Interact");
    static readonly int WinHash      = Animator.StringToHash("Win");
    static readonly int FailHash     = Animator.StringToHash("Fail");

    void Reset() { anim = GetComponentInChildren<Animator>(); }

    void Awake()
    {
        controller = GetComponent<FoxController>();
        if (!anim) anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float spd = controller?.CurrentSpeed01 ?? 0f;
        anim.SetFloat(SpeedHash, spd);
        anim.SetBool(IsMovingHash, spd > 0.1f);
    }

    public void PlayInteract() => anim.SetTrigger(InteractHash);
    public void PlayWin()      => anim.SetTrigger(WinHash);
    public void PlayFail()     => anim.SetTrigger(FailHash);
}
