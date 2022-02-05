using System;

public class LevelInteractor : Interactor
{
    private SpawnSystem spawnSystem;
    public int CurrentLvl => repository.CurrentLvl;
    public int CurrentDifficulty => repository.LvlDifficulty;

    public bool IsGame => Game.GetInteractor<PlayerInteractor>().Saw.CanMove;

    private LevelRepository repository;


    public void FinishLevel()
    {
        repository.CurrentLvl += 1;
        repository.InvokeOnLevelEndEvent();
    }

    public void Lose()
    {
        spawnSystem.StopSpawn();
        repository.InvokeLoseLevelEvent();
        repository.CurrentLvl -= 1;
        FinishLevel();
    }

    public void StartLevel()
    {
        var currentDifficulty = spawnSystem.StartSpawn(CurrentDifficulty);

        repository.InvokeOnLevelStartEvent();

        repository.levelProgress.CurrentDifficulty = currentDifficulty;
        repository.levelProgress.DefaultDifficulty = currentDifficulty;
    }

    public void ChangeCurrentDifficulty(int deltaValue)
    {
        repository.levelProgress.CurrentDifficulty += deltaValue;
        repository.InvokeOnCurrentDifficultyChangeEvent();

        if(repository.levelProgress.CurrentDifficulty == 0)
            FinishLevel();
    }

    public void AddActionToOnLoseEvent(Action action)
    {
        repository.OnLoseEvent += action;
    }

    public void AddActionToOnLevelStartEvent(Action action)
    {
        repository.OnLevelStartEvent += action;
    }

    public void AddActionToOnLevelEndEvent(Action<int> action)
    {
        repository.OnLevelEndEvent += action;
    }

    public void AddActionToOnCurrentDifficultyChangeEvent(Action<int, int> action)
    {
        repository.OnCurrentDifficultyChangeEvent += action;
    }

    public void RemoveActionFromOnCurrentDifficultyChangeEvent(Action<int, int> action)
    {
        repository.OnCurrentDifficultyChangeEvent -= action;
    }

    public override void OnCreate()
    {
        repository = Game.GetRepository<LevelRepository>();
        spawnSystem = Game.GetInteractor<SpawnSystem>();
    }

    public void IncreaseLvl()
    {
        repository.CurrentLvl++;
    }
}
