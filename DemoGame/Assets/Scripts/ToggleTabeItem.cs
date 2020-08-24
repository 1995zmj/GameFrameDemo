using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTabeItem : MonoBehaviour
{
    public string TrueToFalse;
    public string FalseToTrue;
    public string TrueState;
    public string TalseState;
    private Toggle toggle;
    private bool toggleSelected;
    private Animator animator;
    void Awake()
    {
        toggle = GetComponent<Toggle>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        toggle.onValueChanged.AddListener(OnTabClicked);
        toggleSelected = toggle.isOn;
    }

    private void OnTabClicked(bool bIsOn)
    {
        if(toggleSelected == bIsOn)
            return;
        ChangeToggle(bIsOn);
    }

    private void ChangeToggle(bool bIsOn)
    {
        var tag = bIsOn ? "IsOnSelected" : "IsOnUnSelected";
        animator.SetTrigger(tag);
        toggleSelected = bIsOn;
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnTabClicked);
    }
}
