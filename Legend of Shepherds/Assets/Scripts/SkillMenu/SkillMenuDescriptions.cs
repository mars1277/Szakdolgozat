using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SkillMenuDescriptions : MonoBehaviour {

    float dmg;
    float cd;

    Item[] equippedItems;
    float mp;
    int level;

    public string GetSkillDescription(int skillLevel, int skillNumber, string character)
    {
        mp = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_MagicPower();
        level = (PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Level"));
        if (character.Equals("FireMage"))
        {
            return GetFireMageSkillDescription(skillLevel, skillNumber);
        }

        return "Error";
    }

    public string GetFireMageSkillDescription(int skillLevel, int skillNumber)
    {
        UniversalSkillType skill = GameObject.Find("Datas").GetComponent<SkillDatas>().GetSkillDatas(skillLevel, skillNumber);
        float mpInc;
        switch (skillLevel)
        {
            case 1:
                if (skillNumber == 1)
                {
                    dmg = Mathf.RoundToInt(GameObject.Find("Datas").GetComponent<Datas>().Get_FireBallDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(level));
                    mpInc = mp * GameObject.Find("Datas").GetComponent<Datas>().Get_FireBallDamage_Player(0);
                    mpInc = Mathf.RoundToInt(mpInc); 
                    cd = skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f);
                    return "Shoots a Fireball in front of you dealing damage to the first enemy hit." + "\n" + "Damage: " + dmg + " <color=cyan>(+" + mpInc + ")</color> " + " / enemy" + "\n" + "Cooldown: " + cd + " sec";
                }
                else
                {
                    dmg = Mathf.RoundToInt(GameObject.Find("Datas").GetComponent<Datas>().Get_FireBreathDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(level));
                    mpInc = mp * GameObject.Find("Datas").GetComponent<Datas>().Get_FireBreathDamage_Player(0);
                    mpInc = Mathf.RoundToInt(mpInc);
                    cd = skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f);
                    return "Breaths fire in a cone dealing damage to all enemy hit." + "\n" + "Damage: " + dmg + " <color=cyan>(+" + mpInc + ")</color> " + " / enemy" + "\n" + "Cooldown: " + cd + " sec";
                }
            case 2:
                if (skillNumber == 1)
                {
                    dmg = Mathf.RoundToInt(GameObject.Find("Datas").GetComponent<Datas>().Get_FireNovaDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(level));
                    mpInc = mp * GameObject.Find("Datas").GetComponent<Datas>().Get_FireNovaDamage_Player(0);
                    mpInc = Mathf.RoundToInt(mpInc);
                    cd = skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f);
                    return "Hits your staff to the ground making a circle of fire that grows over time and dealing damage to all enemy hit." + "\n" + "Damage: " + dmg + " <color=cyan>(+" + mpInc + ")</color> " + " / enemy" + "\n" + "Cooldown: " + cd + " sec";
                }
                else
                {
                    dmg = Mathf.RoundToInt(GameObject.Find("Datas").GetComponent<Datas>().Get_FireWallDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(level));
                    mpInc = mp * GameObject.Find("Datas").GetComponent<Datas>().Get_FireWallDamage_Player(0);
                    mpInc = Mathf.RoundToInt(mpInc);
                    cd = skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f);
                    return "Summons a wall of fire that sets on fire all enemy hit and dealing damage over time." + "\n" + "Damage: " + dmg + " <color=cyan>(+" + mpInc + ")</color> " + " / enemy" + "\n" + "Cooldown: " + cd + " sec";
                }
            case 3:
                if (skillNumber == 1)
                {
                    dmg = Mathf.RoundToInt(GameObject.Find("Datas").GetComponent<Datas>().Get_FireMineDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(level));
                    mpInc = mp * GameObject.Find("Datas").GetComponent<Datas>().Get_FireMineDamage_Player(0);
                    mpInc = Mathf.RoundToInt(mpInc);
                    cd = skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f);
                    return "Puts down a Fire Mine. If an enemy steps on it it explodes dealing damage to all enemy around itself. Stores up to 3 charges." + "\n" + "Damage: " + dmg + " <color=cyan>(+" + mpInc + ")</color> " + " / enemy" + "\n" + "Cooldown: " + cd + " sec";
                }
                else
                {
                    dmg = Mathf.RoundToInt(GameObject.Find("Datas").GetComponent<Datas>().Get_MeteoriteDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(level));
                    mpInc = mp * GameObject.Find("Datas").GetComponent<Datas>().Get_MeteoriteDamage_Player(0);
                    mpInc = Mathf.RoundToInt(mpInc);
                    cd = skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f);
                    return "Summons a Meteorite that explodes on impact dealing damage to all enemy hit. The further from the impact, the less damage they take." + "\n" + "Damage: " + dmg + " <color=cyan>(+" + mpInc + ")</color> " + " / enemy" + "\n" + "Cooldown: " + cd + " sec";
                }
            case 4:
                if (skillNumber == 1)
                {
                    dmg = Mathf.RoundToInt(GameObject.Find("Datas").GetComponent<Datas>().Get_FireRainDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(level));
                    mpInc = mp * GameObject.Find("Datas").GetComponent<Datas>().Get_FireRainDamage_Player(0);
                    mpInc = Mathf.RoundToInt(mpInc);
                    cd = skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f);
                    return "Calls a rain of fire. Every drop explodes on impact dealing damage to all enemy hit." + "\n" + "Damage: " + dmg + " <color=cyan>(+" + mpInc + ")</color> " + " / enemy" + "\n" + "Cooldown: " + cd + " sec";
                }
                else
                {
                    dmg = Mathf.RoundToInt(GameObject.Find("Datas").GetComponent<Datas>().Get_FireElementalDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(level));
                    mpInc = mp * GameObject.Find("Datas").GetComponent<Datas>().Get_FireElementalDamage_Player(0);
                    mpInc = Mathf.RoundToInt(mpInc);
                    cd = skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f);
                    return "Summons an ancient fire elemental that shoots fire beams every second to the closest enemy to your castle. Lasts for 20 seconds." + "\n" + "Damage: " + dmg + " <color=cyan>(+" + mpInc + ")</color> " + " / shot" + "\n" + "Cooldown: " + cd + " sec";
                }
            default:
                break;
        }
        return "Error";
    }
}