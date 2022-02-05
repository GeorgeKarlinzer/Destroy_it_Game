using UnityEngine;

public class BonusCube : BaseCube
{
    public BonusCubeType Type { get; set; }


    public void SetSprite(Sprite sprite)
    {
        renderer.sprite = sprite;
    }
}
