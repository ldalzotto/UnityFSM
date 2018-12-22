using UnityEngine;
using System.Collections;

public class IsAnyKeyPressed : FromChallenge.FSMTransition
{
    public override bool ComputeTransition()
    {
        return Input.anyKeyDown;
    }
}
