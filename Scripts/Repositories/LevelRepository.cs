using System;
using UnityEngine;

public class LevelRepository : Repository
{
    public int CurrentLvl { get; set; }
    /// <summary>
    /// Default difficulty of next lvl
    /// </summary>
    public int LvlDifficulty => (int)(Mathf.Pow(DIFFICULTY_FACTOR, CurrentLvl + 1) * INITIAL_DIFFICULTY);

    private const int INITIAL_DIFFICULTY = 100;
    public const float DIFFICULTY_FACTOR = 1.1f;

    public LevelProgress levelProgress;
    /// <summary>
    /// Return current and default difficulty
    /// </summary>
    public event Action<int, int> OnCurrentDifficultyChangeEvent;
    public event Action OnLoseEvent;
    /// <summary>
    /// Return score of the lvl
    /// </summary>
    public event Action<int> OnLevelEndEvent;
    public event Action OnLevelStartEvent;



    public void InvokeOnLevelStartEvent()
    {
        OnLevelStartEvent?.Invoke();
    }

    public void InvokeLoseLevelEvent()
    {
        OnLoseEvent?.Invoke();
    }

    public void InvokeOnLevelEndEvent()
    {
        OnLevelEndEvent?.Invoke(levelProgress.DefaultDifficulty - levelProgress.CurrentDifficulty);
    }

    public void InvokeOnCurrentDifficultyChangeEvent()
    {
        OnCurrentDifficultyChangeEvent?.Invoke(levelProgress.CurrentDifficulty, levelProgress.DefaultDifficulty);
    }

    public override void OnCreate()
    {
        CurrentLvl = 0;

        var type = typeof(LevelRepository);
        SaveSystem.AddISave(type, this);
    }

    public override void Initialize()
    {
        levelProgress = new LevelProgress();
    }

    public override object GetSaveData()
    {
        return CurrentLvl;
    }

    public override void Load(object obj)
    {
        CurrentLvl = (int)obj;
    }
}
