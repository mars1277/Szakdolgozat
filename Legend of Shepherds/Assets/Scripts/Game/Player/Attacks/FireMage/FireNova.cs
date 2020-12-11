using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireNova : MonoBehaviour {

    float speed = 0.8f;
    float dmg;

    bool started = false;

    int targetHealth;

    public GameObject damageValueCanvas;

    void Start()
    {
        transform.position = GameObject.Find("Player").transform.position + (new Vector3(0f, 0.135f));
        dmg = GameObject.Find("Datas").GetComponent<Datas>().Get_FireNovaDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GameObject.Find("Player").GetComponent<Player_Level>().GetLevel());
        dmg += GameObject.Find("Datas").GetComponent<Datas>().Get_FireNovaDamage_Player(0) * GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_MagicPower();
        dmg *= (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMagicDamage() / 100f);
        dmg *= (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusAllDmgDone() / 100f);

        Vector3 pos = GameObject.Find("Player").transform.position + new Vector3(0.12f, -0.06f, 2f);
        transform.position = pos;
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

        if (transform.localScale.x > 0.6f)
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
