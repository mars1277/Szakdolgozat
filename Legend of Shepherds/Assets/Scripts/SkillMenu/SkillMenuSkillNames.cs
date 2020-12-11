using UnityEngine;
using System.Collections;

public class SkillMenuSkillNames : MonoBehaviour {

    public string GetSkillName(int skillLevel, int skillNumber, string character)
    {
        if (character.Equals("FireMage"))
        {
            return GetFireMageSkillName(skillLevel, skillNumber);
        }

        if (character.Equals("FrostMage"))
        {
            return GetFrostMageSkillName(skillLevel, skillNumber);
        }

        return "Error";
    }

    public string GetFireMageSkillName(int skillLevel, int skillNumber)
    {
        switch (skillLevel)
        {
            case 1:
                if (skillNumber == 1)
                    return "Fireball";
                else
                    return "Firebreath";
            case 2:
                if (skillNumber == 1)
                    return "Fire Nova";
                else
                    return "Fire Wall";
            case 3:
                if (skillNumber == 1)
                    return "Fire Mine";
                else
                    return "Meteorite";
            case 4:
                if (skillNumber == 1)
                    return "Fire Rain";
                else
                    return "Fire Elemental";
            default:
                break;
        }
        return "Error";
    }

    public string GetFrostMageSkillName(int skillLevel, int skillNumber)
    {
        switch (skillLevel)
        {
            case 1:
                if (skillNumber == 1)
                    return "Fireball";
                else
                    return "Firebreath";
            case 2:
                if (skillNumber == 1)
                    return "Fire Nova";
                else
                    return "Fire Wall";
            case 3:
                if (skillNumber == 1)
                    return "Fire Mine";
                else
                    return "Meteorite";
            case 4:
                if (skillNumber == 1)
                    return "Fire Rain";
                else
                    return "Fire Elemental";
            default:
                break;
        }
        return "Error";
    }
}
