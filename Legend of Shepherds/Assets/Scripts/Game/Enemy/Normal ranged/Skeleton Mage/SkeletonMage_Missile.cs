using UnityEngine;
using System.Collections;

public class SkeletonMage_Missile : MonoBehaviour {

    float counter;

    public GameObject skeletonMageGO;

    Vector3 velocity = Vector3.zero;
    float forwardSpeed = 1f;
    int baseDmg;
    float dmg;

    int level;
    int star;
    bool starBonusesSet;
    bool starNumberSet;

    void Start()
    {
        counter = 0f;

        level = int.Parse(gameObject.name.Substring(25, 1));
        baseDmg = 2100;
        dmg = baseDmg * (int)Mathf.Pow(10, level - 1);
    }

    void Update()
    {
        if (starNumberSet && !starBonusesSet)
        {
            dmg *= (int)Mathf.Pow(2, star - 1);
            dmg = Mathf.RoundToInt(dmg * Random.Range(0.8f, 1.2f));
            starBonusesSet = true;
        }
        counter += Time.deltaTime;

        transform.position += -velocity * forwardSpeed * Time.deltaTime;
    }

    public void SetWay(Vector3 vec)
    {
        velocity = vec;
    }

    public void SetStar(int s)
    {
        star = s;
        starNumberSet = true;
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
