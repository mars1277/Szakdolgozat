using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImages : MonoBehaviour {

    public Sprite raritySprite_Common;
    public Sprite raritySprite_Uncommon;
    public Sprite raritySprite_Heroic;
    public Sprite raritySprite_Legendary;

    public Sprite itemSprite_Head;
    public Sprite itemSprite_Amulet;
    public Sprite itemSprite_Robe;
    public Sprite itemSprite_Boots;
    public Sprite itemSprite_Ring;
    public Sprite itemSprite_Bow;
    public Sprite itemSprite_Crossbow;
    public Sprite itemSprite_Staff;
    public Sprite itemSprite_Boomerang;
    public Sprite itemSprite_Shield;
    public Sprite itemSprite_Wand;

    public Sprite GetRaritySprite(string rarity)
    {
        switch (rarity)
        {
            case "Common":
                return raritySprite_Common;
            case "Uncommon":
                return raritySprite_Uncommon;
            case "Heroic":
                return raritySprite_Heroic;
            case "Legendary":
                return raritySprite_Legendary;
            default:
                return null;
        }
    }

    public Sprite GetItemSprite(string itemType)
    {
        switch (itemType)
        {
            case "Head":
                return itemSprite_Head;
            case "Amulet":
                return itemSprite_Amulet;
            case "Robe":
                return itemSprite_Robe;
            case "Boots":
                return itemSprite_Boots;
            case "Ring":
                return itemSprite_Ring;
            case "Bow":
                return itemSprite_Bow;
            case "Crossbow":
                return itemSprite_Crossbow;
            case "Staff":
                return itemSprite_Staff;
            case "Boomerang":
                return itemSprite_Boomerang;
            case "Shield":
                return itemSprite_Shield;
            case "Wand":
                return itemSprite_Wand;
            default:
                return null;
        }
    }
}
