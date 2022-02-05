using System;

public class UpgradesInteractor : Interactor
{
    public int SawPrice
    {
        get => repository.SawPrice;
        set
        {
            repository.SawPrice = value;
            repository.InvokeOnSawPriceChangedEvent();
        }
    }
    public int SpikesPrice
    {
        get => repository.SpikesPrice;
        set
        {
            repository.SpikesPrice = value;
            repository.InvokeOnSpikesPriceChangedEvent();
        }
    }
    public int IncomePrice
    {
        get => repository.IncomePrice;
        set
        {
            repository.IncomePrice = value;
            repository.InvokeOnIncomePriceChangedEvent();
        }
    }

    private UpgradesRepository repository;


    public override void OnCreate()
    {
        repository = Game.GetRepository<UpgradesRepository>();
    }

    public void AddActionToOnSawPricesChangedEvent(Action<int> action)
    {
        repository.OnSawPriceChanged += action;
        SawPrice += 0;
    }

    public void AddActionToOnSpikesPricesChangedEvent(Action<int> action)
    {
        repository.OnSpikesPriceChanged += action;
        SpikesPrice += 0;
    }
    
    public void AddActionToOnIncomePricesChangedEvent(Action<int> action)
    {
        repository.OnIncomePriceChanged += action;
        IncomePrice += 0;
    }
}
