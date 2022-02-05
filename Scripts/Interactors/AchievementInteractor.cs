using System;
using UnityEngine;

public class AchievementInteractor : Interactor
{
    private AchievementRepository repository;
    private CoinsInteractor coinsInteractor;
    private CrystalsInteractor crystalsInteractor;
    private BoxShopInteractor boxShopInteractor;
    private LevelInteractor levelInteractor;
    private PlayerInteractor playerInteractor;


    public override void Initialize()
    {
        repository = Game.GetRepository<AchievementRepository>();
        coinsInteractor = Game.GetInteractor<CoinsInteractor>();
        crystalsInteractor = Game.GetInteractor<CrystalsInteractor>();
        boxShopInteractor = Game.GetInteractor<BoxShopInteractor>();
        levelInteractor = Game.GetInteractor<LevelInteractor>();
        playerInteractor = Game.GetInteractor<PlayerInteractor>();
    }

    public override void OnStart()
    {
        coinsInteractor.AddActionToOnCoinsChangeEvent(OnCoinsChanged);
        boxShopInteractor.AddListenerToOnBuyEvent(OnBuyBox);
        crystalsInteractor.AddListenerToOnCrystalsChangeEvent(OnCrystalsChanged);
        levelInteractor.AddActionToOnLevelEndEvent(OnLevelEnd);
    }

    public void OnLevelEnd(int x)
    {
        if (playerInteractor.Health == playerInteractor.MaxHealth)
        {
            repository.GamesWithMaxHealth++;
            if (!repository.doneAchieves.Contains(4))
            {
                repository.InvokeOnAchieveDoneEvent(4);
                repository.doneAchieves.Add(4);
            }

            if (repository.GamesWithMaxHealth == 5 && !repository.doneAchieves.Contains(5))
            {
                repository.InvokeOnAchieveDoneEvent(5);
                repository.doneAchieves.Add(5);
            }
        }

        if (playerInteractor.Health == 1)
        {
            repository.GamesWithHalfHearh++;
            if (!repository.doneAchieves.Contains(9))
            {
                repository.InvokeOnAchieveDoneEvent(9);
                repository.doneAchieves.Add(9);
            }

            if (repository.GamesWithMaxHealth == 5 && !repository.doneAchieves.Contains(10))
            {
                repository.InvokeOnAchieveDoneEvent(10);
                repository.doneAchieves.Add(10);
            }
        }

        if (levelInteractor.CurrentLvl == 10 && !repository.doneAchieves.Contains(6))
        {
            repository.InvokeOnAchieveDoneEvent(6);
            repository.doneAchieves.Add(6);
        }

        if (levelInteractor.CurrentLvl == 20 && !repository.doneAchieves.Contains(7))
        {
            repository.InvokeOnAchieveDoneEvent(7);
            repository.doneAchieves.Add(7);
        }

        if (levelInteractor.CurrentLvl == 30 && !repository.doneAchieves.Contains(8))
        {
            repository.InvokeOnAchieveDoneEvent(8);
            repository.doneAchieves.Add(8);
        }
    }

    public void OnBuyBox(int x)
    {
        if (boxShopInteractor.BoughtBoxesAmount == boxShopInteractor.BoxesAmount)
        {
            repository.InvokeOnAchieveDoneEvent(3);
            repository.doneAchieves.Add(3);
        }
    }

    public void OnCoinsChanged(int x)
    {
        if (coinsInteractor.TotalCoins >= 10000 && !repository.doneAchieves.Contains(1))
        {
            repository.InvokeOnAchieveDoneEvent(1);
            repository.doneAchieves.Add(1);
        }

        if (coinsInteractor.TotalCoins >= 30000 && !repository.doneAchieves.Contains(2))
        {
            repository.InvokeOnAchieveDoneEvent(2);
            repository.doneAchieves.Add(2);
        }
    }

    public void OnCrystalsChanged(int x)
    {
        if (crystalsInteractor.TotalCrystals >= 5000 && !repository.doneAchieves.Contains(11))
        {
            repository.InvokeOnAchieveDoneEvent(11);
            repository.doneAchieves.Add(11);
        }
    }

    public string GetDescription(int index)
    {
        return repository.descriptions[index];
    }

    public void AddEventToOnAchieveDone(Action<int> action)
    {
        repository.OnAchieveDoneEvent += action;

        foreach (var i in repository.doneAchieves)
            repository.InvokeOnAchieveDoneEvent(i);
    }
}