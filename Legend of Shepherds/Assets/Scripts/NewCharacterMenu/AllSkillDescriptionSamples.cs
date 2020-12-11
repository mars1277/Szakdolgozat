using UnityEngine;
using System.Collections;

public class AllSkillDescriptionSamples : MonoBehaviour {

    public string GetSkillDescriptionSample(int skillSampleNumber, string character)
    {
        if (character.Equals("FireMage"))
        {
            return GetFireMageSkillDescriptionSample(skillSampleNumber);
        }
        if (character.Equals("FrostMage"))
        {
            return GetFrostMageSkillDescriptionSample(skillSampleNumber);
        }
        return "Error";
    }

    public string GetFireMageSkillDescriptionSample(int skillSampleNumber)
    {
        switch (skillSampleNumber)
        {
            case 1:
                    return "Fireball\nShoots a fireball that hits and damages the first enemy.";
            case 2:
                    return "Fire Wall\nPlaces a fire wall that damages all enemies going through.";
            case 3:
                    return "Fire Mine\nPlaces a fire mine. If an enemy step on it it eplodes damaging all enemies nearby.";
            case 4:
                    return "Fire Elemental\nCalls the force of a fire elemental that damaging the closest enemy to the castle.";
            default:
                break;
        }
        return "Error";
    }

    public string GetFrostMageSkillDescriptionSample(int skillSampleNumber)
    {
        switch (skillSampleNumber)
        {
            case 1:
                return "Fireball\nShoots a fireball that hits and damages the first enemy.";
            case 2:
                return "Fire Wall\nPlaces a fire wall that damages all enemies going through.";
            case 3:
                return "Fire Mine\nPlaces a fire mine. If an enemy step on it it eplodes damaging all enemies nearby.";
            case 4:
                return "Fire Elemental\nCalls the force of a fire elemental that damaging the closest enemy to the castle.";
            default:
                break;
        }
        return "Error";
    }
}
