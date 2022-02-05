using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISave
{
    object GetSaveData();

    void Load(object obj);
}
