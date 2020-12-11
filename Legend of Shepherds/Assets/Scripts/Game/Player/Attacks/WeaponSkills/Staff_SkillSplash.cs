using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff_SkillSplash : MonoBehaviour {

    float speed = 0.8f;
    float dmg;

    bool started = false;

    public GameObject damageValueCanvas;

    void Start () {
        dmg = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_AttackDamage() + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_MagicPower() * 0.2f;
        dmg *= 1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusWeaponSkillDmg() / 100f;
    }

    void Update () {
        if (!started)
        {
            transform.localScale = Vector3.zero;
            started = true;
        }

        Vector2 scale = transform.localScale;

        scale = new Vector3(scale.x + speed * Time.deltaTime, scale.y + speed * Time.deltaTime);

        transform.localScale = scale;

        if (transform.localScale.x > 0.5f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            int tmpDmg = Mathf.RoundToInt(dmg * Random.Range(0.8f, 1.2f));
            collider.gameObject.GetComponent<DamageDealer>().DealDamage(tmpDmg);
        }
    }
}
