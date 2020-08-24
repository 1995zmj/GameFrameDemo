using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;



public class PersistableObject
{
    protected string key;
    protected JSONObject value;
    public PersistableObject(string flag)
    {
        key = flag;
        value = new JSONObject();
    }
    
    public virtual void Reset()
    {
        
    }
    
    public virtual void Save(ref JSONObject jsonObject)
    {
        jsonObject.Add(key,value);
    }

    public virtual void Load(JSONObject jsonObject)
    {
        if (jsonObject.HasKey(key))
        {
            value = (JSONObject)jsonObject[key];
        }
    }
}
