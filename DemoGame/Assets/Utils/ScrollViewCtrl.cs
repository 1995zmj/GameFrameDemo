using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewCtrl : MonoBehaviour
{
   private ScrollRect _scrollRect;
   private RectTransform _viewport;
   private Transform _content;
   
   private List<ScrollViewItem> _items;
   private ScrollViewItem itemPrefab;
   private float itemH;
   
   private List<int> _data;
   
   private int curMinIndex;
   private int curMaxIndex;
   private int spawnCount;
   private void Awake()
   {
      _items = new List<ScrollViewItem>();
      _data = new List<int>();
      
      _scrollRect = GetComponent<ScrollRect>();
      _content = _scrollRect.content;
      _viewport = _scrollRect.viewport;
      _scrollRect.onValueChanged.AddListener(ScrollRectValueChanged);
   }

   public void init(ScrollViewItem prefab,int ISpawnCount = 0)
   {
      itemPrefab = prefab;
      itemH = itemPrefab.transform.GetComponent<RectTransform>().rect.height;
      var viewH = _viewport.rect.height;
      if (ISpawnCount == 0)
      {
         spawnCount = Convert.ToInt32(Mathf.Floor(viewH / itemH)) * 2 + 1;
      }
      else
      {
         spawnCount = ISpawnCount;
      }

      UpdatePool(spawnCount);
   }

   public void UpdatePool(int spawnCount)
   {
      var count = _items.Count;
      if (spawnCount > _items.Count)
      {
         for (int i = 0; i < spawnCount - count; i++)
         {
            _items.Add(Instantiate(itemPrefab, _content));
         }
      }
   }
   
   // TODO 数据更新
   
   // TODO item之间的间隔
   
   // TODO 直接跳转到
   public void GotoIndex(int index)
   {
      var v = GetPosByIndex(index);
      
   }

   // TODO 还没有统一更新；
   public void UpdateData(List<int> data)
   {
      _data = data;
      _content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,itemH * data.Count);
   
      var l = _viewport.rect.height * 0.5f + _content.localPosition.y;
      var c = Convert.ToInt32(Mathf.Floor(l / itemH));
      var count = spawnCount / 2;
      var minIndex = Mathf.Clamp( c - count, 0, _data.Count - 1);
      var maxindex = Mathf.Clamp(c + count, 0, _data.Count - 1);
      Debug.Log(minIndex + ":" + maxindex);
      UpdateItems(minIndex, maxindex);
   }

   public Vector3 GetPosByIndex(int index)
   {
      var y = index * (itemH) + itemH * 0.5f;
      var x = 0;
      var z = 0;
      return new Vector3(x, -y,z);
   }

   // TODO 还待优化
   public void UpdateItems(int minIndex,int maxIndex)
   {
      curMinIndex = minIndex;
      curMaxIndex = maxIndex;
      
      var f = 0;
      for (int i = minIndex; i <= maxIndex; i++)
      {
         if (f < _items.Count)
         {
            _items[f].gameObject.SetActive(true);
            _items[f].transform.localPosition = GetPosByIndex(i);
            _items[f].UpdateIndex(i);
         }
         f++;
      }

      for (; f < _items.Count; f++)
      {
         _items[f].gameObject.SetActive(false);
      }
   }

   private void ScrollRectValueChanged(Vector2 v)
   {
      Debug.Log(v);
      var y = Mathf.Clamp(v.y, 0f, 1f);
      int count = spawnCount / 2;
      var c =  Convert.ToInt32(((_data.Count - 1) * (1 - v.y)));
      var minIndex = Mathf.Clamp( c - count, 0, _data.Count - 1);
      var maxindex = Mathf.Clamp(c + count, 0, _data.Count - 1);
     
      Debug.Log("Up:" + minIndex + ":" + maxindex);
      UpdateItems(minIndex, maxindex);
   }
      
}
