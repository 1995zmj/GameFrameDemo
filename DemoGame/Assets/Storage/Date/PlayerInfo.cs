using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class PlayerInfo : PersistableObject
{
    public PlayerInfo(string flag) : base(flag)
    {
    }

    private string ss;
    private int level = 0;
    public int Level
    {
        get => level;
        set => level = value;
    }
    public override void Save(ref JSONObject jsonObject)
    {
        value.Add(nameof(level),level);
        
        base.Save(ref jsonObject);
    }

    public override void Load(JSONObject jsonObject)
    {
        base.Load(jsonObject);
        if (value.HasKey(nameof(level)))
        {
            level = value[nameof(level)];
        }
    }
}
