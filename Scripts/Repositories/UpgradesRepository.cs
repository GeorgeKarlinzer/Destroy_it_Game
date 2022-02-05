using System;

public class UpgradesRepository : Repository
{
    public const int SawDamagePerLvl = 1;
    public const float SpikesSpeedPerLvl = 0.1f;
    public const float IncomeFactorPerLvl = 0.1f;

    public int SawPrice { get; set; }
    public int SpikesPrice { get; set; }
    public int IncomePrice { get; set; }

    public const float PRICE_FACTOR = 1.1f;

    public event Action<int> OnSawPriceChanged;
    public event Action<int> OnSpikesPriceChanged;
    public event Action<int> OnIncomePriceChanged;


    public override void OnCreate()
    {
        SawPrice = 10;
        SpikesPrice = 10;
        IncomePrice = 10;

        var type = typeof(UpgradesRepository);
        SaveSystem.AddISave(type, this);
    }

    public void InvokeOnSawPriceChangedEvent()
    {
        OnSawPriceChanged?.Invoke(SawPrice);
    }

    public void InvokeOnSpikesPriceChangedEvent()
    {
        OnSpikesPriceChanged?.Invoke(SpikesPrice);
    }

    public void InvokeOnIncomePriceChangedEvent()
    {
        OnIncomePriceChanged?.Invoke(IncomePrice);
    }

    public override void Load(object obj)
    {
        var (sawPrice, spikePrice, incomePrice) = ((int, int, int))obj;

        SawPrice = sawPrice;
        SpikesPrice = spikePrice;
        IncomePrice = incomePrice;
    }

    public override object GetSaveData()
    {
        return (SawPrice, SpikesPrice, IncomePrice);
    }
}
