using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponSkills : MonoBehaviour {

    string weaponType;

    float cooldown;
    float cooldownTimer;
    public Image skillCdShadow;

    bool skillReady;

    public void Initialize()
    {
        weaponType = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_WeaponType();
        cooldown = 15f;
        cooldown -= GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusWeaponSkillCD();
        cooldownTimer = 1000f;
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= cooldown)
            skillReady = true;

        float skillCdShadowCounter;
        if (cooldownTimer >= cooldown)
            skillCdShadowCounter = 1;
        else
            skillCdShadowCounter = cooldownTimer / cooldown;

        skillCdShadow.fillAmount = 1 - skillCdShadowCounter;
    }

    public void UseSkill()
    {
        if (skillReady)
        {
            UseWeaponSkill();
            skillReady = false;
            cooldownTimer = 0f;
        }
    }

    public void UseWeaponSkill()
    {
        switch (weaponType)
        {
            case "Staff":
                Instantiate(GameObject.Find("Datas").GetComponent<AttackPrefabs>().weaponSkill_Staff);
                break;
            case "Bow":
                Instantiate(GameObject.Find("Datas").GetComponent<AttackPrefabs>().weaponSkill_Bow);
                break;
            case "Crossbow":
                Instantiate(GameObject.Find("Datas").GetComponent<AttackPrefabs>().weaponSkill_Crossbow);
                break;
            case "Boomerang":
                Instantiate(GameObject.Find("Datas").GetComponent<AttackPrefabs>().weaponSkill_Boomerang);
                break;
        }
    }
}
