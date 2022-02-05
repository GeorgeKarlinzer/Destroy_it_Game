using System;
using System.Collections.Generic;
using UnityEngine;

public class BoxShopRepository : Repository
{
    public List<Sprite> boxSprites;
    public int CurrentBox { get; set; }
    public List<int> boughtBoxes;
    public List<int> prices;
    public event Action<int> OnBuyBoxEvent;


    public override void OnCreate()
    {
        boughtBoxes = new List<int>() { 0 };
        prices = new List<int>{ 0, 20, 30, 50, 70, 90, 120, 150, 180, 220, 260 };

        boxSprites = new List<Sprite>();
        for (int i = 0; i < prices.Count; i++)
            boxSprites.Add(Resources.Load<Sprite>($"Boxes/BoxSprite{i}"));
        CurrentBox = 0;

        var type = typeof(BoxShopRepository);
        SaveSystem.AddISave(type, this);
    }

    public void InvokeOnBuyBoxEvent(int index)
    {
        OnBuyBoxEvent?.Invoke(index);
    }

    public override void OnStart()
    {
        boughtBoxes.ForEach(x => InvokeOnBuyBoxEvent(x));
    }

    public override object GetSaveData()
    {
        return (new List<int>(boughtBoxes), CurrentBox);
    }

    public override void Load(object obj)
    {
        var (boughtBoxes, currentSrite) = ((List<int>, int))obj;
        (this.boughtBoxes, CurrentBox) = (new List<int>(boughtBoxes), currentSrite);
    }
}
