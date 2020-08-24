using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameDataManager : Singleton<GameDataManager>
{
    const int saveVersion = 7;
    private PersistentStorage storage;
    private Dictionary<string, GameData> gameDatas = new Dictionary<string, GameData>();
    
    public GameDataManager()
    {
        storage = new PersistentStorage();
        storage.savePath = Path.Combine(Application.persistentDataPath, "saveFile");
        gameDatas = new Dictionary<string, GameData>();
    }
    
    public GameData GetGameData(string key = "0")
    {
        GameData gameData;
        if (gameDatas.TryGetValue(key,out gameData))
        {
            
        }
        else
        {
            gameData = new GameData(nameof(gameData) + key);
            gameDatas.Add(key,gameData);
        }

        return gameData;
    }

    public void Save()
    {
        ResourcesManager.Instance.AddressablesLoad<BaseData>("Assets/Storage/New Base Data.asset", (AsyncOperationHandle<BaseData> asyncOperationHandle) =>
        {
            var assetObj = asyncOperationHandle.Result;
            JSONObject jo = new JSONObject();
            GetGameData().Save(ref jo);
            Debug.LogError(jo.ToString());
            assetObj.key = jo.ToString();
        });
    }
    
    public void Load()
    {
        ResourcesManager.Instance.AddressablesLoad<BaseData>("Assets/Storage/New Base Data.asset", (AsyncOperationHandle<BaseData> asyncOperationHandle) =>
        {
            var assetObj = asyncOperationHandle.Result;
            JSONObject jo = (JSONObject)JSON.Parse(assetObj.key);
            GetGameData().Load(jo);
        });
    }
}
