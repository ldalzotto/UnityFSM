using UnityEngine;
using System.Collections;

public class CounterComponent : MonoBehaviour
{
    public int TargetCount;
    public int IncrementDelta;

    public int StateEnterCounter;

    private int _currentCount;

    public int CurrentCount { get => _currentCount; set => _currentCount = value; }

    private void Awake()
    {
        StateEnterCounter = 0;
    }

}
