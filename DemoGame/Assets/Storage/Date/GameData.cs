using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class GameData: PersistableObject
{
    public PlayerInfo playerInfo;

    public GameData(string flag): base(flag)
    {
        playerInfo = new PlayerInfo(nameof(playerInfo));
    }

    public override void Save(ref JSONObject jsonObject)
    {
        playerInfo.Save(ref value);
        
        base.Save(ref jsonObject);
    }

    public override void Load(JSONObject jsonObject)
    {
        base.Load(jsonObject);
        
        playerInfo.Load(value);
    }
}
