using System;
using System.Collections;
using System.IO;
using SimpleJSON;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary> 加载测试 </summary>
public class LoadTest : MonoBehaviour {
    
    void OnGUI()
    {
        // if(GUILayout.Button("LoadFromFile Assetbundle"))
        // {
        //     LoadFromFile();
        // }
        //
        // if(GUILayout.Button("LoadFromFileAsync Assetbundle"))
        // {
        //     StartCoroutine(LoadFromFileAsync());
        // }
        //
        // if(GUILayout.Button("Resources.Load"))
        // {
        //     // var go = Resources.Load("Prefabs/Tree");
        //     // var g = Instantiate(go);
        //     StartCoroutine(ras("Prefabs/Tree"));
        // }
        
        
        if(GUILayout.Button("init"))
        {
            // var go = Resources.Load("Prefabs/Tree");
            // var g = Instantiate(go);
            // StartCoroutine(ras("Prefabs/Tree"));
            // Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Tree.prefab").Completed += OnAssetObjLoaded;
            // ResourcesManager.Instance.AddressablesLoad<BaseData>("Assets/StorageDate/New Base Data.asset", (AsyncOperationHandle<BaseData> asyncOperationHandle) =>
            // {
            //     var assetObj = asyncOperationHandle.Result;
            //     assetObj.m_data.level = 100;
            // });
            
            // GameDataManager.Instance.storage.Save();
            getProperties();
        }
        
        
        if(GUILayout.Button("Save"))
        {
            GameDataManager.Instance.Save();
        }
        
        if(GUILayout.Button("Load"))
        {
            GameDataManager.Instance.Load();
        }
        
        
        

        
        // if(GUILayout.Button("LoadFromWWW Assetbundle"))
        // {
        //     StartCoroutine(LoadFromWWW());
        // }

        // if(GUILayout.Button("LoadFromUnityWebRequest Assetbundle"))
        // {
        //     StartCoroutine(LoadFromUnityWebRequest());
        // }
    }

    private event Action<int> _testEvent = null;
    public event Action<int> TestEvent
    {
        add => _testEvent += value;
        remove => _testEvent -= value;
    }

    void Start()
    {
        var str = "{\"id\":10001,\"name\":\"test\"}";
        var N = JSON.Parse(str);
        var k = N["id"].AsInt;
        Debug.Log(k + 1);
    }

    public void aaa(int i)
    {
        
    }

    public string assetName = "BundledSpriteObject";
    public string bundleName = "testbundle";

    public IEnumerator ras(string str)
    {
      
        ResourceRequest rr = Resources.LoadAsync<GameObject>(str);
        yield return rr;
        yield return new WaitForSeconds(0.5f);
        Instantiate(rr.asset).name = rr.asset.name;
     
    }
    
    
    void OnAssetObjLoaded(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
       
        var assetObj = asyncOperationHandle.Result;
        Instantiate(assetObj);
    }



    public void LoadFromFile()
    {
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName));

        if (localAssetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            return;
        }

        GameObject asset = localAssetBundle.LoadAsset<GameObject>(assetName);
        Instantiate(asset);
        localAssetBundle.Unload(false);
    }

    IEnumerator LoadFromFileAsync()
    {
        AssetBundleCreateRequest asyncBundleRequest =
            AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, bundleName));
        yield return asyncBundleRequest;

        AssetBundle localAssetBundle = asyncBundleRequest.assetBundle;
        
        if (localAssetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }

        AssetBundleRequest assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(assetName);
        yield return assetRequest;

        GameObject prefab = assetRequest.asset as GameObject;

        Instantiate(prefab);
        localAssetBundle.Unload(false);
    }

    
    // IEnumerator LoadFromWWW()
    // {
    //     using (WWW web = new WWW(Path.Combine(Application.streamingAssetsPath, bundleName)))
    //     {
    //         yield return web;
    //         AssetBundle remoteAssetBundle = web.assetBundle;
    //         if (remoteAssetBundle == null)
    //         {
    //             Debug.LogError("Failed to download AssetBundle!");
    //         }
    //
    //         Instantiate(remoteAssetBundle.LoadAsset(assetName));
    //         remoteAssetBundle.Unload(false);
    //
    //     }
    // }
    
    IEnumerator LoadFromUnityWebRequest()
    {
        string uri = "file:///" + Path.Combine(Application.streamingAssetsPath, bundleName);        
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
        yield return request.SendWebRequest();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        GameObject cube = bundle.LoadAsset<GameObject>(assetName);
        Instantiate(cube);
    }
    
    
    public void getProperties()
    {
        PlayerInfo playInfo = new PlayerInfo("test");
        var pr = playInfo.GetType().GetProperties();
        
        Type shpWrRiver = typeof(PlayerInfo);
        var  props = shpWrRiver.GetProperties();
        foreach (System.Reflection.PropertyInfo p in playInfo.GetType().GetProperties())
        {
            Debug.LogError(p.Name+ " :"+p.GetValue(playInfo, null));
        }
    }



}