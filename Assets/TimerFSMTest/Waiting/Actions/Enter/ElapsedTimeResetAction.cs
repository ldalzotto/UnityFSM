

public class ElapsedTimeResetAction : FromChallenge.FSMAction
{
    public WaitingComponent WaitingComponent;

    public override void ExecuteAction()
    {
        WaitingComponent.ElapsedTime = 0f;
    }
}
