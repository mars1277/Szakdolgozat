using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Boomerang : MonoBehaviour
{

    float speed = 3.0f;

    float dmg;
    bool crit;
    bool turnedBack;

    public GameObject damageValueCanvas;

    float rotationCounter;
    float rotationSpeed = 1.5f;
    Vector3 velocity = Vector3.zero;

    float flightTimer;

    void Start()
    {
        dmg = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_AttackDamage();
        dmg = Mathf.RoundToInt(dmg * Random.Range(0.8f, 1.2f));
        crit = false;
        turnedBack = false;
        velocity = new Vector3(0f, 1f);
        rotationCounter = 0.0f;
        flightTimer = 0.0f;
    }

    void Update()
    {
        Vector3 shootingPos = GameObject.Find("ShootPosition").transform.position;
        rotationCounter += Time.deltaTime;
        flightTimer += Time.deltaTime;

        //rotation
        float rotation = (rotationCounter % (1f / rotationSpeed)) * rotationSpeed * 360f;
        Quaternion rot = Quaternion.Euler(0f, 0f, rotation);
        gameObject.transform.rotation = rot;

        if (flightTimer < 0.5f)
            transform.position += velocity * speed * Time.deltaTime;
        else if (flightTimer < 0.7f)
        {
            transform.position += velocity * speed * Time.deltaTime * (0.7f - flightTimer) * 5f;
        }
        else if (flightTimer < 0.9f)
        {
            if (!turnedBack)
            {
                turnedBack = true;
            }
            velocity = (shootingPos - transform.position).normalized;
            transform.position += velocity * speed * Time.deltaTime * (flightTimer - 0.7f) * 5f;
        }
        else
        {
            velocity = (shootingPos - transform.position).normalized;
            transform.position += velocity * speed * Time.deltaTime;
        }


        if ((flightTimer > 0.5f) && (Vector3.Distance(shootingPos, transform.position) < 0.05f))
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
        }
    }
}
