using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class ToggleGroupLayerEvent : UnityEvent<Toggle, bool>
{
}


public class ToggleGroupLayer : MonoBehaviour
{
    private ToggleGroup toggleGroups;
    public List<ToggleTabeItem> toggleTabItems = new List<ToggleTabeItem>();
    [HideInInspector]
    public ToggleGroupLayerEvent onValueChanged = new ToggleGroupLayerEvent();
    void Awake()
    {
        toggleGroups = GetComponent<ToggleGroup>();
        foreach (var item in toggleTabItems)
        {
            var tmpItem = item;
            void onclick(bool iIsOn)
            {
                onValueChanged.Invoke( tmpItem.GetComponent<Toggle>(),iIsOn);
            } 
            tmpItem.GetComponent<Toggle>().onValueChanged.RemoveListener(onclick);
            tmpItem.GetComponent<Toggle>().onValueChanged.AddListener(onclick);
        }
    }
}
