using System.Collections.Generic;
using UnityEngine;

public class BonusCubeRepository : Repository
{
    public Dictionary<BonusCubeType, Sprite> spritesMap;


    public override void Initialize()
    {
        spritesMap = new Dictionary<BonusCubeType, Sprite>();
        spritesMap.Add(BonusCubeType.DEATH, Resources.Load<Sprite>($"BonusCubes/CubeDeath"));
        spritesMap.Add(BonusCubeType.FREEZING, Resources.Load<Sprite>($"BonusCubes/CubeFreezing"));
        spritesMap.Add(BonusCubeType.HEALING, Resources.Load<Sprite>($"BonusCubes/CubeHealing"));
        spritesMap.Add(BonusCubeType.SIZEINCREASE, Resources.Load<Sprite>($"BonusCubes/CubeSizeIncrease"));
    }

    public override object GetSaveData()
    {
        throw new System.NotImplementedException();
    }

    public override void Load(object obj)
    {
        throw new System.NotImplementedException();
    }

    public override void OnCreate() { }
}
