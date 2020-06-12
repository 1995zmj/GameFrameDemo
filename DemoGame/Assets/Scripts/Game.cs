using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Game : MonoBehaviour
{
    private TaskManager.TaskHandler task;
    // Start is called before the first frame update
    void Start()
    {
        string configpath = Application.streamingAssetsPath + "/Config/Config.data";
        ConfigManager.LoadConfig(configpath);

        Debug.Log(Test1CfgMgr.Instance.GetDataByID(1).Name1);
        // TimerManager.Instance.DoLoop(1, () =>
        // {
        //     Debug.Log("time over");
        // });
        //
        TimerManager.Instance.DoOnce(5.0f, () =>
        {
            Debug.Log("over");
        });
        
        // TimerManager.Instance.doFrameLoop(30, () =>
        // {
        //     Debug.Log("frame over");
        // });

        // task = TaskManager.Instance.Create(MyAwesomeTask());
        // task.Start();
        // TimerManager.Instance.DoOnce(2f, () =>
        // {
        //     Debug.Log("task stop");
        //     task.Stop();
        // });
    }
    
    IEnumerator MyAwesomeTask()
    {
        var i = 0;
        while (true)
        {
            Debug.Log(i.ToString());
            i++;
            yield return null;
        }
    }
    

    [ContextMenu("Pause TimerManger")]
    public void Pause()
    {
        TimerManager.Instance.Pause = !TimerManager.Instance.Pause;
    }

    [ContextMenu("remove task")]
    public void RemoveTask()
    {
        task.Recycle();
        task = null;
    }
    
    [ContextMenu("start task")]
    public void StartTask()
    {
        task.Start();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
