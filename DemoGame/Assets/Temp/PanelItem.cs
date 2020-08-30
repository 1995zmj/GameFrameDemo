using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelItem : ScrollViewItem
{
    [SerializeField] private Text txt_info;
    // Start is called before the first frame update

    public override void UpdateIndex(int index)
    {
        txt_info.text = index.ToString();
    }
}
