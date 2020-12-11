using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Firebreath : MonoBehaviour {

    float speed = 1f;
    float dmg;

    bool started = false;

    int targetHealth;

    public GameObject damageValueCanvas;

    Vector3 pos = Vector3.zero;

    void Start()
    {
        transform.position = GameObject.Find("Player").transform.position + (new Vector3(0f, 0.135f));
        dmg = GameObject.Find("Datas").GetComponent<Datas>().Get_FireBreathDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GameObject.Find("Player").GetComponent<Player_Level>().GetLevel());
        dmg += GameObject.Find("Datas").GetComponent<Datas>().Get_FireBreathDamage_Player(0) * GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_MagicPower();
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

        Vector3 playerPos = GameObject.Find("Player").transform.position;

        pos = new Vector2(pos.x, pos.y + 0.765f * speed * Time.deltaTime);

        transform.position = playerPos + (new Vector3(0f, 0.135f, 0.1f)) + pos;

        Vector2 scale = transform.localScale;

        scale = new Vector3(scale.x + speed * Time.deltaTime, scale.y + speed * Time.deltaTime);

        transform.localScale = scale;

        if (transform.localScale.x > 1f)
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
