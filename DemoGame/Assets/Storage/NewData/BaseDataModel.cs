using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDataModel
{
   private string keyName;
   private DataTable dataTable;

   public BaseDataModel(string keyName)
   {
      this.keyName = keyName;
      this.init();
   }

   public void init()
   {
      dataTable =  DataCenter.Instance.GetData(keyName);
      // dataTable.
   }
   public void  GetSaveData()
   {
      //index = PlayerPrefs.GetInt(nameof(index),1);
      //name = PlayerPrefs.GetString(nameof(name),"zmj");
      //flag = PlayerPrefs.GetString(nameof(flag)) == "true";
   }

   public void GetValue<T>(T variate)
   {
      
   }
}
