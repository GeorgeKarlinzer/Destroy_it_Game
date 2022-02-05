using System;
using UnityEngine;

public class CoinsInteractor : Interactor
{
    public int TotalCoins => repository.TotalCoins;
    public int Coins
    {
        get => repository.Coins;
        set
        {
            var delta = value - repository.Coins;
            repository.TotalCoins += Mathf.Clamp(delta, 0, int.MaxValue);
            repository.Coins = value;
            repository.InvokeOnCoinsChangeEvent();
        }
    }

    private CoinsRepository repository;


    public override void OnCreate()
    {
        repository = Game.GetRepository<CoinsRepository>();
    }

    public override void OnStart()
    {
        Game.GetInteractor<LevelInteractor>().AddActionToOnLevelEndEvent(x => Coins += x);
    }

    public void AddActionToOnCoinsChangeEvent(Action<int> action)
    {
        repository.OnCoinsChangeEvent += action;
        Coins += 0;
    }

    public void RemoveActionFromOnCoinsChangeEvent(Action<int> action)
    {
        repository.OnCoinsChangeEvent -= action;
    }

    public bool IsEnoughCoins(int value) => repository.Coins >= value;
}
