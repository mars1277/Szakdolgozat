using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Active_Dragable : MonoBehaviour {

    string type;
    int skillLevel;
    int skillNumber;

    float cooldown;
    float cooldownTimer;
    public Image skillCdShadow;

    bool skillReady;

    public GameObject dragArea;
    public Image margin;
    Vector2 marginSize;

    void Start()
    {
        type = "Active_Dragable";
        cooldownTimer = 1000f;
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer > cooldown)
            skillReady = true;

        float skillCdShadowCounter;
        if (cooldownTimer >= cooldown)
            skillCdShadowCounter = 1;
        else
            skillCdShadowCounter = cooldownTimer / cooldown;

        skillCdShadow.fillAmount = 1 - skillCdShadowCounter;
    }

    public void Inic_Active_Dragable(int sl, int sn, float cd, Sprite skillMarginSprite, Vector2 ms)
    {
        skillLevel = sl;
        skillNumber = sn;
        cooldown = cd;
        margin.sprite = skillMarginSprite;
        marginSize = ms;
        margin.rectTransform.sizeDelta = ms;
    }

    public new string GetType()
    {
        return type;
    }

    public float GetCoolDown()
    {
        return cooldown;
    }

    public Sprite GetMarginSprite()
    {
        return margin.sprite;
    }

    public Vector2 GetMarginSpriteSize()
    {
        return marginSize;
    }

    public bool SkillReady()
    {
        return skillReady;
    }

    public void SkillUsed()
    {
        Vector3 pos = dragArea.GetComponent<Active_Dragable_DragArea>().GetSkillUsedPosition();
        GameObject attack = GameObject.Find("Datas").GetComponent<AttackManager>().UseSkill(skillLevel, skillNumber);
        attack.transform.position = pos;
        skillReady = false;
        cooldownTimer = 0f;
    }
}
