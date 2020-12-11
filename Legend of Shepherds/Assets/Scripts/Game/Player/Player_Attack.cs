using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Player_Attack : MonoBehaviour
{

    public GameObject magicMissleGO;
    public GameObject bowArrowGO;
    public GameObject crossbowArrowGO;
    public GameObject boomerangGO;
    public GameObject shootPosition;

    string weaponType;

    float shootPerSec;
    float shootingSpeed;
    float attackCounter = 100;
    public Image attackCdShadow;

    int[] skills;

    public GameObject skill_1_Active_Normal;
    public GameObject skill_1_Active_WithCharges;
    public GameObject skill_1_Active_Dragable;
    public GameObject skill_2_Active_Normal;
    public GameObject skill_2_Active_WithCharges;
    public GameObject skill_2_Active_Dragable;
    public GameObject skill_3_Active_Normal;
    public GameObject skill_3_Active_WithCharges;
    public GameObject skill_3_Active_Dragable;
    public GameObject skill_4_Active_Normal;
    public GameObject skill_4_Active_WithCharges;
    public GameObject skill_4_Active_Dragable;

    public GameObject medallionButton;

    public Image weaponAttackButtonImage;
    public Image weaponSkillButtonImage;

    public Sprite staffWeaponSkillSprite;
    public Sprite bowWeaponSkillSprite;
    public Sprite crossbowWeaponSkillSprite;
    public Sprite boomerangWeaponSkillSprite;

    int slotNumber;

    System.Random r;
    float critChance;

    public int skillCount = 0;
    public bool medallionIsAvailable = false;

    public void Initialize()
    {
        skills = new int[4];
        shootingSpeed = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_AttackSpeed();

        weaponType = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_WeaponType();
        r = new System.Random();
        critChance = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CritChance() / 100f;

        slotNumber = PlayerPrefs.GetInt("GameSlot");

        MakeSkillButtons();
        MakeMedallionButton();
        switch (weaponType)
        {
            case "Staff":
                weaponSkillButtonImage.sprite = staffWeaponSkillSprite;
                weaponSkillButtonImage.rectTransform.sizeDelta = new Vector3(36, 36);
                weaponAttackButtonImage.sprite = staffWeaponSkillSprite;
                weaponAttackButtonImage.rectTransform.sizeDelta = new Vector3(36, 36);
                break;
            case "Bow":
                weaponSkillButtonImage.sprite = bowWeaponSkillSprite;
                weaponSkillButtonImage.rectTransform.sizeDelta = new Vector3(36, 36);
                weaponAttackButtonImage.sprite = bowWeaponSkillSprite;
                weaponAttackButtonImage.rectTransform.sizeDelta = new Vector3(36, 36);
                break;
            case "Crossbow":
                weaponSkillButtonImage.sprite = crossbowWeaponSkillSprite;
                weaponSkillButtonImage.rectTransform.sizeDelta = new Vector3(36, 36);
                weaponAttackButtonImage.sprite = crossbowWeaponSkillSprite;
                weaponAttackButtonImage.rectTransform.sizeDelta = new Vector3(36, 36);
                break;
            case "Boomerang":
                weaponSkillButtonImage.sprite = boomerangWeaponSkillSprite;
                weaponSkillButtonImage.rectTransform.sizeDelta = new Vector3(28, 36);
                weaponAttackButtonImage.sprite = boomerangWeaponSkillSprite;
                weaponAttackButtonImage.rectTransform.sizeDelta = new Vector3(28, 36);
                break;
            default:
                weaponSkillButtonImage.sprite = staffWeaponSkillSprite;
                weaponSkillButtonImage.rectTransform.sizeDelta = new Vector3(36, 36);
                weaponAttackButtonImage.sprite = staffWeaponSkillSprite;
                weaponAttackButtonImage.rectTransform.sizeDelta = new Vector3(36, 36);
                break;
        }
    }

    void Update()
    {
        attackCounter += Time.deltaTime;

        float attackCdShadowCounter;
        if (attackCounter >= shootingSpeed)
            attackCdShadowCounter = 1;
        else
            attackCdShadowCounter = attackCounter / shootingSpeed;

        attackCdShadow.fillAmount = 1 - attackCdShadowCounter;


        if ((attackCounter >= shootingSpeed) && (Input.GetKey("space")))
        {
            Shoot();
        }
    }

    public void MakeSkillButtons()
    {
        int skillNumbers = PlayerPrefs.GetInt("Slot" + slotNumber + "_Skills");
        int tmp = skillNumbers / 1000;
        GameObject datas = GameObject.Find("Datas");

        UniversalSkillType skill;
        GameObject skillButton;

        switch (tmp)
        {
            case 1:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(1, 1);
                break;
            case 2:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(1, 2);

                break;
            case 9:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(0, 0);
                break;
            default:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(0, 0);
                break;
        }

        switch (skill.GetType())
        {
            case "Active_Normal":
                skillButton = (GameObject)Instantiate(skill_1_Active_Normal);
                skillButton = skillButton.transform.Find("FirstSkill").gameObject;
                skillButton.transform.Find("FirstSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_Normal>().Inic_Active_Normal(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f));
                skillCount++;
                break;
            case "Active_WithCharges":
                skillButton = (GameObject)Instantiate(skill_1_Active_WithCharges);
                skillButton = skillButton.transform.Find("FirstSkill").gameObject;
                skillButton.transform.Find("FirstSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_WithCharges>().Inic_Active_WithCharges(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f), skill.GetCharges());
                skillCount++;
                break;
            case "Active_Dragable":
                skillButton = (GameObject)Instantiate(skill_1_Active_Dragable);
                skillButton = skillButton.transform.Find("FirstSkill").gameObject;
                skillButton.transform.Find("FirstSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_Dragable>().Inic_Active_Dragable(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f), skill.GetMarginSprite(), skill.GetMarginSpriteSize());
                skillCount++;
                break;
            default:
                break;
        }

        tmp = skillNumbers / 100 % 10;

        switch (tmp)
        {
            case 1:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(2, 1);
                break;
            case 2:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(2, 2);
                break;
            case 9:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(0, 0);
                break;
            default:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(0, 0);
                break;
        }

        switch (skill.GetType())
        {
            case "Active_Normal":
                skillButton = (GameObject)Instantiate(skill_2_Active_Normal);
                skillButton = skillButton.transform.Find("SecondSkill").gameObject;
                skillButton.transform.Find("SecondSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_Normal>().Inic_Active_Normal(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f));
                skillCount++;
                break;
            case "Active_WithCharges":
                skillButton = (GameObject)Instantiate(skill_2_Active_WithCharges);
                skillButton = skillButton.transform.Find("SecondSkill").gameObject;
                skillButton.transform.Find("SecondSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_WithCharges>().Inic_Active_WithCharges(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f), skill.GetCharges());
                skillCount++;
                break;
            case "Active_Dragable":
                skillButton = (GameObject)Instantiate(skill_2_Active_Dragable);
                skillButton = skillButton.transform.Find("SecondSkill").gameObject;
                skillButton.transform.Find("SecondSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_Dragable>().Inic_Active_Dragable(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f), skill.GetMarginSprite(), skill.GetMarginSpriteSize());
                skillCount++;
                break;
            default:
                break;
        }

        tmp = skillNumbers / 10 % 10;
        switch (tmp)
        {
            case 1:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(3, 1);
                break;
            case 2:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(3, 2);

                break;
            case 9:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(0, 0);
                break;
            default:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(0, 0);
                break;
        }

        switch (skill.GetType())
        {
            case "Active_Normal":
                skillButton = (GameObject)Instantiate(skill_3_Active_Normal);
                skillButton = skillButton.transform.Find("ThirdSkill").gameObject;
                skillButton.transform.Find("ThirdSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_Normal>().Inic_Active_Normal(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f));
                skillCount++;
                break;
            case "Active_WithCharges":
                skillButton = (GameObject)Instantiate(skill_3_Active_WithCharges);
                skillButton = skillButton.transform.Find("ThirdSkill").gameObject;
                skillButton.transform.Find("ThirdSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_WithCharges>().Inic_Active_WithCharges(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f), skill.GetCharges());
                skillCount++;
                break;
            case "Active_Dragable":
                skillButton = (GameObject)Instantiate(skill_3_Active_Dragable);
                skillButton = skillButton.transform.Find("ThirdSkill").gameObject;
                skillButton.transform.Find("ThirdSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_Dragable>().Inic_Active_Dragable(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f), skill.GetMarginSprite(), skill.GetMarginSpriteSize());
                skillCount++;
                break;
            default:
                break;
        }

        tmp = skillNumbers % 10;
        switch (tmp)
        {
            case 1:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(4, 1);
                break;
            case 2:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(4, 2);

                break;
            case 9:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(0, 0);
                break;
            default:
                skill = datas.GetComponent<SkillDatas>().GetSkillDatas(0, 0);
                break;
        }

        switch (skill.GetType())
        {
            case "Active_Normal":
                skillButton = (GameObject)Instantiate(skill_4_Active_Normal);
                skillButton = skillButton.transform.Find("FourthSkill").gameObject;
                skillButton.transform.Find("FourthSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_Normal>().Inic_Active_Normal(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f));
                skillCount++;
                break;
            case "Active_WithCharges":
                skillButton = (GameObject)Instantiate(skill_4_Active_WithCharges);
                skillButton = skillButton.transform.Find("FourthSkill").gameObject;
                skillButton.transform.Find("FourthSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_WithCharges>().Inic_Active_WithCharges(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f), skill.GetCharges());
                skillCount++;
                break;
            case "Active_Dragable":
                skillButton = (GameObject)Instantiate(skill_4_Active_Dragable);
                skillButton = skillButton.transform.Find("FourthSkill").gameObject;
                skillButton.transform.Find("FourthSpellButton").gameObject.GetComponent<Image>().sprite = skill.GetButtonPicture();
                skillButton.GetComponent<Active_Dragable>().Inic_Active_Dragable(skill.GetSkillLevel(), skill.GetSkillNumber(), skill.GetCoolDown() * (1 - GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_CooldownReduction() / 100f), skill.GetMarginSprite(), skill.GetMarginSpriteSize());
                skillCount++;
                break;
            default:
                break;
        }
    }

    public void MakeMedallionButton()
    {
        string tankTreeSpecStr = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_Health");
        string damageTreeSpecStr = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_AttackDamage");
        string specialTreeSpecStr = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_SpellPower");

        int baseFairyPowerID = 0;

        if (int.Parse(tankTreeSpecStr.Substring(0, 1)) == 1)
            baseFairyPowerID = 1;
        else
        if (int.Parse(damageTreeSpecStr.Substring(0, 1)) == 1)
            baseFairyPowerID = 2;
        else
        if (int.Parse(specialTreeSpecStr.Substring(0, 1)) == 1)
            baseFairyPowerID = 3;

        if (baseFairyPowerID != 0)
        {
            Instantiate(medallionButton);
            medallionIsAvailable = true;
        }
    }

    public void Shoot()
    {
        GameObject projectileGO;
        bool crit = false;
        if (r.NextDouble() < critChance)
            crit = true;
        switch (weaponType)
        {
            case "Staff":
                projectileGO = magicMissleGO;
                if (crit)
                    projectileGO.GetComponent<MagicMissile>().SetCrit();
                break;
            case "Bow":
                projectileGO = bowArrowGO;
                if (crit)
                    projectileGO.GetComponent<Bow_Arrow>().SetCrit();
                break;
            case "Crossbow":
                projectileGO = crossbowArrowGO;
                if (crit)
                    projectileGO.GetComponent<Bow_Arrow>().SetCrit();
                break;
            case "Boomerang":
                projectileGO = boomerangGO;
                if (crit)
                    projectileGO.GetComponent<Projectile_Boomerang>().SetCrit();
                break;
            default:
                projectileGO = magicMissleGO;
                break;
        }

        if (attackCounter >= shootingSpeed)
        {
            if (false && (r.Next(0, 100) < 15f))
            {
                GameObject projectile1 = (GameObject)Instantiate(projectileGO);
                projectile1.transform.position = shootPosition.transform.position + new Vector3(0.1f, 0f);
                GameObject projectile2 = (GameObject)Instantiate(projectileGO);
                projectile2.transform.position = shootPosition.transform.position - new Vector3(0.1f, 0f);
                attackCounter = 0;
            }
            else
            {
                GameObject projectile = (GameObject)Instantiate(projectileGO);
                projectile.transform.position = shootPosition.transform.position;
                if ((weaponType.Equals("Bow")) || (weaponType.Equals("Crossbow")))
                    projectile.GetComponent<Bow_Arrow>().SetWay(0f);
                attackCounter = 0;
            }
        }

    }

    public void Skill1()
    {
        GameObject.Find("Datas").GetComponent<AttackManager>().UseSkill(1, skills[0]);
    }

    public void Skill2()
    {
        GameObject.Find("Datas").GetComponent<AttackManager>().UseSkill(2, skills[1]);
    }

    public void Skill3()
    {
        GameObject.Find("Datas").GetComponent<AttackManager>().UseSkill(3, skills[2]);
    }

    public void Skill4()
    {
        GameObject.Find("Datas").GetComponent<AttackManager>().UseSkill(4, skills[3]);
    }

}
