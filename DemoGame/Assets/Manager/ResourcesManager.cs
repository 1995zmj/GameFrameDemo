using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourcesManager : Singleton<ResourcesManager>
{
    public void AddressablesLoad<T>(string AddressNameStr, Action<AsyncOperationHandle<T>> OnAssetObjLoaded)
    {
        Addressables.LoadAssetAsync<T>(AddressNameStr).Completed += OnAssetObjLoaded;
    }
}
