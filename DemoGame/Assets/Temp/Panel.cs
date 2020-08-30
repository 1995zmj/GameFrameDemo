using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField]
    private ScrollViewCtrl _scrollViewCtrl;

    [SerializeField] private ScrollViewItem prefab;
    // Start is called before the first frame update
    void Start()
    {
        _scrollViewCtrl.init(prefab);
        var kist = new List<int>()
        {
            1,2,3,4,5,6,7,8,9,10
        };
        _scrollViewCtrl.UpdateData(kist);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
