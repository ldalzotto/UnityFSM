using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTest : MonoBehaviour
{

    public int NumberofItem;
    public int AccessNumber;

    private int[] RawTable;
    private Hashtable Hashtable;
    private Dictionary<int, int> Dictionary;

    void Start()
    {
        PopulateRawTable();
        PopulateDictionary();
        Populatehashtable();

        var randomAccessArray = new int[AccessNumber];

        for (var i = 0; i < AccessNumber; i++)
        {
            randomAccessArray[i] = UnityEngine.Random.Range(0, NumberofItem);
        }


        for (var i = 0; i < AccessNumber; i++)
        {
            AccessRawTable(randomAccessArray[i]);
            AccessDictionary(randomAccessArray[i]);
            AccessTable(randomAccessArray[i]);
        }
    }

    public void PopulateRawTable()
    {
        RawTable = new int[NumberofItem];
        for (var i = 0; i < NumberofItem; i++)
        {
            RawTable[i] = i;
        }
    }

    public void PopulateDictionary()
    {
        Dictionary = new Dictionary<int, int>(NumberofItem);

        for (var i = 0; i < NumberofItem; i++)
        {
            Dictionary.Add(i, i);
        }
    }

    public void Populatehashtable()
    {
        Hashtable = new Hashtable(NumberofItem);

        for (var i = 0; i < NumberofItem; i++)
        {
            Hashtable.Add(i, i);
        }
    }

    public int AccessRawTable(int id)
    {
        return RawTable[id];
    }

    public int AccessDictionary(int id)
    {
        return Dictionary[id];
    }

    public object AccessTable(int id)
    {
        return Hashtable[id];
    }

}
