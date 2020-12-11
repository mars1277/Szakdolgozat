using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Amulet : Item
{
    int[] attributeQualities = new int[3];

    ItemStatType.Stats mainStatType;
    float mainStatValue;

    ItemStatType.Stats secStatType;
    float secStatValue;

    public Amulet(string r, int l) : base(r, l)
    {
        SetAttributes();
    }


    public override void SetAttributes()
    {
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
        return "Amulet";
    }

    public override string GetDescription()
    {
        string title;
        switch (GetRarity())
        {
            case "Common":
                title = "<color=#606060ff><b>Amulet</b></color>   \n";
                break;
            case "Uncommon":
                title = "<color=green><b>Amulet</b></color>   \n";
                break;
            case "Heroic":
                title = "<color=purple><b>Amulet</b></color>   \n";
                break;
            case "Legendary":
                title = "<color=yellow><b>Amulet</b></color>   \n";
                break;
            default:
                title = "<color=gray><b>Amulet</b></color>   \n";
                break;
        }

        string line1 = GetRarity() + " " + GetLevel() + " level\n";

        float multiplier = GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel()) * (1f + GetStar() * 0.2f);

        string line2 = "  + " + Mathf.RoundToInt(mainStatValue * multiplier) + " " + ItemStatType.GetStatDescription(mainStatType) + "\n";

        string line3;
        if (secStatType == ItemStatType.Stats.HealthRegen)
        {
            line3 = "  + " + Mathf.RoundToInt(secStatValue * multiplier) + " " + ItemStatType.GetStatDescription(secStatType) + "\n";
        }
        else
        {
            line3 = "  + " + Mathf.RoundToInt(secStatValue * (1f + GetStar() * 0.2f)) + "% " + ItemStatType.GetStatDescription(secStatType) + "\n";
        }

        string all = title + line1 + line2 + line3;
        return all;
    }
}
