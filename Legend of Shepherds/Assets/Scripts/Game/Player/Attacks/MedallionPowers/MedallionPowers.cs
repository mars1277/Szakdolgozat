using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MedallionPowers : MonoBehaviour {

    int slotNumber;

    float cooldown;
    float cooldownTimer;
    public Image powerCdShadow;
    public Image medallionImage;

    public Sprite medallion_01;
    public Sprite medallion_02;
    public Sprite medallion_03;
    public Sprite medallion_04;
    public Sprite medallion_05;
    public Sprite medallion_06;

    public int baseFairyPowerID;
    public int empoweredFairyPowerID;

    bool powerReady;

    void Start()
    {
        slotNumber = PlayerPrefs.GetInt("GameSlot");
        string tankTreeSpecStr = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_Health");
        string damageTreeSpecStr = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_AttackDamage");
        string specialTreeSpecStr = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_SpellPower");

        baseFairyPowerID = 0;
        empoweredFairyPowerID = 0;

        if (int.Parse(tankTreeSpecStr.Substring(0, 1)) == 1)
            baseFairyPowerID = 1;
        else
        if (int.Parse(damageTreeSpecStr.Substring(0, 1)) == 1)
            baseFairyPowerID = 2;
        else
        if (int.Parse(specialTreeSpecStr.Substring(0, 1)) == 1)
            baseFairyPowerID = 3;

        if (int.Parse(tankTreeSpecStr.Substring(11, 1)) == 1)
            empoweredFairyPowerID = 1;
        else
        if (int.Parse(damageTreeSpecStr.Substring(11, 1)) == 1)
            empoweredFairyPowerID = 2;
        else
        if (int.Parse(specialTreeSpecStr.Substring(11, 1)) == 1)
            empoweredFairyPowerID = 3;

        cooldown = 45f;
        cooldown -= GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMedallionPowerCD();
        cooldownTimer = 1000f;

        medallionImage.sprite = medallion_01;
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= cooldown)
            powerReady = true;

        float powerCdShadowCounter;
        if (cooldownTimer >= cooldown)
            powerCdShadowCounter = 1;
        else
            powerCdShadowCounter = cooldownTimer / cooldown;

        powerCdShadow.fillAmount = 1 - powerCdShadowCounter;
    }

    public void UsePower()
    {
        if (powerReady)
        {
            UseFairyPower();
            powerReady = false;
            cooldownTimer = 0f;
        }
    }

    public void UseFairyPower()
    {
        switch (baseFairyPowerID)
        {
            case 1:
                GameObject.Find("Player").GetComponent<Player_Health>().SwitchOnDamageReducation(30f * (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMedallionPower() / 100f));
                break;
            case 2:
                GameObject.Find("Datas").GetComponent<AttributeCalculator>().IncreaseFairyPowerSpellDamage(Mathf.RoundToInt(25 * (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMedallionPower() / 100f)));
                Invoke("DecreaseSpellDamage", 10f);
                GameObject.Find("Datas").GetComponent<AttributeCalculator>().IncreaseFairyPowerAttackDamage(Mathf.RoundToInt(25 * (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMedallionPower() / 100f)));
                Invoke("DecreaseAttackDamage", 10f);
                break;
            case 3:
                GameObject.Find("Player").GetComponent<Player_Health>().FairyPowerBaseHealthHeal();
                break;
            default:
                break;
        }

        switch (empoweredFairyPowerID)
        {
            case 1:
                GameObject.Find("Player").GetComponent<Player_Health>().FairyPowerMaxHealthHealOverTime();
                break;
            case 2:
                GameObject.Find("Player").GetComponent<Player_Health>().ActivateFairyPowerHealFromDamage();
                Invoke("DeactivateFairyPowerHealFromDamage", 10f);
                break;
            case 3:
                GameObject.Find("Player").GetComponent<Player_Movement>().IncreaseMovementSpeed(1.2f * (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMedallionPower() / 100f));
                Invoke("DecreaseMovementSpeed", 10f);
                break;
        }
    }

    void SwitchOffDamageReducation()
    {
        GameObject.Find("Player").GetComponent<Player_Health>().SwitchOffDamageReducation();
    }

    void DecreaseSpellDamage()
    {
        GameObject.Find("Datas").GetComponent<AttributeCalculator>().DecreaseFairyPowerSpellDamage(Mathf.RoundToInt(25 * (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMedallionPower() / 100f)));
    }
    void DecreaseAttackDamage()
    {
        GameObject.Find("Datas").GetComponent<AttributeCalculator>().DecreaseFairyPowerAttackDamage(Mathf.RoundToInt(25 * (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMedallionPower() / 100f)));
    }
    void DecreaseMovementSpeed()
    {
        GameObject.Find("Player").GetComponent<Player_Movement>().DecreaseMovementSpeed();
    }

    void DeactivateFairyPowerHealFromDamage()
    {
        GameObject.Find("Player").GetComponent<Player_Health>().DeactivateFairyPowerHealFromDamage();
    }
}
