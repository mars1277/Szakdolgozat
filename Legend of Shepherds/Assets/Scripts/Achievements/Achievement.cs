using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement {

    int achievementID;
    string title;
    int numberOfLevels;
    int actualLevel;
    int rewardsGivenNumber;
    string[] descriptions;
    Reward[] rewards;

    public Achievement(int id, string t, int nol, int al, string[] desc, Reward[] r)
    {
        achievementID = id;
        title = t;
        numberOfLevels = nol;
        actualLevel = al;
        descriptions = desc;
        rewards = r;
    }

    public int GetAchivementID()
    {
        return achievementID;
    }

    public string GetTitle()
    {
        return title;
    }

    public int GetNumberOfLevels()
    {
        return numberOfLevels;
    }

    public int GetActualLevel()
    {
        return actualLevel;
    }

    public string[] GetDescriptions()
    {
        return descriptions;
    }

    public string GetDescription(int level)
    {
        return descriptions[level];
    }

    public Reward[] GetRewards()
    {
        return rewards;
    }

    public Reward GetReward(int level)
    {
        return rewards[level];
    }
}
