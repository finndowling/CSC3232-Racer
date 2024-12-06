using UnityEngine;

public class Patrolling : AISTateBase
{
    public override void EnterState(BlockingAI ai)
    {
        Debug.Log(ai.name + " entered Patrolling State.");
        ai.MoveToNextPatrolPoint();
    }

    public override void UpdateState(BlockingAI ai)
    {
        if (ai.patrolPoints.Length == 0) return;

        // Move along patrol points
        float distance = Vector3.Distance(ai.transform.position, ai.patrolPoints[ai.currentPatrolIndex].position);
        if (distance < 2f)
        {
            ai.MoveToNextPatrolPoint();
        }

        // Detect enemy car
        Collider[] hitColliders = Physics.OverlapSphere(ai.transform.position, ai.detectionRange, ai.carLayer);
        foreach (var hit in hitColliders)
        {
            Vector3 directionToCar = (hit.transform.position - ai.transform.position).normalized;
            float dotProduct = Vector3.Dot(ai.transform.forward, directionToCar); 
            if (dotProduct > 0.5f)
            {
                ai.targetCar = hit.transform;

                // Switch to Engage state with Chase as initial sub-state
                ai.engage = new Engage(ai.chase);
                ai.SwitchState(ai.engage);
                return;
            }
        }
    }

    public override void ExitState(BlockingAI ai)
    {
        Debug.Log(ai.name + " exited Patrolling State.");
    }
}
