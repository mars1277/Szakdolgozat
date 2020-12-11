using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fireball : MonoBehaviour
{

    float speed = 1.2f;
    float dmg;

    int targetHealth;

    public GameObject damageValueCanvas;

    void Start()
    {
        transform.position = GameObject.Find("ShootPosition").transform.position;
        dmg = GameObject.Find("Datas").GetComponent<Datas>().Get_FireBallDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GameObject.Find("Player").GetComponent<Player_Level>().GetLevel());
        dmg += GameObject.Find("Datas").GetComponent<Datas>().Get_FireBallDamage_Player(0) * GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_MagicPower();
        dmg *= (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMagicDamage() / 100f);
        dmg *= (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusAllDmgDone() / 100f);
    }

    void Update()
    {
        Vector2 pos = transform.position;

        pos = new Vector2(pos.x, pos.y + speed * Time.deltaTime);

        transform.position = pos;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.gameObject.GetComponent<DamageDealer>().DealDamage(Mathf.RoundToInt(dmg * Random.Range(0.8f, 1.2f)));
            Destroy(gameObject);
        }
    }
}
