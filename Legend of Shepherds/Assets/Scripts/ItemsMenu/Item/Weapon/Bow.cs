﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bow : Weapon
{
    int[] attributeQualities = new int[3];

    ItemStatType.Stats mainStatType;
    float mainStatValue;

    ItemStatType.Stats secStatType;
    float secStatValue;

    public Bow(string r, int l) : base(r, l)
    {
        SetAttributes();
    }

    public override void SetAttributes()
    {
        SetAttackSpeed(1f);
        SetTwoHanded(true);
        System.Random r = new System.Random();
        float attackQuality = r.Next(800, 1201);
        SetDamageQuality(attackQuality);
        SetDamagePerSec(attackQuality * 15 / 100 / 10f);

        float rarityBonus;
        attributeQualities = GameObject.Find("Datas").GetComponent<ItemDatas>().GetAttributesQualities(2);
        switch (GetRarity())
        {
            case "Common":
                rarityBonus = 1f;
                break;
            case "Uncommon":
                rarityBonus = 2f;
                break;
            case "Heroic":
                rarityBonus = 3f;
                break;
            case "Legendary":
                rarityBonus = 4f;
                break;
            default:
                rarityBonus = 0f;
                break;
        }
        mainStatType = GameObject.Find("Datas").GetComponent<ItemDatas>().GetMainAttributeType();
        mainStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(mainStatType, attributeQualities[1]) * rarityBonus;
        secStatType = GameObject.Find("Datas").GetComponent<ItemDatas>().GetSecondaryAttributeType();
        if (secStatType == ItemStatType.Stats.HealthRegen)
        {
            secStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStatType, attributeQualities[2]) * rarityBonus;
        }
        else
        {
            secStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStatType, attributeQualities[2]);
        }
        SetPrice(Mathf.RoundToInt((attributeQualities[0] + GetDamageQuality()) * rarityBonus * rarityBonus / 10f * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel())));
        SetUpgradePrice(Mathf.RoundToInt(GetPrice() * 1.2f));
    }

    public override float[] GetAttributes()
    {
        float[] attributes = new float[9];
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i] = 0;
        }

        attributes[(int)mainStatType] += Mathf.RoundToInt(mainStatValue * (1f + GetStar() * 0.2f));
        attributes[(int)secStatType] += Mathf.RoundToInt(secStatValue * (1f + GetStar() * 0.2f));

        for (int i = 0; i < 5; i++)
        {
            int tmp = Mathf.RoundToInt(attributes[i] * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel()));
            attributes[i] = tmp;
        }

        return attributes;
    }

    public override void Upgrade()
    {
        SetPrice(GetPrice() + GetUpgradePrice());
        SetUpgradePrice(Mathf.RoundToInt(GetPrice() / (1f + GetStar() * 0.2f) * (1.2f + GetStar() * 0.2f)));
        IncStar();
    }

    public override string GetWeaponType()
    {
        return "Bow";
    }

    public override string GetDescription()
    {
        string title;
        switch (GetRarity())
        {
            case "Common":
                title = "<color=#606060ff><b>Bow</b></color>   \n";
                break;
            case "Uncommon":
                title = "<color=green><b>Bow</b></color>   \n";
                break;
            case "Heroic":
                title = "<color=purple><b>Bow</b></color>   \n";
                break;
            case "Legendary":
                title = "<color=yellow><b>Bow</b></color>   \n";
                break;
            default:
                title = "<color=gray><b>Bow</b></color>   \n";
                break;
        }

        string line1 = GetRarity() + " " + GetLevel() + " level\n";

        string line2;
        if (GetTwoHanded())
            line2 = "Two-Hand\n";
        else
            line2 = "One-Hand\n";

        float multiplier = GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel()) * (1f + GetStar() * 0.2f);

        int minDmg = Mathf.CeilToInt(GetAttackDamage() *  0.8f);
        int maxDmg = Mathf.CeilToInt(GetAttackDamage() *  1.2f);
        string line3 = "Damage: " + minDmg + " - " + maxDmg + "\n";

        string line4 = "Speed: " + GetAttackSpeed().ToString("0.00") + "\n";

        string line5 = "  + " + Mathf.RoundToInt(mainStatValue * multiplier) + " " + ItemStatType.GetStatDescription(mainStatType) + "\n";

        string line6;
        if (secStatType == ItemStatType.Stats.HealthRegen)
        {
            line6 = "  + " + Mathf.RoundToInt(secStatValue * multiplier) + " " + ItemStatType.GetStatDescription(secStatType) + "\n";
        }
        else
        {
            line6 = "  + " + Mathf.RoundToInt(secStatValue * (1f + GetStar() * 0.2f)) + "% " + ItemStatType.GetStatDescription(secStatType) + "\n";
        }

        string all = title + line1 + line2 + line3 + line4 + line5 + line6;
        return all;
    }
}