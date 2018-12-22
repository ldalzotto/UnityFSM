using UnityEngine;
using System.Collections;

public class IsSwitchingScene : FromChallenge.FSMTransition
{
    public CounterComponent CounterComponent;

    public override bool ComputeTransition()
    {
        return CounterComponent.StateEnterCounter > 2;
    }
}
