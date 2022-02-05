using System.Collections.Generic;
using UnityEngine;

public class SawShopRepository : Repository
{
    public List<Sprite> sawSprites;
    public int CurrentIndex;
    public float[] progress;


    public override void OnCreate()
    {
        CurrentIndex = 0;

        SaveSystem.AddISave(typeof(SawShopInteractor), this);
    }

    public override void Initialize()
    {
        sawSprites = new List<Sprite>();
        for (int i = 0; i < 12; i++)
            sawSprites.Add(Resources.Load<Sprite>($"Saws/SawSprite{i}"));
    }

    public override object GetSaveData()
    {
        return (CurrentIndex, progress);
    }

    public override void Load(object obj)
    {
        (CurrentIndex, progress) = ((int, float[]))obj;
    }
}
