using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter: Singleton<DataCenter>
{
    private Dictionary<string, DataTable> dataDic;

    public DataTable GetData(string key)
    {
        if (dataDic.ContainsKey(key))
        {
            
        }
        else
        {
            dataDic[key] = new DataTable();
        }
        return dataDic[key];
    }
}
