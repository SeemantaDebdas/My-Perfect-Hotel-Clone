using UnityEngine;

public abstract class State
{
    public virtual void Enter() {}
    public virtual void Tick() {}
    public virtual void FixedTick() { }
    public virtual void LateTick() { }
    public virtual void OnAnimatorMove() { }
        
    public virtual void Exit() { }

    protected float GetNormalizedTime(Animator animator, string tag, int layer = 0)
    {
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(layer);
        AnimatorStateInfo nextStateInfo = animator.GetNextAnimatorStateInfo(layer);

        if (animator.IsInTransition(layer) && nextStateInfo.IsTag(tag))
        {
            return nextStateInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(layer) && currentStateInfo.IsTag(tag))
        {
            return currentStateInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }
    protected void MatchTarget(Animator animator, Vector3 matchPosition, Quaternion matchRotation, AvatarTarget target, MatchTargetWeightMask weightMask, float normalisedStartTime, float normalisedEndTime)
    {
        if (animator.IsInTransition(0))
        {
            return;
        }

        if (animator.isMatchingTarget)
        {
            return;
        }

        float normalizeTime = Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f);

        if (normalizeTime > normalisedEndTime)
        {
            return;
        }
        animator.MatchTarget(matchPosition, matchRotation, target, weightMask, normalisedStartTime, normalisedEndTime);
    }

    protected void FaceTarget(Transform target, Statemachine SM)
    {
        float rotationMultiplier = 5f;

        Vector3 directionToTarget = target.position - SM.transform.position;
        directionToTarget.y = 0;
        directionToTarget.Normalize();

        float angleToTarget = Vector3.Angle(SM.transform.forward, directionToTarget);

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        SM.transform.rotation = Quaternion.RotateTowards(SM.transform.rotation, targetRotation, angleToTarget * rotationMultiplier * Time.deltaTime);
    }
    
    protected void Print(string text) => Debug.Log(text);

}
