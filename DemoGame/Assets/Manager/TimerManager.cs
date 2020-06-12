using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;

public delegate void Handler();
public class TimerManager : Singleton<TimerManager>
{
    
    private List<TimerHandler> _pool = new List<TimerHandler>();
    private List<TimerHandler> _trash = new List<TimerHandler>();
    private List<TimerHandler> _handlers = new List<TimerHandler>();
    private float _time = 0.0f;
    private int _frame = 0;
    private bool _pause = false;
    private int _instanceId = 0;
    private TimerDriver _driver = null;
    
    public TimerManager()
    {
        if(_driver == null) 
        {  
            GameObject go = new GameObject("TimerManagerDriver"); 
            GameObject.DontDestroyOnLoad(go);
            _driver = go.AddComponent<TimerDriver>();  
        }  
    }
    
    public bool Pause
    {
        get => _pause;
        set => _pause = value;
    }
    
    private void AdvanceTime(float dt)
    {
        if (Pause)
        {
            return;
        }
        
        _time += dt;
        _frame += 1;
        for (int i = 0; i < _handlers.Count; i++)
        {
            TimerHandler handler = _handlers[i];
            var t = handler.useFrame ? _frame:_time;

            // if (false == handler.active)
            // {
            //     continue;
            // }

            if (handler.repeatCount == 0)
            {
                PushTrash(handler);
                continue;
            }
            
            if (t >= handler.exeTime)
            {
                while (t >= handler.exeTime)
                {
                    if (handler.repeatCount == -1)
                    {
                        handler.Execute();
                    }
                    else if(handler.repeatCount > 0)
                    {
                        handler.repeatCount -= 1;
                        handler.Execute();
                    }
                }
            }
        }

        ClearTrash();
    }

    private int Create( bool useFrame, float delay, Delegate method,int repeatCount = -1)
    {
        if (method == null || repeatCount == 0 || delay < 0)
        {
            return -1;
        }

        TimerHandler handler = GetTimerHandler();
        handler.useFrame = useFrame;
        handler.delay = delay;
        handler.method = method;
        handler.repeatCount = repeatCount;
        handler.exeTime = delay + _time;
        _handlers.Add(handler);
        return handler.instanceId;
    }

    private void ClearTrash()
    {
        if (_trash.Count > 0)
        {
            for (int i = 0; i < _trash.Count; i++)
            {
                TimerHandler handler = _trash[i];
                handler.Clear();
                _handlers.Remove(handler);
                _pool.Add(handler);
            }
            _trash.Clear();
        }
    }

    private void PushTrash(TimerHandler handler)
    {
        handler.repeatCount = 0;
        _trash.Add(handler);
    }

    private void Clear(TimerHandler handler)
    {
        // 会在 AdvanceTime 统一去除
        PushTrash(handler);
    }
    
    private void Clear(Delegate method) 
    {
        TimerHandler handler = _handlers.FirstOrDefault(t => t.method == method);
        Clear(handler);
    }
    private void Clear(int handlerInstanceId)
    {
        TimerHandler handler = _handlers.FirstOrDefault(t => t.instanceId == handlerInstanceId);
        Clear(handler);
    }
    

    private TimerHandler GetTimerHandler()
    {
        TimerHandler handler;
        if (_pool.Count > 0)
        {
            handler = _pool[_pool.Count - 1];
            _pool.Remove(handler);
        }
        else
        {
            handler = new TimerHandler();
        }

        handler.instanceId = _instanceId++;
        return handler;
    }
  
    /// /// <summary>
    /// 定时执行一次(秒)
    /// </summary>
    /// <param name="delay">延迟时间(单位毫秒)</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public int DoOnce(float delay, Handler method) {
        return Create(false, delay, method,1);
    }

    /// /// <summary>
    /// 定时重复执行(秒)
    /// </summary>
    /// <param name="delay">延迟时间(单位毫秒)</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public int DoLoop(float delay, Handler method,int repeatCount = -1) {
        return Create(false, delay, method, repeatCount);
    }
    
    /// <summary>
    /// 定时执行一次(基于帧率)
    /// </summary>
    /// <param name="delay">延迟时间(单位为帧)</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public int doFrameOnce(int delay, Handler method) {
        return Create(true, delay, method, 1);
    }

    /// <summary>
    /// 定时重复执行(基于帧率)
    /// </summary>
    /// <param name="delay">延迟时间(单位为帧)</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public int doFrameLoop(int delay, Handler method, int repeatCount = -1) {
        return Create(true, delay, method, repeatCount);
    }


    /// <summary>
    /// 清理定时器
    /// </summary>
    /// <param name="method">method为回调函数本身</param>
    public void ClearTimer(Handler method) {
        Clear(method);
    }
    
    /// <summary>
    /// 清理定时器
    /// </summary>
    /// <param name="handlerInstanceId">handler的实例id</param>
    public void ClearTimer(int handlerInstanceId) {
        Clear(handlerInstanceId);
    }

    private class TimerDriver: MonoBehaviour
    {
        private void Start()
        {
            
        }

        private void Update()
        {
            TimerManager.Instance.AdvanceTime(Time.deltaTime);
        }
    }
    
    private class TimerHandler
    {
        public int instanceId;
        public float delay;
        public int repeatCount;
        
        public bool useFrame;

        public float exeTime;
        public Delegate method;

        public void Clear()
        {
            method = null;
            repeatCount = 0;
        }

        public void Execute()
        {
            if (method != null)
            {
                try
                {
                    exeTime += delay;
                    method.DynamicInvoke();
                }
                catch (Exception e)
                {
#if UNITY_EDITOR
                    Debug.Log("method error" + e.ToString());
#endif
                    throw;
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("method null");
#endif
            }
                
        }
    }
    
}
