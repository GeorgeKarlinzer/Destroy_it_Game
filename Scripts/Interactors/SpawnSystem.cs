using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnSystem : Interactor
{
    private readonly Vector3 spawnPosLeft = new Vector3(3.5f, 2.5f, -9.5f);
    private readonly Vector3 spawnPosRight = new Vector3(-3.5f, 2.5f, -9.5f);
    private readonly Vector2 spawnForceFromLeft = new Vector2(-100, 200);
    private readonly Vector2 spawnForceFromRight = new Vector2(100, 200);

    private MonoPool pool;
    private Coroutine spawning;

    /// <summary>
    /// Time between spawning two cubes
    /// </summary>
    private const float DELTA_TIME = 2f;
    /// <summary>
    /// Max percentage of the level difficulty
    /// </summary>
    private const float MAX_CUBE_STRENGTH = 0.1f;
    /// <summary>
    /// Min perventage of the level difficulty
    /// </summary>
    private const float MIN_CUBE_STRENGTH = 0.05f;
    public const int MAX_CUBE_SIZE = 3;
    private const int MIN_CUBE_SIZE = 1;
    /// <summary>
    /// Chance that cube will be bonus
    /// </summary>
    private const float BONUS_CUBE_CHANCE = 0.5f;


    public override void Initialize()
    {
        pool = Game.GetInteractor<MonoPool>();
    }

    public int StartSpawn(int difficulty)
    {
        var cubesToSpawn = new Queue<(int size, int strength)>();

        var currentDifficulty = GenerateRandomCubesValues(cubesToSpawn, difficulty);

        spawning = Coroutines.StartRoutine(SpawnRoutine(cubesToSpawn));

        return currentDifficulty;
    }

    public void StopSpawn()
    {
        Coroutines.StopRoutine(spawning);
    }

    private int GenerateRandomCubesValues(Queue<(int size, int strength)> cubesToSpawn, int difficulty)
    {
        var currentDifficulty = 0;

        while (currentDifficulty < difficulty)
        {
            var cubeSize = Random.Range(MIN_CUBE_SIZE, MAX_CUBE_SIZE + 1);

            var minStrength = (int)Mathf.Ceil(difficulty * MAX_CUBE_STRENGTH / cubeSize);
            var maxStrength = (int)(difficulty * MIN_CUBE_STRENGTH / cubeSize);

            var cubeStrength = Random.Range(minStrength, maxStrength);

            cubesToSpawn.Enqueue((cubeSize, cubeStrength));

            var cubeDifficulty = (int)Mathf.Pow(2, cubeSize) * cubeStrength;

            currentDifficulty += cubeDifficulty;
        }

        return currentDifficulty;
    }

    public StandartCube SpawnCube(int size, int defaultSize, int strength, int defaultStrength, Vector3 position, Vector2 force)
    {
        var cube = pool.PopOrCreate<StandartCube>();

        cube.OnCreate(position, force);

        cube.Initialize(size, defaultSize, strength, defaultStrength);

        return cube;
    }

    private void SpawnBonusCube()
    {
        var randNumber = Random.Range(0, BonusCubeInteractor.BONUS_CUBES_AMOUNT);
        var type = (BonusCubeType)randNumber;

        var (position, force) = GetRandomSpawnPositionAndForce();

        var bonusCube = pool.PopOrCreate<BonusCube>();

        bonusCube.Type = type;
        bonusCube.OnCreate(position, force);
        bonusCube.SetSprite(Game.GetInteractor<BonusCubeInteractor>().GetSprite(type));
    }

    private IEnumerator SpawnRoutine(Queue<(int size, int strength)> cubes)
    {
        while (cubes.Count > 0)
        {
            if (Random.value < BONUS_CUBE_CHANCE)
            {
                SpawnBonusCube();
                yield return new WaitForSeconds(0.5f);
            }

            var (size, strength) = cubes.Dequeue();

            var (position, force) = GetRandomSpawnPositionAndForce();

            SpawnCube(size, size, strength, strength, position, force);

            yield return new WaitForSeconds(DELTA_TIME);
        }
    }

    private (Vector3 position, Vector2 force) GetRandomSpawnPositionAndForce()
    {
        return Random.value > 0.5f ? (spawnPosLeft, spawnForceFromLeft) : (spawnPosRight, spawnForceFromRight);
    }
}
