using UnityEngine;
using System.Collections;

public class IsTimeElapsed : FromChallenge.FSMTransition
{
    public WaitingComponent WaitingComponent;

    public override bool ComputeTransition()
    {
        return WaitingComponent.ElapsedTime >= WaitingComponent.TargetTime;
    }
}
