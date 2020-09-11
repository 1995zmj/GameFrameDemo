using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewItem : MonoBehaviour
{
    private int dataIndex = -1;
    public int DataIndex => dataIndex;
    public virtual void UpdateIndex(int index)
    {
        dataIndex = index;
    }
}
