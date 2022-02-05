using System;
using UnityEngine;

public class CoinsRepository : Repository
{
    public int TotalCoins { get; set; }
    //public int Coins { get; set; }
    private int coins;
    public int Coins
    {
        get => coins ^ salt;
        set => coins = value ^ salt;
    }
    private readonly int salt = new System.Random().Next(int.MinValue, int.MaxValue);

    public event Action<int> OnCoinsChangeEvent;


    public override void OnCreate()
    {
        Coins = 100;
        TotalCoins = Coins;

        var type = typeof(CoinsRepository);
        SaveSystem.AddISave(type, this);
    }

    public void InvokeOnCoinsChangeEvent()
    {
        OnCoinsChangeEvent?.Invoke(Coins);
    }

    public override void Load(object obj)
    {
        var (coins, totalCoins) = ((int, int))obj;
        (Coins, TotalCoins)  = (coins, totalCoins);
    }

    public override object GetSaveData()
    {
        return (Coins, TotalCoins);
    }
}
