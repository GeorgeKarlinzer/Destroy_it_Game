using UnityEngine;

public class CubeDetector : MonoBehaviour
{
    private CubesHitsHandler hitsHandler;


    private void Start()
    {
        hitsHandler = Game.GetInteractor<CubesHitsHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out StandartCube cube))
        {
            hitsHandler.HitByDetector(cube);
            return;
        }

        if (collision.TryGetComponent(out BonusCube bonusCube))
            hitsHandler.HitByDetector(bonusCube);
    }
}
