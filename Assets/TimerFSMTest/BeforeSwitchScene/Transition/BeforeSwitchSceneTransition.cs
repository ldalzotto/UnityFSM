using UnityEngine;
using System.Collections;

public class BeforeSwitchSceneTransition : FromChallenge.FSMTransition
{
    public BeforeSwitchSceneComponent BeforeSwitchSceneComponent;
    public override bool ComputeTransition()
    {
        return BeforeSwitchSceneComponent.GO;
    }
}
