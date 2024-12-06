using UnityEngine;
using System.Collections;

public class Engage : AISTateBase
{
    private AISTateBase currentSubState;
    private float stateTimeout = 10f; // Time after which AI returns to patrolling
    private Coroutine timeoutRoutine;

    public Engage(AISTateBase initialSubState)
    {
        currentSubState = initialSubState;
    }

    public override void EnterState(BlockingAI ai)
    {
        // Start timeout
        timeoutRoutine = ai.StartCoroutine(RunTimeout(ai));
        currentSubState.EnterState(ai);
    }

    public override void UpdateState(BlockingAI ai)
    {
        currentSubState.UpdateState(ai);

        // If target is lost or out of range, revert to Patrolling
        if (ai.targetCar == null || Vector3.Distance(ai.transform.position, ai.targetCar.position) > ai.detectionRange)
        {
            ai.SwitchState(ai.patrolling);
        }
    }

    public override void ExitState(BlockingAI ai)
    {
        if (timeoutRoutine != null)
        {
            ai.StopCoroutine(timeoutRoutine);
        }
        currentSubState.ExitState(ai);
    }

    public void SwitchSubState(BlockingAI ai, AISTateBase newSubState)
    {
        currentSubState.ExitState(ai);
        currentSubState = newSubState;
        currentSubState.EnterState(ai);
    }

    private IEnumerator RunTimeout(BlockingAI ai)
    {
        yield return new WaitForSeconds(stateTimeout);
        // If still in Engage state after timeout, revert to Patrolling
        if (ai.GetCurrentState() == this)
        {
            ai.SwitchState(ai.patrolling);
        }
    }
}
