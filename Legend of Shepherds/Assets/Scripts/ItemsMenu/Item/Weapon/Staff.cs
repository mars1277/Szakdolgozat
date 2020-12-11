using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Staff : Weapon
{
    float magicPowerMultiplier;

    int[] attributeQualities = new int[2];

    ItemStatType.Stats mainStatType;
    float mainStatValue;

    public Staff(string r, int l, int attackQuality, ItemStatType.Stats[] attributeTypes, int[] attributeValues) : base(r, l)
    {
        magicPowerMultiplier = 1.3f;
        SetAttackSpeed(1.5f);
        SetTwoHanded(false);
        SetDamageQuality(attackQuality);
        float rarityBonus;
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
        SetDamagePerSec(attackQuality * 15 * rarityBonus / 100 / 10f);
        attributeQualities = attributeValues;
        mainStatType = attributeTypes[0];
        mainStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(mainStatType, attributeQualities[1]) * rarityBonus;
        SetPrice(Mathf.RoundToInt((attributeQualities[0] + GetDamageQuality() + 200) * rarityBonus * rarityBonus / 10f * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel())));
        SetUpgradePrice(Mathf.RoundToInt(GetPrice() * 1.2f));
    }

    public Staff(string r, int l) : base(r, l)
    {
        SetAttributes();
    }

    public override void SetAttributes()
    {
        magicPowerMultiplier = 1.3f;
        SetAttackSpeed(1f);
        SetTwoHanded(false);
        System.Random r = new System.Random();
        float attackQuality = r.Next(320, 481);
        SetDamageQuality(attackQuality);
        float rarityBonus;
        attributeQualities = GameObject.Find("Datas").GetComponent<ItemDatas>().GetAttributesQualities(1);
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
        SetDamagePerSec(attackQuality * 15 * rarityBonus / 100 / 10f);


        mainStatType = GameObject.Find("Datas").GetComponent<ItemDatas>().GetMainAttributeType();
        mainStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(mainStatType, attributeQualities[1]) * rarityBonus;
        SetPrice(Mathf.RoundToInt((attributeQualities[0] + GetDamageQuality() + 200) * rarityBonus * rarityBonus / 10f * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel())));
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

    public float GetMagicPowerMultiplier()
    {
        return magicPowerMultiplier;
    }

    public override string GetWeaponType()
    {
        return "Staff";
    }

    public override string GetDescription()
    {
        string title;
        switch (GetRarity())
        {
            case "Common":
                title = "<color=#606060ff><b>Staff</b></color>   \n";
                break;
            case "Uncommon":
                title = "<color=green><b>Staff</b></color>   \n";
                break;
            case "Heroic":
                title = "<color=purple><b>Staff</b></color>   \n";
                break;
            case "Legendary":
                title = "<color=yellow><b>Staff</b></color>   \n";
                break;
            default:
                title = "<color=gray><b>Staff</b></color>   \n";
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

        string line6 = "  + " + magicPowerMultiplier + "% Magic Power \n";

        string all = title + line1 + line2 + line3 + line4 + line5 + line6;
        return all;
    }
}
