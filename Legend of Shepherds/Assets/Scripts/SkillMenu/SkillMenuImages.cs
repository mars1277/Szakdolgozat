using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillMenuImages : MonoBehaviour {

    public Sprite firemage_Skill_1_1_Unchoosed;
    public Sprite firemage_Skill_1_1_Choosed;
    public Sprite firemage_Skill_1_2_Unchoosed;
    public Sprite firemage_Skill_1_2_Choosed;
    public Sprite firemage_Skill_2_1_Unchoosed;
    public Sprite firemage_Skill_2_1_Choosed;
    public Sprite firemage_Skill_2_2_Unchoosed;
    public Sprite firemage_Skill_2_2_Choosed;
    public Sprite firemage_Skill_3_1_Unchoosed;
    public Sprite firemage_Skill_3_1_Choosed;
    public Sprite firemage_Skill_3_2_Unchoosed;
    public Sprite firemage_Skill_3_2_Choosed;
    public Sprite firemage_Skill_4_1_Unchoosed;
    public Sprite firemage_Skill_4_1_Choosed;
    public Sprite firemage_Skill_4_2_Unchoosed;
    public Sprite firemage_Skill_4_2_Choosed;

    public Sprite frostmage_Skill_1_1_Unchoosed;
    public Sprite frostmage_Skill_1_1_Choosed;
    public Sprite frostmage_Skill_1_2_Unchoosed;
    public Sprite frostmage_Skill_1_2_Choosed;
    public Sprite frostmage_Skill_2_1_Unchoosed;
    public Sprite frostmage_Skill_2_1_Choosed;
    public Sprite frostmage_Skill_2_2_Unchoosed;
    public Sprite frostmage_Skill_2_2_Choosed;
    public Sprite frostmage_Skill_3_1_Unchoosed;
    public Sprite frostmage_Skill_3_1_Choosed;
    public Sprite frostmage_Skill_3_2_Unchoosed;
    public Sprite frostmage_Skill_3_2_Choosed;
    public Sprite frostmage_Skill_4_1_Unchoosed;
    public Sprite frostmage_Skill_4_1_Choosed;
    public Sprite frostmage_Skill_4_2_Unchoosed;
    public Sprite frostmage_Skill_4_2_Choosed;

    public Sprite GetSkillSprite(int skillLevel, int skillNumber, bool choosed, string character)
    {
        if(character.Equals("FireMage"))
        {
            return GetFireMageSkillSprite(skillLevel, skillNumber, choosed);
        }

        if (character.Equals("FrostMage"))
        {
            return GetFrostMageSkillSprite(skillLevel, skillNumber, choosed);
        }

        return firemage_Skill_1_1_Unchoosed;
    }

    public Sprite GetFireMageSkillSprite(int skillLevel, int skillNumber, bool choosed)
    {
        switch (skillLevel)
        {
            case 1:
                if (skillNumber == 1)
                    if (choosed)
                        return firemage_Skill_1_1_Choosed;
                    else
                        return firemage_Skill_1_1_Unchoosed;
                else
                    if (choosed)
                    return firemage_Skill_1_2_Choosed;
                else
                    return firemage_Skill_1_2_Unchoosed;
            case 2:
                if (skillNumber == 1)
                    if (choosed)
                        return firemage_Skill_2_1_Choosed;
                    else
                        return firemage_Skill_2_1_Unchoosed;
                else
                    if (choosed)
                    return firemage_Skill_2_2_Choosed;
                else
                    return firemage_Skill_2_2_Unchoosed;
            case 3:
                if (skillNumber == 1)
                    if (choosed)
                        return firemage_Skill_3_1_Choosed;
                    else
                        return firemage_Skill_3_1_Unchoosed;
                else
                    if (choosed)
                    return firemage_Skill_3_2_Choosed;
                else
                    return firemage_Skill_3_2_Unchoosed;
            case 4:
                if (skillNumber == 1)
                    if (choosed)
                        return firemage_Skill_4_1_Choosed;
                    else
                        return firemage_Skill_4_1_Unchoosed;
                else
                    if (choosed)
                    return firemage_Skill_4_2_Choosed;
                else
                    return firemage_Skill_4_2_Unchoosed;
            default:
                break;
        }
        return firemage_Skill_1_1_Unchoosed;
    }

    public Sprite GetFrostMageSkillSprite(int skillLevel, int skillNumber, bool choosed)
    {
        switch (skillLevel)
        {
            case 1:
                if (skillNumber == 1)
                    if (choosed)
                        return frostmage_Skill_1_1_Choosed;
                    else
                        return frostmage_Skill_1_1_Unchoosed;
                else
                    if (choosed)
                    return frostmage_Skill_1_2_Choosed;
                else
                    return frostmage_Skill_1_2_Unchoosed;
            case 2:
                if (skillNumber == 1)
                    if (choosed)
                        return frostmage_Skill_2_1_Choosed;
                    else
                        return frostmage_Skill_2_1_Unchoosed;
                else
                    if (choosed)
                    return frostmage_Skill_2_2_Choosed;
                else
                    return frostmage_Skill_2_2_Unchoosed;
            case 3:
                if (skillNumber == 1)
                    if (choosed)
                        return frostmage_Skill_3_1_Choosed;
                    else
                        return frostmage_Skill_3_1_Unchoosed;
                else
                    if (choosed)
                    return frostmage_Skill_3_2_Choosed;
                else
                    return frostmage_Skill_3_2_Unchoosed;
            case 4:
                if (skillNumber == 1)
                    if (choosed)
                        return frostmage_Skill_4_1_Choosed;
                    else
                        return frostmage_Skill_4_1_Unchoosed;
                else
                    if (choosed)
                    return frostmage_Skill_4_2_Choosed;
                else
                    return frostmage_Skill_4_2_Unchoosed;
            default:
                break;
        }
        return frostmage_Skill_1_1_Unchoosed;
    }
}
