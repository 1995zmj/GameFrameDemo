using System;
using System.Collections;
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

   public float spacing;
   //数据
   private List<int> _data;
   //生成的个数
   private int spawnCount;
   private float mid;
   
   private void Awake()
   {
      _items = new List<ScrollViewItem>();
      _data = new List<int>();
      
      _scrollRect = GetComponent<ScrollRect>();
      _content = _scrollRect.content;
      _viewport = _scrollRect.viewport;
      _scrollRect.onValueChanged.AddListener(ScrollRectValueChanged);
   }

   public void init(ScrollViewItem prefab)
   {
      itemPrefab = prefab;
      itemH = itemPrefab.transform.GetComponent<RectTransform>().rect.height;
      var viewH = _viewport.rect.height;
      spawnCount = Convert.ToInt32(Mathf.Floor(viewH / itemH)) * 2 + 1;
   
      UpdatePool(spawnCount);
   }

   private void UpdatePool(int spawnCount)
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

   private float GetContentSize()
   {
      var y = itemH * _data.Count;
      var sp = (_data.Count - 1) * spacing;
      return y + sp;
   }
   
   public void GotoIndex(int index)
   {
      float v = index * 1.0f / _data.Count;
      _scrollRect.verticalNormalizedPosition =  1 -v;
      var c = _scrollRect.verticalNormalizedPosition;
      UpdateItems(1 - c);
   }
   
   // TODO 定位不一定适应任何情况
   public void UpdateData(List<int> data)
   {
      _data = data;
      _content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, GetContentSize());
      mid = GetDataange() * 0.5f;
      var c = _scrollRect.verticalNormalizedPosition;
      UpdateItems(1 - c,true);
   }

   private int GetDataange()
   {
      var index = 0;
      index = _data.Count - spawnCount;
      if (index < 0)
         index = 0;
      return index;
   }

   private Vector3 GetPosByIndex(int index)
   {
      var y = index * (itemH) + itemH * 0.5f + spacing * index;
      var x = 0;
      var z = 0;
      return new Vector3(x, -y,z);
   }

   private void UpdateItems(float y,bool all = false)
   {
      var t = Mathf.Lerp(0, GetDataange(),  y);
      t = t < mid ? Mathf.Floor(t) : Mathf.Ceil(t);
      int index = Convert.ToInt32(t) ;
      //Debug.Log(index + ":" + (index + spawnCount - 1));
      for (int i = index; i <= index + spawnCount - 1; i++)
      {
         if (_items[i % spawnCount].DataIndex != i || all)
         {
            if (i >= _data.Count)
            {
               _items[i % spawnCount].gameObject.SetActive(false);
            }
            else
            {
               _items[i % spawnCount].gameObject.SetActive(true);
               _items[i % spawnCount].transform.localPosition = GetPosByIndex(i);
               _items[i % spawnCount].UpdateIndex(i);
            }
         }
      }
   }

   private void ScrollRectValueChanged(Vector2 v)
   {
      var y = Mathf.Clamp(v.y, 0f, 1f);
      y = 1 - y;
      UpdateItems(y);
   }
      
}
