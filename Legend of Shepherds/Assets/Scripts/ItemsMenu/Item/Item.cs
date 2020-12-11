using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {

    string rarity;
    int level;
    int star;
    int price;
    int upgradePrice;

    private Item() {
        rarity = "non";
    }

    public Item(string r, int l)
    {
        rarity = r;
        level = l;
        star = 0;
    }

    public string GetRarity()
    {
        return rarity;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetStar()
    {
        return star;
    }

    public virtual void Upgrade()
    {
        star++;
        price = level * 5;
    }

    public int GetPrice()
    {
        return price;
    }

    public void SetPrice(int p)
    {
        price = p;
    }

    public void  IncStar()
    {
        if(star != 5)
            star++;
    }

    public int GetUpgradePrice()
    {
        return upgradePrice;
    }

    public void SetUpgradePrice(int up)
    {
        upgradePrice = up;
    }

    public virtual void SetAttributes()
    {
    }

    public virtual float[] GetAttributes()
    {
        float[] attributes = new float[9];
        return attributes;
    }

    public virtual string GetItemType()
    {
        return "";
    }

    public virtual string GetDescription()
    {
        return "";
    }
 
}
