using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;

public class Save_Load : MonoBehaviour {

    public void ClearSlot(int slotNumber)
    {
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Empty", 1);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Name", "Empty");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Character", "");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Level", 1);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_XP", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_DayPassed", 0);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_Health", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_AttackDamage", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_SpellPower", "0000000000000000000");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Specialization_ActivatedNumber", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_SkillsNumber", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Skills", 9999);
        PlayerPrefs.SetString("Slot" + slotNumber + "_ItemShopRefreshedDate", "");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_ItemShopManuallyRefreshed", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Gold", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_FairyShard", 0);
    }

    public void SaveSlot(int slotNumber, string name, string character, int level, int xp, int dayPassed, string specHealth, string specAttackPower, string specSpellPower, int specActivatedNumber, int skills, int skillsNumber, string itemShopRefreshedDate, int itemShopManuallyRefreshed, int gold, int fairyShard, bool newCharacter)
    {
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Empty", 0);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Name", name);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Character", character);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Level", level);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_XP", xp);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_DayPassed", dayPassed);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_Health", specHealth);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_AttackDamage", specAttackPower);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_SpellPower", specSpellPower);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Specialization_ActivatedNumber", specActivatedNumber);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Skills", skills);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_SkillsNumber", skillsNumber);
        PlayerPrefs.SetString("Slot" + slotNumber + "_ItemShopRefreshedDate", itemShopRefreshedDate);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_ItemShopManuallyRefreshed", itemShopManuallyRefreshed);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Gold", gold);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_FairyShard", fairyShard);
        if (newCharacter)
            SaveItemsBeginning(slotNumber);
    }

    public void ResetSpecialization(int slotNumber)
    {
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_Health", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_AttackDamage", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_SpellPower", "0000000000000000000");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Specialization_ActivatedNumber", 0);
    }

    public void ResetSkills(int slotNumber)
    {
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Skills", 9999);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_SkillsNumber", 0);
    }

    public void MakeCharacter()
    {
        DateTime yesterday = DateTime.Today.AddDays(-1f);
        string yesterdayStr = yesterday.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));

        int slotNumber = 1;

        PlayerPrefs.SetInt("Slot" + slotNumber + "_Empty", 0);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Name", "Kehiba");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Character", "FireMage");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Level", 24);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_XP", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_DayPassed", 20);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_Health", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_AttackDamage", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_SpellPower", "0000000000000000000");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Specialization_ActivatedNumber", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Skills", 1999);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_SkillsNumber", 1);
        PlayerPrefs.SetString("Slot" + slotNumber + "_ItemShopRefreshedDate", yesterdayStr);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_ItemShopManuallyRefreshed", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Gold", 300000);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_FairyShard", 2000);
        SaveItemsBeginning(slotNumber);

        slotNumber = 2;

        PlayerPrefs.SetInt("Slot" + slotNumber + "_Empty", 0);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Name", "Brand");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Character", "FireMage");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Level", 18);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_XP", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_DayPassed", 18);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_Health", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_AttackDamage", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_SpellPower", "0000000000000000000");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Specialization_ActivatedNumber", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Skills", 1999);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_SkillsNumber", 1);
        PlayerPrefs.SetString("Slot" + slotNumber + "_ItemShopRefreshedDate", yesterdayStr);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_ItemShopManuallyRefreshed", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Gold", 100000);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_FairyShard", 2000);
        SaveItemsBeginning(slotNumber);

        slotNumber = 3;

        PlayerPrefs.SetInt("Slot" + slotNumber + "_Empty", 0);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Name", "Pewdiepie");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Character", "FireMage");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Level", 15);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_XP", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_DayPassed", 18);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_Health", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_AttackDamage", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_SpellPower", "0000000000000000000");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Specialization_ActivatedNumber", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Skills", 1999);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_SkillsNumber", 1);
        PlayerPrefs.SetString("Slot" + slotNumber + "_ItemShopRefreshedDate", yesterdayStr);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_ItemShopManuallyRefreshed", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Gold", 80000);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_FairyShard", 2000);
        SaveItemsBeginning(slotNumber);

        slotNumber = 4;

        PlayerPrefs.SetInt("Slot" + slotNumber + "_Empty", 0);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Name", "Charmander");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Character", "FireMage");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Level", 10);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_XP", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_DayPassed", 4);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_Health", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_AttackDamage", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_SpellPower", "0000000000000000000");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Specialization_ActivatedNumber", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Skills", 1999);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_SkillsNumber", 1);
        PlayerPrefs.SetString("Slot" + slotNumber + "_ItemShopRefreshedDate", yesterdayStr);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_ItemShopManuallyRefreshed", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Gold", 2000);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_FairyShard", 2000);
        SaveItemsBeginning(slotNumber);

        slotNumber = 5;

        PlayerPrefs.SetInt("Slot" + slotNumber + "_Empty", 0);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Name", "Gandalf");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Character", "FireMage");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Level", 24);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_XP", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_DayPassed", 99);
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_Health", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_AttackDamage", "0000000000000000000");
        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_SpellPower", "0000000000000000000");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Specialization_ActivatedNumber", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Skills", 1999);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_SkillsNumber", 1);
        PlayerPrefs.SetString("Slot" + slotNumber + "_ItemShopRefreshedDate", yesterdayStr);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_ItemShopManuallyRefreshed", 0);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Gold", 200000000);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_FairyShard", 200000);
        SaveItemsBeginning(slotNumber);
    }

    public void SaveItemsBeginning(int slotNumber)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/itemsData" + slotNumber + ".dat");

        SerializationItems items = new SerializationItems();

        Item[] equippedItems = new Item[8];
        Item[] storedItems = new Item[28];
        Item[] shopItems = new Item[9];

        equippedItems[0] = new Head("Common", 1, new ItemStatType.Stats[] { ItemStatType.Stats.MagicPower, ItemStatType.Stats.AttackSpeed }, new int[] { 160, 160, 160, 160 });
        equippedItems[2] = new Staff("Common", 1, 320, new ItemStatType.Stats[] { ItemStatType.Stats.MagicPower }, new int[] { 160, 160 });
        equippedItems[3] = new Robe("Common", 1, new ItemStatType.Stats[] { ItemStatType.Stats.Health, ItemStatType.Stats.AttackPower, ItemStatType.Stats.HealthRegen }, new int[] { 160, 160, 160, 160, 160 });
        equippedItems[6] = new Boots("Common", 1, new ItemStatType.Stats[] { ItemStatType.Stats.AttackPower}, new int[] { 160, 160, 160, 160 });

        items.equippedItemsSer = equippedItems;
        items.storedItemsSer = storedItems;
        items.shopItemsSer = shopItems;

        bf.Serialize(file, items);
        file.Close();
    }
}
