using System;
using UnityEngine;  
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

class TaskManager: Singleton<TaskManager>
{
    private TaskDriver driver = null;
    // private int instanceId = 0;
    private List<TaskHandler> pool = null;
    // 判断产出和回收是否一致
    private int creatCount = 0;
    public TaskManager()
    {
        if(driver == null) 
        {  
            pool = new List<TaskHandler>();
            GameObject go = new GameObject("TaskManagerDriver"); 
            GameObject.DontDestroyOnLoad(go);
            driver = go.AddComponent<TaskDriver>();  
        }  
    }

    private Coroutine StartCoroutine(IEnumerator routine)
    {
        return driver.StartCoroutine(routine);
    }

    private void StopCoroutine(IEnumerator routine)
    {
        driver.StopCoroutine(routine);
    }
    
  
    public TaskHandler Create(IEnumerator coroutine)  
    {  
        TaskHandler handler = GetTaskHandler();  
        handler.Init(coroutine);
        return handler;
    } 
    
    private TaskHandler GetTaskHandler()
    {
        TaskHandler handler;
        if (pool.Count > 0)
        {
            handler = pool[pool.Count - 1];
            pool.Remove(handler);
        }
        else
        {
            handler = new TaskHandler();
        }
        creatCount++;
        return handler;
    }

    private void RecycleTaskHandler(TaskHandler handler)
    {
        pool.Add(handler);
        creatCount--;
    }
    
    private class TaskDriver: MonoBehaviour
    {
        private void Start()
        {
            
        }
    }
    
    public class TaskHandler
    {
        private IEnumerator coroutine;  
        private bool running;  
        private bool paused;
        public bool Paused
        {
            get => paused;
            set => paused = value;
        }
        
        public TaskHandler()  
        {  
        }

        public void Init(IEnumerator c)
        {
            running = false;
            paused = false;
            coroutine = c;  
        }
          
        public void Start()  
        {
            if (running)
            {
#if UNITY_EDITOR
                Debug.Log("已经在执行");
#endif
                return;
            }
            running = true;  
            TaskManager.Instance.StartCoroutine(CallWrapper());  
        }  
          
        public void Stop()  
        {  
            running = false;  
            TaskManager.Instance.StopCoroutine(CallWrapper());
        }  
          
        private IEnumerator CallWrapper()  
        {  
            yield return null;  
            IEnumerator e = coroutine;  
            while(running) {  
                if(paused)  
                    yield return null;  
                else {  
                    if(e != null && e.MoveNext()) {  
                        yield return e.Current;  
                    }  
                    else {  
                        running = false;  
                    }  
                }  
            }
        }  
        
        public void Recycle()
        {
            if (running)
            {
                Stop();
            }
            TaskManager.Instance.StopCoroutine(CallWrapper());
            TaskManager.Instance.RecycleTaskHandler(this);
        }
    }  
}  