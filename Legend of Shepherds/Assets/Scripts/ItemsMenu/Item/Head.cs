using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Head : Item
{
    int[] attributeQualities = new int[4];

    ItemStatType.Stats armorType = ItemStatType.Stats.Armor;
    float armorValue;

    ItemStatType.Stats mainStatType;
    float mainStatValue;

    ItemStatType.Stats secStatType;
    float secStatValue;

    public Head(string r, int l, ItemStatType.Stats[] attributeTypes, int[] attributeValues) : base(r, l)
    {
        attributeQualities = attributeValues;
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
        armorValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(armorType, attributeQualities[1]) * rarityBonus;
        mainStatType = attributeTypes[0];
        mainStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(mainStatType, attributeQualities[2]) * rarityBonus;
        secStatType = attributeTypes[1];
        if (secStatType == ItemStatType.Stats.HealthRegen)
        {
            secStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStatType, attributeQualities[3]) * rarityBonus;
        }
        else
        {
            secStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStatType, attributeQualities[3]);
        }
        SetPrice(Mathf.RoundToInt(attributeQualities[0] * rarityBonus * rarityBonus / 10f * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel())));
        SetUpgradePrice(Mathf.RoundToInt(GetPrice() * 1.2f));
    }

    public Head(string r, int l) : base(r, l)
    {
        SetAttributes();
    }

    public override void SetAttributes()
    {
        float rarityBonus;
        attributeQualities = GameObject.Find("Datas").GetComponent<ItemDatas>().GetAttributesQualities(3);
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
        armorValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(armorType, attributeQualities[1]) * rarityBonus;
        mainStatType = GameObject.Find("Datas").GetComponent<ItemDatas>().GetMainAttributeType();
        mainStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(mainStatType, attributeQualities[2]) * rarityBonus;
        secStatType = GameObject.Find("Datas").GetComponent<ItemDatas>().GetSecondaryAttributeType();
        if (secStatType == ItemStatType.Stats.HealthRegen)
        {
            secStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStatType, attributeQualities[3]) * rarityBonus;
        }
        else
        {
            secStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStatType, attributeQualities[3]);
        }
        SetPrice(Mathf.RoundToInt(attributeQualities[0] * rarityBonus * rarityBonus / 10f * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel())));
        SetUpgradePrice(Mathf.RoundToInt(GetPrice() * 1.2f));
    }

    public override float[] GetAttributes()
    {
        float[] attributes = new float[9];
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i] = 0;
        }

        attributes[(int)armorType] += Mathf.RoundToInt(armorValue * (1f + GetStar() * 0.2f));
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

    public override string GetItemType()
    {
        return "Head";
    }

    public override string GetDescription()
    {
        string title;
        switch (GetRarity())
        {
            case "Common":
                title = "<color=#606060ff><b>Head</b></color>   \n";
                break;
            case "Uncommon":
                title = "<color=green><b>Head</b></color>   \n";
                break;
            case "Heroic":
                title = "<color=purple><b>Head</b></color>   \n";
                break;
            case "Legendary":
                title = "<color=yellow><b>Head</b></color>   \n";
                break;
            default:
                title = "<color=gray><b>Head</b></color>   \n";
                break;
        }

        string line1 = GetRarity() + " " + GetLevel() + " level\n";

        float multiplier = GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel()) * (1f + GetStar() * 0.2f);

        string line2 = "  + " + Mathf.RoundToInt(armorValue * multiplier) + " " + ItemStatType.GetStatDescription(armorType) + "\n";

        string line3 = "  + " + Mathf.RoundToInt(mainStatValue * multiplier) + " " + ItemStatType.GetStatDescription(mainStatType) + "\n";

        string line4;
        if (secStatType == ItemStatType.Stats.HealthRegen)
        {
            line4 = "  + " + Mathf.RoundToInt(secStatValue * multiplier) + " " + ItemStatType.GetStatDescription(secStatType) + "\n";
        }
        else
        {
            line4 = "  + " + Mathf.RoundToInt(secStatValue * (1f + GetStar() * 0.2f)) + "% " + ItemStatType.GetStatDescription(secStatType) + "\n";
        }

        string all = title + line1 + line2 + line3 + line4;
        return all;
    }
}
