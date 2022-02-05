using System;
using System.Collections.Generic;

public class AchievementRepository : Repository
{
    public string[] descriptions;

    public List<int> doneAchieves;

    public event Action<int> OnAchieveDoneEvent;

    public int GamesWithMaxHealth { get; set; }
    public int GamesWithHalfHearh { get; set; }


    public override void OnCreate()
    {
        doneAchieves = new List<int>() { 0 };

        SaveSystem.AddISave(typeof(AchievementRepository), this);
    }

    public override void OnStart()
    {
        descriptions = new string[]
        {
            "Nothing",
            "Earn 10000 coins",
            "Earn 30000 coins",
            "Open all boxes",
            "Finish one level with a full health",
            "Finish five levels with a full health",
            "Open 10 lvl",
            "Open 30 lvl",
            "Open 50 lvl",
            "Finish one level with half of heart",
            "Finish five levels with half of heart",
            "Earn 5000 crystals",
        };
    }

    public void InvokeOnAchieveDoneEvent(int index)
    {
        OnAchieveDoneEvent?.Invoke(index);
    }

    public override object GetSaveData()
    {
        return (doneAchieves, GamesWithMaxHealth, GamesWithHalfHearh);
    }

    public override void Load(object obj)
    {
        var (doneAchieves, gamesWithMaxHealth, gamesWithHalfHearh) = ((List<int>, int, int))(obj);
        (this.doneAchieves, GamesWithMaxHealth, GamesWithHalfHearh) =
            (new List<int>(doneAchieves), gamesWithMaxHealth, gamesWithHalfHearh);
    }
}
