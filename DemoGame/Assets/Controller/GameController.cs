// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics.Tracing;
// using System.Text;
// // using DG.Tweening;
// using UnityEngine;
// using UnityEngine.Audio;
// using Random = System.Random;
//
// namespace QFramework.BigTown
// {
//     
//     public class GameController : MonoBehaviour
//     {
//         // Start is called before the first frame update
//
//         private ResLoader mResLoader = ResLoader.Allocate();
//         private List<string> m_logEntries = new List<string>();
//         private bool bFirstOnShow = true;
//         private void Awake()
//         {
//             InitialLogic();
//             LoadConfig();
//             CatchException();
//         }
//
//         private void CatchException()
//         {
//             Application.logMessageReceived += (string condition, string stackTrace, LogType type) =>
//             {
//                 if (type == LogType.Exception || type == LogType.Error)
//                 {
//                     var errorString = string.Format("{0}\n{1}", condition, stackTrace);
//                     if (m_logEntries.Contains(errorString))
//                     {
//                         return;                        
//                     }
//                     m_logEntries.Add(errorString);
//                     GameData gameData = GameDataManager.Instance.GetGameData();
//                     #if !UNITY_EDITOR
//                         ServerUtil.LogErrorEvent("3003001", errorString, gameData.playerInfo.Level, gameData.playerInfo.AccumulateGold, gameData.playerInfo.Gem, 0);
//                     #endif
//                 }
//             };
//         }
//
//         private void LoadConfig()
//         {
//             //加载配置
//             string path = "";
//             #if UNITY_ANDROID && !UNITY_EDITOR
//                 path = "jar:file://" + Application.dataPath + "!assets/Config/Config.data";
//                 StartCoroutine(ReadData(path));
//             #elif UNITY_IPHONE && !UNITY_EDITOR
//                 path =  Application.dataPath + "/Raw/Config/Config.data";
//                 ConfigManager.LoadConfig(path);
//                 LoadGame();
//             #elif UNITY_EDITOR
//                 path = Application.dataPath + "/StreamingAssets/Config/Config.data";
//                 ConfigManager.LoadConfig(path);
//                 LoadGame();
//             #endif
//             Debug.Log("kkkkkkkkkkkkkkkk____path = " + path);
//         }
//
//         private void InitialLogic()
//         {
//             //初始化资源管理器
//             ResMgr.Init();
//             YmnSdk.Instance.init();
//             //设置游戏帧率
//             Application.targetFrameRate = 30;
//             // Debug.Log("获取设备Id: " + NativeSupport.Instance.getDeviceId());
//             UIMgr.SetResolution(750, 1334, 0);
//             AudioManager.Instance.Init();
//             AudioManager.On();
//             AudioManager.LoadAllAudioClip("Audio");
// #if !UNITY_EDITOR
//     Log.Level = LogLevel.Error;
// #endif
//             AudioManager.PlayMusic("Audio/bgm", true);
//             Application.quitting += () =>
//             {
//                 if (GameDataManager.Instance.GetGameData().isLoadKeyChar)
//                 {
//                     GameDataManager.Instance.GetGameData().SavePlayerDataInfo();
//                 }
//             };
//         }
//         
//         
//         IEnumerator ReadData(string path)
//         {
//             WWW www = new WWW(path);
//             yield return www;
//             while (www.isDone == false)
//             {
//                 yield return new WaitForEndOfFrame();
//             }     
//             byte[] bytes = ArrayUtil.SubByte(www.bytes, 4, www.bytes.Length);
//             ConfigManager.DeserializeData(bytes);
//             LoadGame();
//         }
//
//         void LoadGame()
//         {
//             TimeController.Instance.Pause = true;
//             GameDataManager.Instance.GetGameData().UnserializeData();
//             GuideManager.Instance.Init();
//             ServerUtil.UserLogin();
//
//             Debug.Log("name = " + StringUtil.GetRandomString(4));
//             
//             if (GameDataManager.Instance.GetGameData().isLoadKeyChar)
//             {
//                 MergeController.Instance.OpenAllBox();
//                 EventListenerManager.Instance.Trigger(ListenerType.OnGuidConditionEvent, new object[1] { "gamestart" });
//                 LoadingController.Instance.LoadingHouse(() =>
//                 {
//                     if(GameDataManager.Instance.GetGameData().isFinishedBuild)
//                     {
//                         UIMgr.CloseAllBut(typeof(IGuideLock));
//                         UIMgr.OpenPanel<UIPhotographLayer>();
//                     }
//                     EarningController.Instance.CalculateOffline();
//                     TimeController.Instance.Pause = false;
//                     YmnSdk.Instance.logEvent("on_game_page");
//                 });
//             }
//         }
//         
//
//         private void Update()
//         {
//             if(GameDataManager.Instance.GetGameData().isLoadKeyChar)
//                 TimeController.Instance.GameUpdate();
//         }
//
//         private void OnDestroy()
//         {
//             mResLoader.Recycle2Cache();
//             mResLoader = null;
//         }
//
//
//         private void OnApplicationQuit()
//         {    
//             if (GameDataManager.Instance.GetGameData().isLoadKeyChar)
//             {
//                 GameDataManager.Instance.GetGameData().SavePlayerDataInfo();
//             }
//         }
//
//         void OnApplicationPause(bool pauseStatus)
//         {
//             if (pauseStatus)
//             {
//                 EventListenerManager.Instance.Trigger(ListenerType.OnApplicationHide);
//                 TimeController.Instance.Pause = true;
//
//                 if (GameDataManager.Instance.GetGameData().isLoadKeyChar)
//                 {
//                     GameDataManager.Instance.GetGameData().SavePlayerDataInfo();
//                 }
//
//             }
//             else
//             {
//                 if (this.bFirstOnShow)
//                 {
//                     this.bFirstOnShow = false;
//                     return;
//                 }
//                 EarningController.Instance.CalculateOffline();
//                 EventListenerManager.Instance.Trigger(ListenerType.OnApplicationShow);
//                 TimeController.Instance.Pause = false;
//             }
//             
//             Debug.Log("OnApplicationPause,pauseStatus:" + pauseStatus);
//         }
//
//     }
// }
