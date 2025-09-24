using UnityEngine;

public class FoxAnimatorBridge : MonoBehaviour
{
    [Header("Assign the Animator on the fox@all object")]
    [SerializeField] private Animator animator;

    // Animator parameter hashes (Triggers)
    private static readonly int StepHash         = Animator.StringToHash("Step");
    private static readonly int TurnLHash        = Animator.StringToHash("TurnL");
    private static readonly int TurnRHash        = Animator.StringToHash("TurnR");
    private static readonly int GrabHash         = Animator.StringToHash("Grab");
    private static readonly int FallHash         = Animator.StringToHash("Fall");
    private static readonly int CelebrateHash    = Animator.StringToHash("Celebrate");
    private static readonly int ShouldersUpHash  = Animator.StringToHash("ShouldersUp");

    private void Reset()
    {
        // Auto-fill when you add the component
        if (animator == null) animator = GetComponent<Animator>();
    }

    // ----- Public API your game can call -----
    public void PlayIdle()               => animator.CrossFadeInFixedTime("Idle", 0.05f);

    public void Step()                   => animator.SetTrigger(StepHash);
    public void TurnLeft()               => animator.SetTrigger(TurnLHash);
    public void TurnRight()              => animator.SetTrigger(TurnRHash);
    public void Grab()                   => animator.SetTrigger(GrabHash);
    public void Fall()                   => animator.SetTrigger(FallHash);
    public void Celebrate()              => animator.SetTrigger(CelebrateHash);
    public void ShouldersUp()            => animator.SetTrigger(ShouldersUpHash);

    // Optional helpers
    public void SetAnimSpeed(float s)    => animator.speed = s;   // e.g., speed up/down if you want
    public void ResetAllTriggers()
    {
        animator.ResetTrigger(StepHash);
        animator.ResetTrigger(TurnLHash);
        animator.ResetTrigger(TurnRHash);
        animator.ResetTrigger(GrabHash);
        animator.ResetTrigger(FallHash);
        animator.ResetTrigger(CelebrateHash);
        animator.ResetTrigger(ShouldersUpHash);
    }
}
