using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class AttributeCalculator : MonoBehaviour {

    private float health;
    private float attackdamage;
    private float magicPower;
    private float armor;
    private float healthregen;
    private float attackspeed;
    private float critChance;
    private float cooldownreduction;
    private float movSpeed;

    int slotNumber;
    int levelValue;
    float levelMultiplier;

    Item[] equippedItems;
    float itemBonusHealth;
    float itemBonusAttackPower;
    float itemBonusMagicPower;
    float itemBonusArmor;
    float itemBonusHealthRegen;
    float itemBonusAttackSpeed;
    float itemBonusCritChance;
    float itemBonusCDReduc;
    float itemBonusMovSpeed;

    int specBonusAllDmgDone;
    int specBonusAllDmgTaken;
    int specBonusHealth;
    int specBonusHealthRegen;
    int specBonusArmor;
    int specBonusAttackPower;
    int specBonusAttackDamage;
    int specBonusAttackSpeed;
    int specBonusCritChance;
    int specBonusMagicPower;
    int specBonusMagicDamage;
    int specBonusCDReduc;
    int specBonusAllMainStats;
    int specBonusMovSpeed;

    int specBonusMedPower;
    int specBonusMedPowerCD;
    int specBonusWeaponSkillDmg;
    int specBonusWeaponSkillCD;


    float spellDmgFairyPowerBonus;

    public void Initialize()
    {
        slotNumber = PlayerPrefs.GetInt("GameSlot");
        levelValue = PlayerPrefs.GetInt("Slot" + slotNumber + "_Level");
        levelMultiplier = GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(levelValue);

        LoadEquipItems();
        SetSpecializationBonuses();

        itemBonusHealth *= (1 + specBonusAllMainStats / 100f);
        itemBonusAttackPower *= (1 + specBonusAllMainStats / 100f);
        itemBonusMagicPower *= (1 + specBonusAllMainStats / 100f);

        spellDmgFairyPowerBonus = 1f;

        health = 120 * levelMultiplier + Mathf.RoundToInt(itemBonusHealth * (1 + specBonusHealth / 100f));
        attackspeed = ((Weapon)equippedItems[2]).GetAttackSpeed();
        attackdamage = Mathf.RoundToInt(((((Weapon)equippedItems[2]).GetAttackDamage() + Mathf.RoundToInt(itemBonusAttackPower * (1 + specBonusAttackPower / 100f)) / 10f * attackspeed) * (1 + specBonusAttackDamage / 100f)) * (1f + specBonusAllDmgDone / 100f));

        int weaponDamage = ((Weapon)equippedItems[2]).GetAttackDamage();
        int attackPower = Mathf.RoundToInt(itemBonusAttackPower * (1 + specBonusAttackPower / 100f));
        attackdamage = Mathf.RoundToInt(((weaponDamage + attackPower / 10f * attackspeed) * (1 + specBonusAttackDamage / 100f)) * (1f + specBonusAllDmgDone / 100f));

        magicPower = Mathf.RoundToInt(itemBonusMagicPower * (1 + specBonusMagicPower / 100f));
        armor = Mathf.RoundToInt(itemBonusArmor * (1 + specBonusArmor / 100f));
        healthregen = Mathf.RoundToInt(itemBonusHealthRegen * (1 + specBonusHealthRegen / 100f));
        attackspeed *= 100f / (itemBonusAttackSpeed + specBonusAttackSpeed + 100f);
        critChance = itemBonusCritChance + specBonusCritChance;
        cooldownreduction = itemBonusCDReduc + specBonusCDReduc;
        movSpeed = itemBonusMovSpeed + specBonusMovSpeed;
    }

    public void Reinitialize(int newLevel)
    {
        levelValue = newLevel;

        health = GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(levelValue) * 120 + itemBonusHealth;
    }

    void SetSpecializationBonuses()
    {
        string tankTreeSpecStr = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_Health");
        string damageTreeSpecStr = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_AttackDamage");
        string specialTreeSpecStr = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_SpellPower");

        specBonusAllDmgDone = 0;
        specBonusAllDmgTaken = 0;
        specBonusHealth = 0;
        specBonusHealthRegen = 0;
        specBonusArmor = 0;
        specBonusAttackPower = 0;
        specBonusAttackDamage = 0;
        specBonusAttackSpeed = 0;
        specBonusCritChance = 0;
        specBonusMagicPower = 0;
        specBonusMagicDamage = 0;
        specBonusCDReduc = 0;
        specBonusAllMainStats = 0;
        specBonusMovSpeed = 0;

        specBonusMedPower = 0;
        specBonusMedPowerCD = 0;
        specBonusWeaponSkillDmg = 0;
        specBonusWeaponSkillCD = 0;

        //0
        specBonusMovSpeed += Mathf.FloorToInt(3.34f * int.Parse(tankTreeSpecStr.Substring(1, 1)));
        specBonusHealth += 5 * int.Parse(tankTreeSpecStr.Substring(2, 1));
        specBonusHealth += 5 * int.Parse(tankTreeSpecStr.Substring(3, 1));
        specBonusHealth += 5 * int.Parse(tankTreeSpecStr.Substring(4, 1));
        specBonusHealth += 18 * int.Parse(tankTreeSpecStr.Substring(5, 1));
        specBonusArmor = +5 * int.Parse(tankTreeSpecStr.Substring(6, 1));
        specBonusHealthRegen = +5 * int.Parse(tankTreeSpecStr.Substring(7, 1));
        specBonusHealthRegen += 5 * int.Parse(tankTreeSpecStr.Substring(8, 1));
        specBonusHealth += 5 * int.Parse(tankTreeSpecStr.Substring(9, 1));
        specBonusArmor += 5 * int.Parse(tankTreeSpecStr.Substring(10, 1));
        //11
        specBonusHealth += 5 * int.Parse(tankTreeSpecStr.Substring(12, 1));
        specBonusArmor += 5 * int.Parse(tankTreeSpecStr.Substring(13, 1));
        specBonusHealth += 18 * int.Parse(tankTreeSpecStr.Substring(14, 1));
        specBonusArmor += 5 * int.Parse(tankTreeSpecStr.Substring(15, 1));
        specBonusHealthRegen += 5 * int.Parse(tankTreeSpecStr.Substring(16, 1));
        specBonusHealthRegen += 5 * int.Parse(tankTreeSpecStr.Substring(17, 1));
        specBonusArmor += 18 * int.Parse(tankTreeSpecStr.Substring(18, 1));


        //0
        specBonusCDReduc += Mathf.FloorToInt(3.34f * int.Parse(damageTreeSpecStr.Substring(1, 1)));
        specBonusAttackSpeed += Mathf.FloorToInt(3.34f * int.Parse(damageTreeSpecStr.Substring(2, 1)));
        specBonusMagicDamage += Mathf.FloorToInt(3.34f * int.Parse(damageTreeSpecStr.Substring(3, 1)));
        specBonusMagicPower += 5 * int.Parse(damageTreeSpecStr.Substring(4, 1));
        specBonusMagicPower += 18 * int.Parse(damageTreeSpecStr.Substring(5, 1));
        specBonusAttackDamage += Mathf.FloorToInt(3.34f * int.Parse(damageTreeSpecStr.Substring(6, 1)));
        specBonusCritChance += Mathf.FloorToInt(3.34f * int.Parse(damageTreeSpecStr.Substring(7, 1)));
        specBonusMagicDamage += Mathf.FloorToInt(3.34f * int.Parse(damageTreeSpecStr.Substring(8, 1)));
        specBonusAttackSpeed += Mathf.FloorToInt(3.34f * int.Parse(damageTreeSpecStr.Substring(9, 1)));
        specBonusCritChance += Mathf.FloorToInt(3.34f * int.Parse(damageTreeSpecStr.Substring(10, 1)));
        //11
        specBonusAttackPower += 5 * int.Parse(damageTreeSpecStr.Substring(12, 1));
        specBonusAttackDamage += Mathf.FloorToInt(3.34f * int.Parse(damageTreeSpecStr.Substring(13, 1)));
        specBonusAttackPower += 18 * int.Parse(damageTreeSpecStr.Substring(14, 1));
        specBonusCDReduc += Mathf.FloorToInt(3.34f * int.Parse(damageTreeSpecStr.Substring(15, 1)));
        specBonusCDReduc += Mathf.FloorToInt(3.34f * int.Parse(damageTreeSpecStr.Substring(16, 1)));
        specBonusMagicPower += 5 * int.Parse(damageTreeSpecStr.Substring(17, 1));
        specBonusAttackPower += 18 * int.Parse(damageTreeSpecStr.Substring(18, 1));


        //0
        specBonusAttackSpeed += Mathf.FloorToInt(3.34f * int.Parse(specialTreeSpecStr.Substring(1, 1)));
        specBonusCDReduc += Mathf.FloorToInt(3.34f * int.Parse(specialTreeSpecStr.Substring(2, 1)));
        specBonusAllMainStats += Mathf.FloorToInt(3.34f * int.Parse(specialTreeSpecStr.Substring(3, 1)));
        specBonusMovSpeed += Mathf.FloorToInt(3.34f * int.Parse(specialTreeSpecStr.Substring(4, 1)));
        specBonusAllMainStats += 12 * int.Parse(specialTreeSpecStr.Substring(5, 1));
        specBonusMedPower += 5 * int.Parse(specialTreeSpecStr.Substring(6, 1));
        specBonusAllMainStats += Mathf.FloorToInt(3.34f * int.Parse(specialTreeSpecStr.Substring(7, 1)));
        specBonusWeaponSkillDmg += 5 * int.Parse(specialTreeSpecStr.Substring(8, 1));
        specBonusAllMainStats += Mathf.FloorToInt(3.34f * int.Parse(specialTreeSpecStr.Substring(9, 1)));
        specBonusMovSpeed += Mathf.FloorToInt(3.34f * int.Parse(specialTreeSpecStr.Substring(10, 1)));
        //11
        specBonusMedPowerCD += 5 * int.Parse(specialTreeSpecStr.Substring(12, 1));
        specBonusMedPower += 5 * int.Parse(specialTreeSpecStr.Substring(13, 1));
        specBonusMagicPower += 18 * int.Parse(specialTreeSpecStr.Substring(15, 1));
        specBonusWeaponSkillCD += int.Parse(specialTreeSpecStr.Substring(15, 1));
        specBonusWeaponSkillDmg += 5 * int.Parse(specialTreeSpecStr.Substring(16, 1));
        specBonusMovSpeed += Mathf.FloorToInt(3.34f * int.Parse(specialTreeSpecStr.Substring(17, 1)));
        specBonusAllMainStats += 12 * int.Parse(specialTreeSpecStr.Substring(18, 1));
    }

    public void LoadEquipItems()
    {
        if (File.Exists(Application.persistentDataPath + "/itemsData" + PlayerPrefs.GetInt("GameSlot") + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/itemsData" + PlayerPrefs.GetInt("GameSlot") + ".dat", FileMode.Open);
            SerializationItems items = (SerializationItems)bf.Deserialize(file);
            file.Close();

            equippedItems = items.equippedItemsSer;
            CalculateAttributes();
        }
    }

    void CalculateAttributes()
    {
        for (int i = 0; i < 8; i++)
        {
            if ((equippedItems[i] != null) && !equippedItems[i].GetRarity().Equals("non"))
            {
                float[] attributes = equippedItems[i].GetAttributes();
                itemBonusHealth += Mathf.RoundToInt(attributes[0]);
                itemBonusAttackPower += Mathf.RoundToInt(attributes[1]);
                itemBonusMagicPower += Mathf.RoundToInt(attributes[2]);
                itemBonusArmor += Mathf.RoundToInt(attributes[3]);
                itemBonusHealthRegen += Mathf.RoundToInt(attributes[4]);
                itemBonusAttackSpeed += Mathf.RoundToInt(attributes[5]);
                itemBonusCritChance += Mathf.RoundToInt(attributes[6]);
                itemBonusCDReduc += Mathf.RoundToInt(attributes[7]);
                itemBonusMovSpeed += Mathf.RoundToInt(attributes[8]);
            }
        }
        if (((Weapon)equippedItems[2]).GetWeaponType().Equals("Staff"))
            itemBonusMagicPower = Mathf.RoundToInt(itemBonusMagicPower * ((Staff)equippedItems[2]).GetMagicPowerMultiplier());
    }

    public int Get_BonusAllDmgDone()
    {
        return specBonusAllDmgDone;
    }

    public int Get_BonusAllDmgTaken()
    {
        return specBonusAllDmgTaken;
    }

    public int Get_Health()
    {
        return (int)health;
    }

    public int Get_HealthRegen()
    {
        return (int)healthregen;
    }

    public int Get_Armor()
    {
        return (int)armor;
    }

    public int Get_AttackDamage()
    {
        return (int)attackdamage;
    }

    public float Get_AttackSpeed()
    {
        return attackspeed;
    }

    public int Get_CritChance()
    {
        return (int)critChance;
    }

    public int Get_MagicPower()
    {
        return Mathf.RoundToInt(magicPower * spellDmgFairyPowerBonus);
    }

    public int Get_BonusMagicDamage()
    {
        return specBonusMagicDamage;
    }

    public int Get_CooldownReduction()
    {
        return (int)cooldownreduction;
    }

    public int Get_BonusMedallionPower()
    {
        return specBonusMedPower;
    }

    public int Get_BonusMedallionPowerCD()
    {
        return specBonusMedPowerCD;
    }

    public int Get_BonusWeaponSkillDmg()
    {
        return specBonusWeaponSkillDmg;
    }

    public int Get_BonusWeaponSkillCD()
    {
        return specBonusWeaponSkillCD;
    }

    public int Get_MovementSpeed()
    {
        return (int)movSpeed;
    }

    public string Get_WeaponType()
    {
        return ((Weapon)equippedItems[2]).GetWeaponType();
    }

    public void IncreaseFairyPowerSpellDamage(int value)
    {
        specBonusMagicDamage += value;
    }

    public void DecreaseFairyPowerSpellDamage(int value)
    {
        specBonusMagicDamage -= value;
    }

    public void IncreaseFairyPowerAttackDamage(int value)
    {
        specBonusAttackDamage += value;
        attackdamage = Mathf.RoundToInt(((((Weapon)equippedItems[2]).GetAttackDamage() + Mathf.RoundToInt(itemBonusAttackPower * (1 + specBonusAttackPower / 100f)) / 10f * attackspeed) * (1 + specBonusAttackDamage / 100f)) * (1f + specBonusAllDmgDone / 100f));
    }

    public void DecreaseFairyPowerAttackDamage(int value)
    {
        specBonusAttackDamage -= value;
        attackdamage = Mathf.RoundToInt(((((Weapon)equippedItems[2]).GetAttackDamage() + Mathf.RoundToInt(itemBonusAttackPower * (1 + specBonusAttackPower / 100f)) / 10f * attackspeed) * (1 + specBonusAttackDamage / 100f)) * (1f + specBonusAllDmgDone / 100f));
    }
}
