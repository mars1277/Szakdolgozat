using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

[System.Serializable]
public class Robe : Item
{
    int[] attributeQualities = new int[5];

    ItemStatType.Stats armorType = ItemStatType.Stats.Armor;
    float armorValue;

    ItemStatType.Stats mainStat1Type;
    float mainStat1Value;

    ItemStatType.Stats mainStat2Type;
    float mainStat2Value;

    ItemStatType.Stats secStatType;
    float secStatValue;

    public Robe(string r, int l, ItemStatType.Stats[] attributeTypes, int[] attributeValues) : base(r, l)
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
        mainStat1Type = attributeTypes[0];
        mainStat1Value = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(mainStat1Type, attributeQualities[2]) * rarityBonus;
        mainStat2Type = attributeTypes[1];
        mainStat2Value = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(mainStat2Type, attributeQualities[3]) * rarityBonus;
        secStatType = attributeTypes[2];
        if (secStatType == ItemStatType.Stats.HealthRegen)
        {
            secStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStatType, attributeQualities[4]) * rarityBonus;
        }
        else
        {
            secStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStatType, attributeQualities[4]);
        }
        SetPrice(Mathf.RoundToInt(attributeQualities[0] * rarityBonus * rarityBonus / 10f * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel())));
        SetUpgradePrice(Mathf.RoundToInt(GetPrice() * 1.2f));
    }

    public Robe(string r, int l) : base(r, l)
    {
        SetAttributes();
    }

    public override void SetAttributes()
    {
        float rarityBonus;
        attributeQualities = GameObject.Find("Datas").GetComponent<ItemDatas>().GetAttributesQualities(4);
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
        mainStat1Type = GameObject.Find("Datas").GetComponent<ItemDatas>().GetMainAttributeType();
        mainStat1Value = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(mainStat1Type, attributeQualities[2]) * rarityBonus;
        mainStat2Type = GameObject.Find("Datas").GetComponent<ItemDatas>().GetMainAttributeType();
        mainStat2Value = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(mainStat2Type, attributeQualities[3]) * rarityBonus;
        secStatType = GameObject.Find("Datas").GetComponent<ItemDatas>().GetSecondaryAttributeType();
        if (secStatType == ItemStatType.Stats.HealthRegen)
        {
            secStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStatType, attributeQualities[4]) * rarityBonus;
        }
        else
        {
            secStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStatType, attributeQualities[4]);
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
        attributes[(int)mainStat1Type] += Mathf.RoundToInt(mainStat1Value * (1f + GetStar() * 0.2f));
        attributes[(int)mainStat2Type] += Mathf.RoundToInt(mainStat2Value * (1f + GetStar() * 0.2f));
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
        return "Robe";
    }

    public override string GetDescription()
    {
        string title;
        switch (GetRarity())
        {
            case "Common":
                title = "<color=#606060ff><b>Robe</b></color>   \n";
                break;
            case "Uncommon":
                title = "<color=green><b>Robe</b></color>   \n";
                break;
            case "Heroic":
                title = "<color=purple><b>Robe</b></color>   \n";
                break;
            case "Legendary":
                title = "<color=yellow><b>Robe</b></color>   \n";
                break;
            default:
                title = "<color=gray><b>Robe</b></color>   \n";
                break;
        }

        string line1 = GetRarity() + " " + GetLevel() + " level\n";

        float multiplier = GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel()) * (1f + GetStar() * 0.2f);

        string line2 = "  + " + Mathf.RoundToInt(armorValue * multiplier) + " " + ItemStatType.GetStatDescription(armorType) + "\n";
        string line3;
        string line4;
        if (mainStat1Type == mainStat2Type)
        {
            line3 = "  + " + Mathf.RoundToInt((mainStat1Value + mainStat2Value) * multiplier) + " " + ItemStatType.GetStatDescription(mainStat1Type) + "\n";
            line4 = "";
        }
        else
        {
            line3 = "  + " + Mathf.RoundToInt(mainStat1Value * multiplier) + " " + ItemStatType.GetStatDescription(mainStat1Type) + "\n";

            line4 = "  + " + Mathf.RoundToInt(mainStat2Value * multiplier) + " " + ItemStatType.GetStatDescription(mainStat2Type) + "\n";
        }
        string line5;
        if (secStatType == ItemStatType.Stats.HealthRegen)
        {
            line5 = "  + " + Mathf.RoundToInt(secStatValue * multiplier) + " " + ItemStatType.GetStatDescription(secStatType) + "\n";
        }
        else
        {
            line5 = "  + " + Mathf.RoundToInt(secStatValue * (1f + GetStar() * 0.2f)) + "% " + ItemStatType.GetStatDescription(secStatType) + "\n";
        }
        string all = title + line1 + line2 + line3 + line4 + line5;
        return all;
    }
}
