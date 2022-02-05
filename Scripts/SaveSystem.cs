using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveSystem : Interactor
{
    private static Dictionary<Type, ISave> iSavesMap = new Dictionary<Type, ISave>();
    private Dictionary<Type, object> saveDataMap;

    string path;

    public override void Initialize()
    {
        saveDataMap = new Dictionary<Type, object>();

        path = Application.persistentDataPath + "/save";

        if (File.Exists(path))
        {
            saveDataMap = SaveAndLoad.LoadEncrypted<Dictionary<Type, object>>(path);

            foreach (var key in iSavesMap.Keys)
                if (saveDataMap.ContainsKey(key))
                    iSavesMap[key].Load(saveDataMap[key]);
        }
    }

    public object GetLoad(Type type)
    {
        return saveDataMap.ContainsKey(type) ? saveDataMap[type] : default;
    }

    public static void AddISave(Type type, ISave save)
    {
        iSavesMap[type] = save;
    }
    
    public void SaveAll()
    {
        saveDataMap.Clear();
        iSavesMap.Keys.ToList().ForEach(key => saveDataMap.Add(key, iSavesMap[key].GetSaveData()));

        SaveAndLoad.SaveEncrypted(saveDataMap, path);
    }
}
