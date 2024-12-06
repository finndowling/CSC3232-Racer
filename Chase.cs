using UnityEngine;

public class Chase : AISTateBase
{
    public override void EnterState(BlockingAI ai)
    {
        Debug.Log(ai.name + " entered Chase State.");
    }

    public override void UpdateState(BlockingAI ai)
    {
        if (ai.targetCar == null) return;

        // Chase the target car
        Vector3 direction = (ai.targetCar.position - ai.transform.position).normalized;
        ai.rigidBody.velocity = direction * ai.blockingSpeed;

        // If car is closer, randomly switch to Blocking
        float distanceToCar = Vector3.Distance(ai.transform.position, ai.targetCar.position);
        if (distanceToCar < ai.detectionRange / 2f)
        {
            Vector3 directionToCar = (ai.targetCar.position - ai.transform.position).normalized;
            float dotProduct = Vector3.Dot(ai.transform.forward, directionToCar);
            if (dotProduct > 0.5f)
            {
                // 50% chance to switch to Blocking
                if (Random.value > 0.5f)
                {
                    Engage parent = ai.GetCurrentState() as Engage;
                    parent.SwitchSubState(ai, ai.blocking);
                }
            }
        }
    }

    public override void ExitState(BlockingAI ai)
    {
        Debug.Log(ai.name + " exited Chase State.");
    }
}
