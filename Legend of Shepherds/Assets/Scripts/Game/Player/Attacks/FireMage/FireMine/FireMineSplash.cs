using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireMineSplash : MonoBehaviour {

    float speed = 0.6f;
    float dmg;

    bool started = false;

    public GameObject damageValueCanvas;

    void Start()
    {
        dmg = GameObject.Find("Datas").GetComponent<Datas>().Get_FireMineDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GameObject.Find("Player").GetComponent<Player_Level>().GetLevel());
        dmg += GameObject.Find("Datas").GetComponent<Datas>().Get_FireMineDamage_Player(0) * GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_MagicPower();
        dmg *= (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMagicDamage() / 100f);
        dmg *= (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusAllDmgDone() / 100f);
    }

    void Update()
    {
        if (!started)
        {
            transform.localScale = Vector3.zero;
            started = true;
        }

        Vector2 scale = transform.localScale;

        scale = new Vector3(scale.x + speed * Time.deltaTime, scale.y + speed * Time.deltaTime);

        transform.localScale = scale;

        if (transform.localScale.x > 0.3f)
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
