using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterInstanciate : MonoBehaviour
{

    public GameObject CounterPrefab;
    public int Nb;
    public Transform Parent;

    private void Start()
    {
        for (var i = 0; i < Nb; i++)
        {
            var c = Instantiate(CounterPrefab);
            c.transform.parent = Parent;
        }
    }

}
