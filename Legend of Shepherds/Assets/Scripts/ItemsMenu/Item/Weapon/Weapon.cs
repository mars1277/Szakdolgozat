using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : Item
{
    float attackSpeed;
    float damageQuality;
    float damagePerSec;
    bool twoHanded;

    public Weapon(string r, int l) : base(r, l)
    {
    }


    public void SetAttackSpeed(float speed)
    {
        attackSpeed = speed;
    }

    public void SetDamageQuality(float quality)
    {
        damageQuality = quality;
    }

    public void SetDamagePerSec(float dps)
    {
        damagePerSec = dps;
    }

    public void SetTwoHanded(bool th)
    {
        twoHanded = th;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetDamageQuality()
    {
        return damageQuality;
    }

    public float GetDamagePerSec()
    {
        return damagePerSec * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GetLevel()) * (1f + GetStar() * 0.2f);
    }

    public int GetAttackDamage()
    {
        return Mathf.RoundToInt(GetDamagePerSec() * attackSpeed);
    }
    public bool GetTwoHanded()
    {
        return twoHanded;
    }

    public override string GetItemType()
    {
        return "Weapon";
    }

    public virtual string GetWeaponType()
    {
        return "";
    }
}
