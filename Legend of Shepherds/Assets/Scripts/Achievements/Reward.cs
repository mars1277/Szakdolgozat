using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward {

    public enum RewardType { Money, FairyShard };
    RewardType rewardType;
    int rewardAmount;

    public Reward(RewardType rt, int ra)
    {
        rewardType = rt;
        rewardAmount = ra;
    }

    public RewardType GetRewardType()
    {
        return rewardType;
    }

    public int GetRewardAmount()
    {
        return rewardAmount;
    }
}
