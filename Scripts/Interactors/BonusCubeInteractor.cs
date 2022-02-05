using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCubeInteractor : Interactor
{
    private Dictionary<BonusCubeType, Action> bonusCubesActionsMap;
    private PlayerInteractor playerInteractor;
    private LevelInteractor levelInteractor;
    private Coroutine freezingRoutine;
    private Coroutine increaseSizeRoutine;

    public const int BONUS_CUBES_AMOUNT = 4;

    private float increasingSizeTime = 5f;
    private float freezingTime = 5f;

    private BonusCubeRepository repository;


    public Sprite GetSprite(BonusCubeType type)
    {
        return repository.spritesMap[type];
    }

    public override void OnCreate()
    {
        bonusCubesActionsMap = new Dictionary<BonusCubeType, Action>();
        playerInteractor = Game.GetInteractor<PlayerInteractor>();
        levelInteractor = Game.GetInteractor<LevelInteractor>();
        repository = Game.GetRepository<BonusCubeRepository>();
    }

    public override void Initialize()
    {
        bonusCubesActionsMap[BonusCubeType.HEALING] = () => playerInteractor.Health += 4;
        bonusCubesActionsMap[BonusCubeType.FREEZING] = () => FreezeTime();
        bonusCubesActionsMap[BonusCubeType.DEATH] = () => levelInteractor.Lose();
        bonusCubesActionsMap[BonusCubeType.SIZEINCREASE] = () => IncreaseSize();
    }

    public void InvokeBonusCubeEvent(BonusCubeType type)
    {
        bonusCubesActionsMap[type]?.Invoke();
    }

    private void FreezeTime()
    {
        if (freezingRoutine != null)
            Coroutines.StopRoutine(freezingRoutine);

        freezingRoutine = Coroutines.StartRoutine(FreezeRoutine());
    }

    private void IncreaseSize()
    {
        if (increaseSizeRoutine != null)
            Coroutines.StopRoutine(increaseSizeRoutine);

        increaseSizeRoutine = Coroutines.StartRoutine(IncreaseSizeRoutine());
    }

    private IEnumerator FreezeRoutine()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(freezingTime);
        Time.timeScale = 1f;
    }

    private IEnumerator IncreaseSizeRoutine()
    {
        playerInteractor.Saw.transform.localScale = new Vector3(2, 2, 1);
        yield return new WaitForSeconds(increasingSizeTime);
        playerInteractor.Saw.transform.localScale = new Vector3(1, 1, 1);
    }
}
