using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.UNetWeaver;
using UnityEngine;
using Utils;


public class StorageImage
{
    private const string storageKey = "StorageImage";
    private static StorageImage _instance = null;
    public static StorageImage Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StorageImage();
                return _instance;
            }
            return _instance;
        }
    }

    public StorageImage()
    {
        if (!PlayerPrefs.HasKey(storageKey))
        {
            PlayerPrefs.SetInt(storageKey, 0);
        }

    }

    public void SaveKey(string key,string value)
    {
        if (PlayerPrefs.HasKey(key))
        {
            Debug.Log("已经存在图片");
            return;
        }
        var num = PlayerPrefs.GetInt(storageKey);
        PlayerPrefs.SetString(storageKey + num,key);
        num++;
        PlayerPrefs.SetInt(storageKey, num);
        PlayerPrefs.SetString(key,value);
        Debug.Log("保存图片" + storageKey + num + "_" + key + "_" + value);

    }

    public int GetSaveImageNum()
    {
        return  PlayerPrefs.GetInt(storageKey);
    }

    public void ClearAllSaveImage()
    {
        var num = PlayerPrefs.GetInt(storageKey);
        for (int i = 0; i < num; i++)
        {
            var md = PlayerPrefs.GetString(storageKey + i);
            PlayerPrefs.DeleteKey(md);
            PlayerPrefs.DeleteKey(storageKey + i);
        }
        PlayerPrefs.SetInt(storageKey,0);
    }
    
    public string GetTexture(string url)
    {
        var key = GetMd5(url);
        return PlayerPrefs.GetString(key);
    }

    public bool IsExistTexture(string url)
    {
        var key = GetMd5(url);
        return PlayerPrefs.HasKey(key);
    }
    
    public void SaveTexture2D(string url, Texture2D texture2D)
    {
        var key = GetMd5(url);
        var bytes = texture2D.EncodeToPNG();
        
        var base64 = System.Convert.ToBase64String(bytes);
        SaveKey(key,base64);
    }
    
    public void LoadTexture2D(string url,out Texture2D texture2D){
        var base64 = GetTexture(url);
        byte[] bytes = System.Convert.FromBase64String(base64);
        texture2D = new Texture2D(100, 100);
        texture2D.LoadImage(bytes);
    }
    
    public string GetMd5(string iString)
    {
        if (string.IsNullOrEmpty(iString))
        {
            Debug.LogError($"UtilsCommon::GetMd5():字符串为空或为null!");
            return null;
        }
        // if (_md5 == null)
        // {
        //     _md5 = new MD5CryptoServiceProvider();
        // }

        var strMd5 = BitConverter.ToString(System.Text.Encoding.UTF8.GetBytes(iString));
        strMd5 = strMd5.ToLower();
        strMd5 = strMd5.Replace("-", "");
        return strMd5;
    }
   
}
