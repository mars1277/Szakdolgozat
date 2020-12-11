using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Active_WithCharges : MonoBehaviour {

    string type;
    int skillLevel;
    int skillNumber;

    float cooldown;
    float cooldownTimer;

    int charges;
    int chargesCounter;
    public Image skillCdShadow;

    bool skillReady;

    public Image charge_1;
    public Image charge_2;
    public Image charge_3;

    void Start()
    {
        type = "Active_Normal";
        cooldownTimer = 1000f;
        chargesCounter = charges;
        skillReady = true;
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        float skillCdShadowCounter;
        if (cooldownTimer >= cooldown)
            skillCdShadowCounter = 1;
        else
            skillCdShadowCounter = cooldownTimer / cooldown;

        skillCdShadow.fillAmount = 1 - skillCdShadowCounter;

        if (cooldownTimer >= cooldown)
        {
            if (chargesCounter == 0)
            {
                chargesCounter++;
                skillReady = true;
                cooldownTimer = 0f;
            }
            else
            if (chargesCounter < charges)
            {
                chargesCounter++;
                cooldownTimer = 0f;
            }
        }
        else
        if (chargesCounter == 0)
        {
            skillReady = false;
            skillCdShadow.color = new Color(1f, 1f, 1f, 1f);
        }
        else
            skillCdShadow.color = new Color(1f, 1f, 1f, 0.6f);

        switch (chargesCounter)
        {
            case 0:
                charge_1.color = new Color(1f, 1f, 1f, 0f);
                charge_2.color = new Color(1f, 1f, 1f, 0f);
                charge_3.color = new Color(1f, 1f, 1f, 0f);
                break;
            case 1:
                charge_1.color = new Color(1f, 1f, 1f, 1f);
                charge_2.color = new Color(1f, 1f, 1f, 0f);
                charge_3.color = new Color(1f, 1f, 1f, 0f);
                break;
            case 2:
                charge_1.color = new Color(1f, 1f, 1f, 1f);
                charge_2.color = new Color(1f, 1f, 1f, 1f);
                charge_3.color = new Color(1f, 1f, 1f, 0f);
                break;
            case 3:
                charge_1.color = new Color(1f, 1f, 1f, 1f);
                charge_2.color = new Color(1f, 1f, 1f, 1f);
                charge_3.color = new Color(1f, 1f, 1f, 1f);
                break;
            default:
                charge_1.color = new Color(1f, 1f, 1f, 0f);
                charge_2.color = new Color(1f, 1f, 1f, 0f);
                charge_3.color = new Color(1f, 1f, 1f, 0f);
                break;
        }
    }

    public void Inic_Active_WithCharges(int sl, int sn, float cd, int c)
    {
        skillLevel = sl;
        skillNumber = sn;
        cooldown = cd;
        charges = c;
        chargesCounter = c;
    }

    public new string GetType()
    {
        return type;
    }

    public float GetCoolDown()
    {
        return cooldown;
    }

    public int GetCharges()
    {
        return charges;
    }

    public bool SkillReady()
    {
        return skillReady;
    }

    public void UseSkill()
    {
        if (skillReady)
        {
            GameObject attack = GameObject.Find("Datas").GetComponent<AttackManager>().UseSkill(skillLevel, skillNumber);
            if(chargesCounter == 3)
            {
                cooldownTimer = 0f;
            }
            chargesCounter--;
        }
    }
}
