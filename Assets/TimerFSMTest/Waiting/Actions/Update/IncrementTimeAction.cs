using UnityEngine;
using System.Collections;

public class IncrementTimeAction : FromChallenge.FSMAction
{
    public WaitingComponent WaitingComponent;

    public override void ExecuteAction()
    {
        WaitingComponent.ElapsedTime += Time.deltaTime;
    }
}
