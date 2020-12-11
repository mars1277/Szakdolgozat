using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow_Skill : MonoBehaviour {

    float speed = 2f;
    float dmg;

    public GameObject damageValueCanvas;

    Vector3 velocity = Vector3.zero;

    void Start()
    {
        GameObject shootPosition = GameObject.Find("ShootPosition");
        velocity = new Vector3(0f, -1f);
        transform.position = shootPosition.transform.position;
        dmg = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_AttackDamage() * 2f;
        dmg *= 1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusWeaponSkillDmg() / 100f;
    }

    void Update()
    {
        transform.position += -velocity * speed * Time.deltaTime;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1.03f, 1.03f));

        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    public void SetWay(float rot)
    {
        rot = rot + 180f;
        Quaternion rotation = Quaternion.Euler(0f, 0f, rot);
        transform.rotation = rotation;

        Vector3 vec = new Vector3(Mathf.Sin(-rot * Mathf.PI / 180f), Mathf.Cos(-rot * Mathf.PI / 180f));

        vec = (vec.magnitude > 1) ? vec.normalized : vec;
        velocity = vec;
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
