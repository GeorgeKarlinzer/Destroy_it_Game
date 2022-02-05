using System;
using UnityEngine;

public class PlayerInteractor : Interactor
{
    public int MaxHealth => repository.MaxHealth;
    public int Health
    {
        get => repository.Health;
        set
        {
            if (value <= 0)
                Game.GetInteractor<LevelInteractor>().Lose();

            repository.Health = Mathf.Clamp(value, 0, repository.MaxHealth);
            repository.InvokeOnHealthChangeEvent();
        }
    }
    public int SawDamage
    {
        get => repository.SawDamage;
        set
        {
            repository.SawDamage = value;
            repository.InvokeOnSawDamageChangeEvent();
        }
    }
    public float SpikeFrequency
    {
        get => repository.SpikeFrequency;
        set
        {
            repository.SpikeFrequency = value;
            repository.InvokeOnSpikeFrequencyChangeEvent();
        }
    }
    public float IncomeFactor
    {
        get => repository.IncomeFactor;
        set
        {
            repository.IncomeFactor = value;
            repository.InvokeOnIncomeFactorChangeEvent();
        }
    }

    public Saw Saw => repository.Saw;

    private PlayerRepository repository;
    private LevelInteractor levelInteractor;


    public override void OnCreate()
    {
        repository = Game.GetRepository<PlayerRepository>();
        levelInteractor = Game.GetInteractor<LevelInteractor>();
        int maxHealth = 16;
        levelInteractor.AddActionToOnLevelStartEvent(() => Health = maxHealth);
    }

    public void AddActionToOnHealthChangeEvent(Action<int> action)
    {
        repository.onHealthChangeEvent += action;
    }

    public void AddActionToOnSawDamageChangeEvent(Action<int> action)
    {
        repository.onSawDamageChangeEvent += action;
        SawDamage += 0;
    }

    public void AddActionToOnSpikeFrequencyChangeEvent(Action<float> action)
    {
        repository.onSpikeFrequencyChangeEvent += action;
        SpikeFrequency += 0;
    }

    public void AddActionToOnIncomeFactorChangeEvent(Action<float> action)
    {
        repository.onIncomeFactorChangeEvent += action;
        IncomeFactor += 0;
    }


    public void RemoveActionFromOnHealthChangeEvent(Action<int> action)
    {
        repository.onHealthChangeEvent -= action;
    }

    public void RemoveActionFromOnSawDamageChangeEvent(Action<int> action)
    {
        repository.onSawDamageChangeEvent -= action;
    }

    public void RemoveActionFromOnSpikeFrequencyChangeEvent(Action<float> action)
    {
        repository.onSpikeFrequencyChangeEvent -= action;
    }

    public void RemoveActionFromOnIncomeFactorChangeEvent(Action<float> action)
    {
        repository.onIncomeFactorChangeEvent -= action;
    }
}
