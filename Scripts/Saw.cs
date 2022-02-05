using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private CubesHitsHandler hitsHandler;

    private float leftBorder;
    private float rightBorder;

    private bool canMove;
    public bool CanMove => canMove;

    private Camera main;


    private void Start()
    {
        hitsHandler = Game.GetInteractor<CubesHitsHandler>();
        Game.GetInteractor<LevelInteractor>().AddActionToOnLevelStartEvent(() => { canMove = true; animator.SetBool("IsGame", true); });
        Game.GetInteractor<LevelInteractor>().AddActionToOnLevelEndEvent(x => { canMove = false; animator.SetBool("IsGame", false); });

        this.main = Camera.main;

        var pixelWidth = main.pixelWidth;

        var leftBorderInPixels = new Vector2(0, 0);
        var rightBorderInPixels = new Vector2(pixelWidth, 0);

        var sawRadius = 0.5f;

        leftBorder = main.ScreenToWorldPoint(leftBorderInPixels).x + sawRadius;
        rightBorder = main.ScreenToWorldPoint(rightBorderInPixels).x - sawRadius;
    }

    void Update()
    {
        if (canMove && !PauseSystem.IsPause)
        {
            Vector3 newSawPos;

            newSawPos.x = main.ScreenToWorldPoint(Input.mousePosition).x;
            newSawPos.x = Mathf.Clamp(newSawPos.x, this.leftBorder, this.rightBorder);

            newSawPos.y = transform.position.y;

            newSawPos.z = transform.position.z;

            transform.position = newSawPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canMove)
            return;

        if (collision.TryGetComponent(out StandartCube cube))
        {
            if (cube.Velocity.y < 0 && cube.Size > 0)
                hitsHandler.HitBySaw(cube);

            return;
        }

        if (collision.TryGetComponent(out BonusCube bonusCube))
            hitsHandler.HitBySaw(bonusCube);
    }
}
