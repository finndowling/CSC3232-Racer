using UnityEngine;

public abstract class AISTateBase
{
    public abstract void EnterState(BlockingAI ai);
    public abstract void UpdateState(BlockingAI ai);
    public abstract void ExitState(BlockingAI ai);
}
