using UnityEngine;
using System.Collections;

public class Bow_Arrow : MonoBehaviour {

    float speed = 2f;
    float dmg;
    bool crit;

    public GameObject damageValueCanvas;

    Vector3 velocity = Vector3.zero;

    void Start()
    {
        dmg = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_AttackDamage();
        dmg = Mathf.RoundToInt(dmg * Random.Range(0.8f, 1.2f));
        crit = false;
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

    public void MultiplyDamage(float num)
    {
        dmg = Mathf.RoundToInt(GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_AttackDamage() * num * Random.Range(0.8f, 1.2f));
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
