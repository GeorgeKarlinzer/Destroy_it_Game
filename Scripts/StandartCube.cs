using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StandartCube : BaseCube
{
    public int DefaultSize { get; set; }
    private int size;
    public int Size
    {
        get => size;
        set
        {
            size = value;
            var newScale = Vector3.one / Mathf.Pow(2, (SpawnSystem.MAX_CUBE_SIZE - size) / 2f);
            this.transform.localScale = newScale;
        }
    }
    public int DefaultStrength { get; set; }
    public Vector2 Velocity { get => rigidbody.velocity; set => rigidbody.velocity = value; }
    private int strength;
    public int Strength 
    {
        get => strength;
        set
        {
            strength = value;
            strengthText.text = size > 0 ? value.ToString() : "";
        }
    }

    [SerializeField] private TextMeshPro strengthText;



    public void AddForce(Vector2 force, ForceMode2D mode)
    {
        rigidbody.AddForce(force, mode);
    }

    public void Initialize(int size, int defalutSize, int strength, int defaultStrength)
    {
        Size = size;
        DefaultSize = defalutSize;
        Strength = strength;
        DefaultStrength = defaultStrength;
    }

    public override void OnCreate(Vector3 position, Vector2 force)
    {
        base.OnCreate(position, force);
        renderer.sprite = Game.GetInteractor<BoxShopInteractor>().GetCurrentSprite();
    }
}
