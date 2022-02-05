using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsRepository : Repository
{
    public int TotalCrystal { get; set; }
    public int Crystals { get; set; }
    
    public event Action<int> onCrystalsChangeEvent;


    public override void OnCreate()
    {
        Crystals = 1000;
        TotalCrystal = Crystals;

        var type = typeof(CrystalsRepository);
        SaveSystem.AddISave(type, this);
    }

    public override void OnStart()
    {
        InvokeOnCrystalChangeEvent();
    }

    public override object GetSaveData()
    {
        return (Crystals, TotalCrystal);
    }

    public void InvokeOnCrystalChangeEvent()
    {
        onCrystalsChangeEvent?.Invoke(this.Crystals);
    }

    public override void Load(object obj)
    {
        var (crystals, totalCrystals) = ((int, int))obj;

        (Crystals, TotalCrystal) = (crystals, totalCrystals);
    }
}
