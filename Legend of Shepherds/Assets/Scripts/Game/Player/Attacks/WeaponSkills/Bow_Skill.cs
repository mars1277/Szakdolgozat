using UnityEngine;
using System.Collections;

public class Bow_Skill : MonoBehaviour {

    public GameObject arrow;
    GameObject shootPosition;

    void Start () {
        shootPosition = GameObject.Find("ShootPosition");
        float damageMultiplier = 2f;
        damageMultiplier *= 1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusWeaponSkillDmg() / 100f;

        GameObject arrow_1 = Instantiate(arrow);
        arrow_1.GetComponent<Bow_Arrow>().MultiplyDamage(damageMultiplier);
        arrow_1.transform.position = shootPosition.transform.position;
        arrow_1.GetComponent<Bow_Arrow>().SetWay(20f);
        GameObject arrow_2 = Instantiate(arrow);
        arrow_2.GetComponent<Bow_Arrow>().MultiplyDamage(damageMultiplier);
        arrow_2.GetComponent<Bow_Arrow>().SetWay(10f);
        arrow_2.transform.position = shootPosition.transform.position;
        GameObject arrow_3 = Instantiate(arrow);
        arrow_3.GetComponent<Bow_Arrow>().MultiplyDamage(damageMultiplier);
        arrow_3.GetComponent<Bow_Arrow>().SetWay(0f);
        arrow_3.transform.position = shootPosition.transform.position;
        GameObject arrow_4 = Instantiate(arrow);
        arrow_4.GetComponent<Bow_Arrow>().MultiplyDamage(damageMultiplier);
        arrow_4.GetComponent<Bow_Arrow>().SetWay(-10f);
        arrow_4.transform.position = shootPosition.transform.position;
        GameObject arrow_5 = Instantiate(arrow);
        arrow_5.GetComponent<Bow_Arrow>().MultiplyDamage(damageMultiplier);
        arrow_5.GetComponent<Bow_Arrow>().SetWay(-20f);
        arrow_5.transform.position = shootPosition.transform.position;
        Destroy(gameObject);
    }
}
