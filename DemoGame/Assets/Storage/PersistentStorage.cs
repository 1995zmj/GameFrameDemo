using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class PersistentStorage
{
    public string savePath;

    public void Save () {
        using (
            var writer = new BinaryWriter(File.Open(savePath, FileMode.Create))
        )
        {
            writer.Write(1);
        }
    }

    public void Load () {
        using (
            var reader = new BinaryReader(File.Open(savePath, FileMode.Open))
        )
        {
            var k = reader.ReadInt32();
            Debug.LogError(k);
        }
    }
    
    public void Claer () {
        File.Delete(savePath);
    }

}