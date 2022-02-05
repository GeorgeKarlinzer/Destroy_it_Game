using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Repository : ISave
{
    public abstract void OnCreate();

    public virtual void Initialize() { }
    
    public virtual void OnStart() { }

    public abstract void Load(object obj);

    public abstract object GetSaveData();
}
