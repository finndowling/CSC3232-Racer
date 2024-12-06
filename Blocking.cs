using UnityEngine;

public class Blocking : AISTateBase
{
    public override void EnterState(BlockingAI ai)
    {
        // Optional: Remove debug log if not needed
        // Debug.Log(ai.name + " entered Blocking State.");
    }

    public override void UpdateState(BlockingAI ai)
    {
        if (ai.targetCar == null) 
            return;

        if (Vector3.Distance(ai.transform.position, ai.targetCar.position) > ai.detectionRange)
            return;

        Vector3 targetPosition = ai.targetCar.position + ai.targetCar.forward * 2f;
        Vector3 moveDirection = (targetPosition - ai.transform.position).normalized;
        ai.rigidBody.velocity = moveDirection * ai.blockingSpeed;
    }

    public override void ExitState(BlockingAI ai)
    {
        // Optional: Remove debug log if not needed
        // Debug.Log(ai.name + " exited Blocking State.");
    }
}
