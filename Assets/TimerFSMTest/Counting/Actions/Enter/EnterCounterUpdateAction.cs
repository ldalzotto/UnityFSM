using UnityEngine;
using System.Collections;

public class EnterCounterUpdateAction : FromChallenge.FSMAction
{
    public CounterComponent CounterComponent;

    public override void ExecuteAction()
    {
        CounterComponent.StateEnterCounter += 1;
    }
}
