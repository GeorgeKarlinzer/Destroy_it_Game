using System;
using UnityEngine;

public class BoxShopInteractor : Interactor
{
    public int CurrentBox => repository.CurrentBox;

    public int BoughtBoxesAmount => repository.boughtBoxes.Count;

    public int BoxesAmount => repository.boxSprites.Count;

    private BoxShopRepository repository;


    public override void OnCreate()
    {
        repository = Game.GetRepository<BoxShopRepository>();
    }

    public void AddListenerToOnBuyEvent(Action<int> action)
    {
        repository.OnBuyBoxEvent += action;

        repository.boughtBoxes.ForEach(x => action(x));
    }

    public void BuyBox(int index)
    {
        Game.GetInteractor<CrystalsInteractor>().Crystals -= repository.prices[index];

        repository.boughtBoxes.Add(index);

        repository.InvokeOnBuyBoxEvent(index);
    }

    internal int GetPrice(int index)
    {
        return repository.prices[index];
    }

    public void SetBox(int index)
    {
        repository.CurrentBox = index;
    }

    public Sprite GetCurrentSprite()
    {
        return repository.boxSprites[CurrentBox];
    }
}
