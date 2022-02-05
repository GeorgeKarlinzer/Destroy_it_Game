using System;
using UnityEngine;

public class CrystalsInteractor : Interactor
{
    public int TotalCrystals => repository.TotalCrystal;
    public int Crystals
    {
        get => repository.Crystals;
        set
        {
            var delta = value - repository.Crystals;
            repository.TotalCrystal += Mathf.Clamp(delta, 0, int.MaxValue);
            repository.Crystals = value;
            repository.InvokeOnCrystalChangeEvent();
        }
    }

    private CrystalsRepository repository;


    public override void OnCreate()
    {
        repository = Game.GetRepository<CrystalsRepository>();    
    }

    public bool IsEnoughCoins(int value) => Crystals >= value;

    public void AddListenerToOnCrystalsChangeEvent(Action<int> action)
    {
        repository.onCrystalsChangeEvent += action;
        Crystals += 0;
    }

    public void RemoveActionFromOnCrystalsChangeEvent(Action<int> action)
    {
        repository.onCrystalsChangeEvent -= action;
    }
}
