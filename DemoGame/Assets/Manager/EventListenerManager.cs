// using System;
// using System.Collections;  
// using System.Collections.Generic;
// using UnityEngine;  
// using UnityEngine.Events;
// using Utils;
//
// public class EventListenerManager : ClassExtension  
// {
//     public delegate void Callback();
//     public delegate void CallbackPrm(params object[] prm);
//     private Dictionary<string, Dictionary<object ,CallbackPrm>> _myEventCallback = new Dictionary<string,  Dictionary<object ,CallbackPrm>>();
//
//     private static EventListenerManager eventManager = new EventListenerManager();  
//     private EventListenerManager()  
//     {  
//         
//     }  
//
//     public static EventListenerManager Instance  
//     {  
//         get  
//         {  
//             return eventManager;  
//         }  
//     }
//
//     public void AddEventListener(string eventName, object caller,CallbackPrm callback)
//     {
//         if (eventManager._myEventCallback.ContainsKey(eventName))
//         {
//             if (eventManager._myEventCallback[eventName].ContainsKey(caller))
//             {
//                 Debug.Log("重复注册: " + eventName);
//             }
//             else
//             {
//                 eventManager._myEventCallback[eventName].Add(caller, callback);
//             }
//         }
//         else
//         {
//             Dictionary<object, CallbackPrm> tempDic = new Dictionary<object, CallbackPrm>();
//             tempDic.Add(caller, callback);
//             eventManager._myEventCallback.Add(eventName, tempDic);
//         }
//     }
//
//     public void AddEventListener(string eventName, object caller,Callback callback)
//     {
//         if (eventManager._myEventCallback.ContainsKey(eventName))
//         {
//             if (eventManager._myEventCallback[eventName].ContainsKey(caller))
//             {
//                 Debug.Log("重复注册: " + eventName);                
//             }
//             else
//             {
//                 eventManager._myEventCallback[eventName].Add(caller,(o)=>
//                 {
//                     callback();
//                 });
//             }
//         }
//         else
//         {
//             Dictionary<object, CallbackPrm> tempDic = new Dictionary<object, CallbackPrm>();
//             tempDic.Add(caller, (object[] o)=> { callback(); });
//             eventManager._myEventCallback.Add(eventName, tempDic);
//         }
//     }
//
//     
//     public void RemoveEventListener(object caller)
//     {
//         var _callerName = caller.GetType().Name;
//         foreach (var item in eventManager._myEventCallback)
//         {
//             item.Value.Remove(caller);
//         }
//     }
//    
//     
//     
//     public void Trigger(string eventName,params object[] o)
//     {
//         var _caller = "";
//         var _callback = eventName;
//         try
//         {
//             if (eventManager._myEventCallback.ContainsKey(_callback))
//             {
//                 var events  = eventManager._myEventCallback[_callback];
//                 if (null == events)
//                 {
//                     W($"Trigger():err : 未注册 {_callback}");
//                     return;
//                 }
//                 var keys = new List<object>(events.Keys);
//                 for (int i = 0; i < keys.Count; i++)
//                 {
//                     var key = keys[i];
//                     _caller = key.GetType().Name;
//                     if ("WorkMan".Equals(_caller))
//                     {
//                         I($"Trigger():Caller{_caller}");
//                     }
//                     if (eventManager._myEventCallback[_callback][key] != null)
//                     {
//                         eventManager._myEventCallback[_callback][key].Invoke(o);
//                     }
//                 }
//             }
//             else {
//                 W($"Trigger():err : 未注册 {_callback}");
//             }
//         }
//         catch (Exception e)
//         {
//             E($"Trigger():Caller:{_caller} EvenetName:{_callback} \n Exception:{e.Message}\n StackTrace:\n {e.StackTrace}");
//         }
//         
//         
//     }
// }