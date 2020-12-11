using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Active_Normal : MonoBehaviour {

    string type;
    int skillLevel;
    int skillNumber;

    float cooldown;
    float cooldownTimer;
    public Image skillCdShadow;

    bool skillReady;

    void Start()
    {
        type = "Active_Normal";
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

    public void Inic_Active_Normal(int sl, int sn, float cd)
    {
        skillLevel = sl;
        skillNumber = sn;
        cooldown = cd;
    }

    public new string GetType()
    {
        return type;
    }

    public float GetCoolDown()
    {
        return cooldown;
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
            skillReady = false;
            cooldownTimer = 0f;
        }
    }
}
