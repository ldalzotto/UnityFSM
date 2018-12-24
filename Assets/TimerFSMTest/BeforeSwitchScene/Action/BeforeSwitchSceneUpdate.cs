using UnityEngine;
using System.Collections;

public class BeforeSwitchSceneUpdate : FromChallenge.FSMAction
{
    public BeforeSwitchSceneComponent BeforeSwitchSceneComponent;
    public override void ExecuteAction()
    {
        BeforeSwitchSceneComponent.GO = true;
    }
}
