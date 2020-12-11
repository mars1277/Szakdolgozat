using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wand : Offhand
{
    int[] attributeQualities = new int[4];

    ItemStatType.Stats mainStatType;
    float mainStatValue;

    ItemStatType.Stats secStat1Type;
    float secStat1Value;

    ItemStatType.Stats secStat2Type;
    float secStat2Value;

    public Wand(string r, int l) : base(r, l)
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
        mainStatType = GameObject.Find("Datas").GetComponent<ItemDatas>().GetMainAttributeType();
        mainStatValue = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(mainStatType, attributeQualities[1]) * rarityBonus;
        secStat1Type = GameObject.Find("Datas").GetComponent<ItemDatas>().GetSecondaryAttributeType();
        if (secStat1Type == ItemStatType.Stats.HealthRegen)
        {
            secStat1Value = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStat1Type, attributeQualities[2]) * rarityBonus;
        }
        else
        {
            secStat1Value = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStat1Type, attributeQualities[2]);
        }
        secStat2Type = GameObject.Find("Datas").GetComponent<ItemDatas>().GetSecondaryAttributeType();
        if (secStat2Type == ItemStatType.Stats.HealthRegen)
        {
            secStat2Value = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStat2Type, attributeQualities[3]) * rarityBonus;
        }
        else
        {
            secStat2Value = GameObject.Find("Datas").GetComponent<ItemDatas>().AttributeQualityToValue(secStat2Type, attributeQualities[3]);
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
        attributes[(int)secStat1Type] += Mathf.RoundToInt(secStat1Value * (1f + GetStar() * 0.2f));
        attributes[(int)secStat2Type] += Mathf.RoundToInt(secStat2Value * (1f + GetStar() * 0.2f));

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
        return "Wand";
    }

    public override string GetDescription()
    {
        string title;
        switch (GetRarity())
        {
            case "Common":
                title = "<color=#606060ff><b>Wand</b></color>   \n";
                break;
            case "Uncommon":
                title = "<color=green><b>Wand</b></color>   \n";
                break;
            case "Heroic":
                title = "<color=purple><b>Wand</b></color>   \n";
                break;
            case "Legendary":
                title = "<color=yellow><b>Wand</b></color>   \n";
                break;
            default:
                title = "<color=gray><b>Wand</b></color>   \n";
                break;
        }

        string line1 = GetRarity() + " " + GetLevel() + " level\n";

        float multiplier = GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel()) * (1f + GetStar() * 0.2f);

        string line2 = "  + " + Mathf.RoundToInt(mainStatValue * multiplier) + " " + ItemStatType.GetStatDescription(mainStatType) + "\n";
        string line3;
        string line4;
        if (secStat1Type == secStat2Type)
        {
            if (secStat1Type == ItemStatType.Stats.HealthRegen)
            {
                line3 = "  + " + Mathf.RoundToInt((secStat1Value + secStat2Value) * multiplier) + " " + ItemStatType.GetStatDescription(secStat1Type) + "\n";
            }
            else
            {
                line3 = "  + " + Mathf.RoundToInt((secStat1Value + secStat2Value) * (1f + GetStar() * 0.2f)) + "% " + ItemStatType.GetStatDescription(secStat1Type) + "\n";
            }
            line4 = "";
        }
        else
        {
            if (secStat1Type == ItemStatType.Stats.HealthRegen)
            {
                line3 = "  + " + Mathf.RoundToInt(secStat1Value * multiplier) + " " + ItemStatType.GetStatDescription(secStat1Type) + "\n";
            }
            else
            {
                line3 = "  + " + Mathf.RoundToInt(secStat1Value * (1f + GetStar() * 0.2f)) + "% " + ItemStatType.GetStatDescription(secStat1Type) + "\n";
            }

            if (secStat2Type == ItemStatType.Stats.HealthRegen)
            {
                line4 = "  + " + Mathf.RoundToInt(secStat2Value * multiplier) + " " + ItemStatType.GetStatDescription(secStat2Type) + "\n";
            }
            else
            {
                line4 = "  + " + Mathf.RoundToInt(secStat2Value * (1f + GetStar() * 0.2f)) + "% " + ItemStatType.GetStatDescription(secStat2Type) + "\n";
            }
        }

        string all = title + line1 + line2 + line3 + line4;
        return all;
    }
}
