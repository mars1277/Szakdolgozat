using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AttributesMenu : MonoBehaviour {

    public new Text name;
    public Text character;
    public Text level;
    public Text dayPassed;
    public Text health;
    public Text healthRegen;
    public Text armor;
    public Text attackDamage;
    public Text attackSpeed;
    public Text critChance;
    public Text magicPower;
    public Text cooldownReduction;
    public Text runSpeed;

    int slotNumber;

    void Start()
    {
        slotNumber = PlayerPrefs.GetInt("GameSlot");
        int levelValue = PlayerPrefs.GetInt("Slot" + slotNumber + "_Level");

        GameObject.Find("Datas").GetComponent<SpecializationDatas>().Initialize();
        GameObject.Find("Datas").GetComponent<AttributeCalculator>().Initialize();

        name.text = PlayerPrefs.GetString("Slot" + slotNumber + "_Name");
        character.text = GetCharacterText();
        level.text = levelValue.ToString();
        dayPassed.text = PlayerPrefs.GetInt("Slot" + slotNumber + "_DayPassed").ToString();
        health.text = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_Health().ToString();
        healthRegen.text = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_HealthRegen().ToString() + " / 2 sec";
        armor.text = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_Armor().ToString();
        float ad = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_AttackDamage();
        int minDmg = Mathf.CeilToInt(ad * 0.8f);
        int maxDmg = Mathf.CeilToInt(ad * 1.2f);
        attackDamage.text = minDmg.ToString() + "-" + maxDmg.ToString();
        attackSpeed.text = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_AttackSpeed().ToString("0.00");
        critChance.text = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CritChance().ToString() + "%";
        magicPower.text = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_MagicPower().ToString();
        cooldownReduction.text = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() + "%";
        runSpeed.text = "+" + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_MovementSpeed() + "%";
    }

    string GetCharacterText()
    {
        string tmp = PlayerPrefs.GetString("Slot" + slotNumber + "_Character");
        switch (tmp)
        {
            case "FireMage":
                return "Fire Mage";
            case "FrostMage":
                return "Frost Mage";
            default:
                return "Error";
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("GameplayMenu");
    }
}
