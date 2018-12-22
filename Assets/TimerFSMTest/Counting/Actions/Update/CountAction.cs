using UnityEngine;
using System.Collections;

public class CountAction : FromChallenge.FSMAction
{
    public CounterComponent CounterComponent;

    public override void ExecuteAction()
    {
        CounterComponent.CurrentCount += CounterComponent.IncrementDelta;
    }
}
