using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesHitsHandler : Interactor
{
    private LevelInteractor levelInteractor;
    private SpawnSystem spawnSystem;
    private BonusCubeInteractor bonusCubeInteractor;
    private PlayerInteractor playerInteractor;
    private MonoPool pool;

    private readonly Vector2 THROW_FORCE = new Vector2(0, 10);
    private const float MIN_X_VELOCITY = -5f;
    private const float MAX_X_VELOCITY = 5f;



    public override void Initialize()
    {
        bonusCubeInteractor = Game.GetInteractor<BonusCubeInteractor>();
        playerInteractor = Game.GetInteractor<PlayerInteractor>();
        pool = Game.GetInteractor<MonoPool>();
        levelInteractor = Game.GetInteractor<LevelInteractor>();
        spawnSystem = Game.GetInteractor<SpawnSystem>();
    }

    public void HitByDetector(BonusCube cube)
    {
        pool.Push(cube);
    }

    public void HitByDetector(StandartCube cube)
    {
        if (!levelInteractor.IsGame)
            return;

        var cubeTotalStrength = (int)Mathf.Pow(2, cube.Size) * cube.DefaultStrength;
        playerInteractor.Health -= cube.Size * 2;
        levelInteractor.ChangeCurrentDifficulty(-cubeTotalStrength);
        pool.Push(cube);
    }

    public void HitByWall(StandartCube cube, bool isLeft)
    {
        var newVelocity = cube.Velocity;
        newVelocity.x = -newVelocity.x;
        cube.Velocity = newVelocity;
    }

    public void HitBySaw(BonusCube cube)
    {
        bonusCubeInteractor.InvokeBonusCubeEvent(cube.Type);
        pool.Push(cube);
    }

    public void HitBySaw(StandartCube cube)
    {
        cube.Strength -= playerInteractor.SawDamage;
        if (cube.Strength <= 0)
        {
            var newCube = DivideCube(cube);
            SetRandomXVelocity(newCube);

            if (cube.Size == 0)
                return;

            ThrowUpCube(newCube);
        }

        SetRandomXVelocity(cube);
        ThrowUpCube(cube);
    }

    public void HitBySpike(StandartCube cube)
    {
        cube.Strength--;
        if (cube.Strength <= 0)
            DivideCube(cube);
    }

    private StandartCube DivideCube(StandartCube cube)
    {
        cube.Size--;

        var newStrength = Mathf.RoundToInt(cube.DefaultStrength / Mathf.Pow(2, cube.Size));
        var pos = cube.transform.position;
        var newVelocity = cube.Velocity;

        var newCube = spawnSystem.SpawnCube(cube.Size, cube.DefaultSize, newStrength, cube.DefaultStrength, pos, Vector2.zero);
        cube.Strength = newStrength;

        newVelocity.x = -newVelocity.x;
        newCube.Velocity = newVelocity;

        return newCube;
    }

    private void SetRandomXVelocity(StandartCube cube)
    {
        var newVelocity = cube.Velocity;
        var randX = UnityEngine.Random.Range(MIN_X_VELOCITY, MAX_X_VELOCITY);
        newVelocity.x = randX;
        cube.Velocity = newVelocity;
    }

    private void ThrowUpCube(StandartCube cube)
    {
        var newVelocity = cube.Velocity;
        newVelocity.y = 0;
        cube.Velocity = newVelocity;

        cube.AddForce(THROW_FORCE, ForceMode2D.Impulse);
    }
}
