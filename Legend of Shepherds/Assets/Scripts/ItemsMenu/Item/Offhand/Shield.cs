using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shield : Offhand {

    int[] attributeQualities = new int[4];

    ItemStatType.Stats armor1Type = ItemStatType.Stats.Armor;
    float armor1Value;

    ItemStatType.Stats armor2Type = ItemStatType.Stats.Armor;
    float armor2Value;

    ItemStatType.Stats mainStatType;
    float mainStatValue;

    public Shield(string r, int l) : base(r, l)
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
        armor1Value = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(armor1Type, attributeQualities[1]) * rarityBonus;
        armor2Value = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(armor2Type, attributeQualities[2]) * rarityBonus;
        mainStatType = GameObject.Find("Datas").GetComponent<ItemDatas>().GetMainAttributeType();
        mainStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(mainStatType, attributeQualities[3]) * rarityBonus;
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

        attributes[(int)armor1Type] += Mathf.RoundToInt(armor1Value * (1f + GetStar() * 0.2f));
        attributes[(int)armor2Type] += Mathf.RoundToInt(armor2Value * (1f + GetStar() * 0.2f));
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

    public override string GetOffhandType()
    {
        return "Shield";
    }

    public override string GetDescription()
    {
        string title;
        switch (GetRarity())
        {
            case "Common":
                title = "<color=#606060ff><b>Shield</b></color>   \n";
                break;
            case "Uncommon":
                title = "<color=green><b>Shield</b></color>   \n";
                break;
            case "Heroic":
                title = "<color=purple><b>Shield</b></color>   \n";
                break;
            case "Legendary":
                title = "<color=yellow><b>Shield</b></color>   \n";
                break;
            default:
                title = "<color=#6d6d6dff><b>Shield</b></color>   \n";
                break;
        }

        string line1 = GetRarity() + " " + GetLevel() + " level\n";

        float multiplier = GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel()) * (1f + GetStar() * 0.2f);

        string line2 = "  + " + Mathf.RoundToInt((armor1Value + armor2Value) * multiplier) + " " + ItemStatType.GetStatDescription(armor1Type) + "\n";

        string line3 = "  + " + Mathf.RoundToInt(mainStatValue * multiplier) + " " + ItemStatType.GetStatDescription(mainStatType) + "\n";


        string all = title + line1 + line2 + line3;
        return all;
    }
}
