using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagicMissile : MonoBehaviour {

    float speed = 1.5f;
    float dmg;
    bool crit;

    public GameObject damageValueCanvas;

    void Start()
    {
        dmg = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_AttackDamage();
        dmg = Mathf.RoundToInt(dmg * Random.Range(0.8f, 1.2f));
        crit = false;
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

    public void SetCrit()
    {
        crit = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            if (crit)
                dmg *= 2;
            collider.gameObject.GetComponent<DamageDealer>().DealDamage(dmg);
            Destroy(gameObject);
        }
    }
}
