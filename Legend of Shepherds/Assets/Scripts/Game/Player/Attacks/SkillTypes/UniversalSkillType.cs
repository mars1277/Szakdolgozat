using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UniversalSkillType {

    Sprite buttonPicture;

    string type;
    int skillLevel;
    int skillNumber;

    //Active

    //Normal
    float cooldown;
    
    //With Charges
    int charges;

    //Dragable
    Sprite margin;
    Vector2 marginSize;

    public UniversalSkillType(int sl, int sn, string t, float cd, Sprite bs)
    {
        skillLevel = sl;
        skillNumber = sn;
        type = t;
        cooldown = cd;
        buttonPicture = bs;
    }

    public UniversalSkillType(int sl, int sn, string t, float cd, int ch, Sprite bs)
    {
        skillLevel = sl;
        skillNumber = sn;
        type = t;
        cooldown = cd;
        charges = ch;
        buttonPicture = bs;
    }

    public UniversalSkillType(int sl, int sn, string t, float cd, Sprite m, Vector2 ms, Sprite bs)
    {
        skillLevel = sl;
        skillNumber = sn;
        type = t;
        cooldown = cd;
        margin = m;
        buttonPicture = bs;
        marginSize = ms;
    }

    public new string GetType()
    {
        return type;
    }

    public int GetSkillLevel()
    {
        return skillLevel;
    }

    public int GetSkillNumber()
    {
        return skillNumber;
    }

    public float GetCoolDown()
    {
        return cooldown;
    }

    public int GetCharges()
    {
        return charges;
    }

    public Sprite GetMarginSprite()
    {
        return margin;
    }

    public Vector2 GetMarginSpriteSize()
    {
        return marginSize;
    }

    public Sprite GetButtonPicture()
    {
        return buttonPicture;
    }
}
