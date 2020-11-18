using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // GetValueType(true);
        // GetValueType(1);
        // GetValueType("123");
        int f = 0;
        var q = Activator.CreateInstance( f.GetType());
        Debug.Log(f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetValueType(object k)
    {
        Debug.Log(k.GetType());
        
    }
    
    
}
