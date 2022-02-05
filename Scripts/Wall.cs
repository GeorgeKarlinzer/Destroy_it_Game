using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private bool isLeft;
    private CubesHitsHandler hitsHandler;




    private void Start()
    {
        this.hitsHandler = Game.GetInteractor<CubesHitsHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out StandartCube cube))
            // If cube is going from outside of sceren then cube have not to bounce from the wall
            if (isLeft ^ cube.Velocity.x > 0)
                hitsHandler.HitByWall(cube, isLeft);
    }
}
