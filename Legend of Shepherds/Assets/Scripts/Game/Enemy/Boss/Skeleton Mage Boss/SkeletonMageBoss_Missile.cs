using UnityEngine;
using System.Collections;

public class SkeletonMageBoss_Missile : MonoBehaviour {

    float counter;

    Vector3 velocity = Vector3.zero;
    float baseDmg;
    float dmg;

    void Start()
    {
        counter = 0f;
        baseDmg = 2000;
        dmg = baseDmg;
    }

    public void SetLevel(int level)
    {
        switch (level)
        {
            case 1:
                dmg = baseDmg;
                break;
            case 2:
                dmg = baseDmg * 7;
                break;
            case 3:
                dmg = baseDmg * 35;
                break;
        }
        dmg = Mathf.RoundToInt(dmg * Random.Range(0.8f, 1.2f));
    }

    void Update()
    {
        counter += Time.deltaTime;

        transform.position += -velocity * 1.2f * Time.deltaTime;
        if (counter > 10f)
            Destroy(gameObject);
    }

    public void SetWay(Vector3 vec)
    {
        velocity = vec;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name.Equals("Player"))
        {
            collider.GetComponent<Player_Health>().DealDamage(dmg);
            Destroy(gameObject);
        }
    }
}
