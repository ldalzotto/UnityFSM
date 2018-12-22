using UnityEngine;
using System.Collections;

public class IsCounterThreshold : FromChallenge.FSMTransition
{
    public CounterComponent CounterComponent;

    public override bool ComputeTransition()
    {
        return CounterComponent.CurrentCount >= CounterComponent.TargetCount;
    }
}
