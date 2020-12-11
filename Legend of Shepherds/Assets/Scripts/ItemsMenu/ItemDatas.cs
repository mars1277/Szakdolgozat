using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ItemDatas : MonoBehaviour {

    System.Random r = new System.Random();

    public int[] GetAttributesQualities(int numberOfAttributes)
    {
        switch (numberOfAttributes)
        {
            case 1:
                return Get1AttributesQualities();
            case 2:
                return Get2AttributesQualities();
            case 3:
                return Get3AttributesQualities();
            case 4:
                return Get4AttributesQualities();
            default:
                int[] a = { 0 };
                return a;
        }
    }
    public int[] Get1AttributesQualities()
    {
        int[] array = new int[2];
        array[0] = r.Next(160, 241);
        array[1] = array[0];
        return array;
    }
    public int[] Get2AttributesQualities()
    {
        int x = r.Next(320, 481);
        int a = r.Next(Math.Max(100, x - 300), Math.Min(x - 100, 300) + 1);
        int b = x - a;
        int[] array = new int[3];
        array[0] = x;
        array[1] = a;
        array[2] = b;
        return ShuffleArray(array);
    }
    public int[] Get3AttributesQualities()
    {
      int x = r.Next(480, 721);
      int a = r.Next(100, Math.Min(x - 200, 400) + 1);
      int xnew = x - a;
      int b = r.Next(Math.Max(100, xnew - 400), Math.Min(xnew - 100, 400) + 1);
      int c = x - a - b;
      int[] array = new int[4];
      array[0] = x;
      array[1] = a;
      array[2] = b;
      array[3] = c;
      return ShuffleArray(array);
    }

    public int[] Get4AttributesQualities()
    {
        int x = r.Next(640, 961);
        int a = r.Next(160, 160 + (x - 640) / 4 + 1);
        int xnew = x - a;
        int b = r.Next(100, Math.Min(xnew - 200, 400) + 1);
        xnew -= b;
        int c = r.Next(Math.Max(100, xnew - 400), Math.Min(xnew - 100, 400) + 1);
        int d = x - a - b - c;
        int[] array = new int[5];
        array[0] = x;
        array[1] = a;
        array[2] = b;
        array[3] = c;
        array[4] = d;
        return ShuffleArray(array);
    }

    public int[] ShuffleArray(int[] array)
    {
        System.Random r = new System.Random();
        int[] a = (int[])array.Clone();
        int length = a.Length;

        for (int i = 1; i < length; i++)
        {
            int n = r.Next(1, length);
            int tmp = a[i];
            a[i] = a[n];
            a[n] = tmp;
        }
        return a;
    }

    public string GetAttributeName(string itemType, string attributeType)
    {
        switch (r.Next(1, 4))
        {
            case 1:
                return "Health";
            case 2:
                return "Attack Power";
            case 3:
                return "Magic Power";
            case 4:
                return "Physical Armor";
            case 5:
                return "Magic Armor";
            case 6:
                return "Health Regen";
            case 7:
                return "Cd Reduc";
            case 8:
                return "Critical chance";
            case 9:
                return "Attack Speed";
            default:
                return "";
        }
    }

    public ItemStatType.Stats GetMainAttributeType()
    {
        switch (r.Next(1, 4))
        {
            case 1:
                return ItemStatType.Stats.Health;
            case 2:
                return ItemStatType.Stats.AttackPower;
            case 3:
                return ItemStatType.Stats.MagicPower;
            default:
                return ItemStatType.Stats.Health;
        }
    }

    public ItemStatType.Stats GetSecondaryAttributeType()
    {
        switch (r.Next(1, 5))
        {
            case 1:
                return ItemStatType.Stats.HealthRegen;
            case 2:
                return ItemStatType.Stats.AttackSpeed;
            case 3:
                return ItemStatType.Stats.CritChance;
            case 4:
                return ItemStatType.Stats.CDReducation;
            default:
                return ItemStatType.Stats.HealthRegen;
        }
    }

    public float AttributeQualityToValue(ItemStatType.Stats attributeName, int attributeQuality)
    {
        switch (attributeName)
        {
            case ItemStatType.Stats.Health:
                return attributeQuality * 15 / 100;
            case ItemStatType.Stats.AttackPower:
                return attributeQuality * 15 / 100;
            case ItemStatType.Stats.MagicPower:
                return attributeQuality * 15 / 100;
            case ItemStatType.Stats.Armor:
                return attributeQuality * 2 / 100;
            case ItemStatType.Stats.HealthRegen:
                return Mathf.RoundToInt(attributeQuality * 3 / 20) / 10f;
            case ItemStatType.Stats.AttackSpeed:
                return GetAS_CC_CDRValue(attributeQuality);
            case ItemStatType.Stats.CritChance:
                return GetAS_CC_CDRValue(attributeQuality);
            case ItemStatType.Stats.CDReducation:
                return GetAS_CC_CDRValue(attributeQuality);
            case ItemStatType.Stats.MovementSpeed:
                return GetMovSpeedValue(attributeQuality);
            default:
                return 0;
        }
    }

    public float GetMovSpeedValue(int attributeQuality)
    {
        return Mathf.RoundToInt((float)attributeQuality / 50f);
    }

    public float GetAS_CC_CDRValue(int attributeQuality)
    {
        return Mathf.RoundToInt((float)attributeQuality / 25f);
    }
}
