using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerRepository : Repository, ISave
{
    public Saw Saw { get; set; }
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public int SawDamage { get; set; }
    public float SpikeFrequency { get; set; }
    public float IncomeFactor { get; set; }

    public event Action<int> onHealthChangeEvent;
    public event Action<int> onSawDamageChangeEvent;
    public event Action<float> onSpikeFrequencyChangeEvent;
    public event Action<float> onIncomeFactorChangeEvent;


    public void InvokeOnHealthChangeEvent()
    {
        onHealthChangeEvent?.Invoke(Health);
    }

    public void InvokeOnSawDamageChangeEvent()
    {
        onSawDamageChangeEvent?.Invoke(SawDamage);
    }

    public void InvokeOnSpikeFrequencyChangeEvent()
    {
        onSpikeFrequencyChangeEvent?.Invoke(SpikeFrequency);
    }

    public void InvokeOnIncomeFactorChangeEvent()
    {
        onIncomeFactorChangeEvent?.Invoke(IncomeFactor);
    }


    public override void OnCreate()
    {
        MaxHealth = 16;
        Health = MaxHealth;
        SawDamage = 1;
        SpikeFrequency = 2f;
        IncomeFactor = 1f;

        var type = typeof(PlayerRepository);
        SaveSystem.AddISave(type, (this));
    }

    public override void Initialize()
    {
        var sawPrefab = Resources.Load<Saw>("Saw");
        Saw = Object.Instantiate(sawPrefab);
    }

    public override object GetSaveData()
    {
        return (Health, SawDamage, SpikeFrequency, IncomeFactor);
    }

    public override void Load(object obj)
    {
        var (health, sawDamage, spikeFrequency, incomeFactor) = ((int, int, float, float))obj;

        Health = health;
        SawDamage = sawDamage;
        SpikeFrequency = spikeFrequency;
        IncomeFactor = incomeFactor;
    }
}
