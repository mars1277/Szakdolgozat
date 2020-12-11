using UnityEngine;
using System.Collections;

public class SkeletonMageBoss_Bomb : MonoBehaviour {

    public GameObject missileGO;

    float counter;

    float rotationSpeed = 0.2f;
    float baseDmg;
    float dmg;

    int level;

    Vector3 scale;

    int shotCounter;

    void Start () {
        counter = 0f;
        shotCounter = 0;

        baseDmg = 5000;
        dmg = baseDmg;
        gameObject.transform.localScale = new Vector3(0f, 0f);
        scale = new Vector3(0f, 0f);
    }

    public void SetLevel(int l)
    {
        level = l;
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
    }

    void Update()
    {
        counter += Time.deltaTime;

        //rotation
        float rotation = (counter % (1f / rotationSpeed)) * rotationSpeed * 360f;
        Quaternion rot = Quaternion.Euler(0f, 0f, rotation);
        gameObject.transform.rotation = rot;

        //movement
        if (counter < 4f)
        {
            scale += new Vector3(Time.deltaTime / 4f, Time.deltaTime / 4f);
            gameObject.transform.localScale = scale;
        }
        else
        if (counter < 12f)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f);
            transform.position += new Vector3(0f, -1f) * 0.2f * Time.deltaTime;

            switch (shotCounter)
            {
                case 0:
                    if(counter >= 5f)
                    {
                        ShootMissiles();
                        shotCounter++;
                    }
                    break;
                case 1:
                    if (counter >= 7f)
                    {
                        ShootMissiles();
                        shotCounter++;
                    }
                    break;
                case 2:
                    if (counter >= 9f)
                    {
                        ShootMissiles();
                        shotCounter++;
                    }
                    break;
                case 3:
                    if (counter >= 11f)
                    {
                        ShootMissiles();
                        shotCounter++;
                    }
                    break;
                default:
                    break;
            }
        }
        else
        if (counter < 14f)
        {
            scale -= new Vector3(Time.deltaTime / 2f, Time.deltaTime / 2f);
            gameObject.transform.localScale = scale;
            transform.position += new Vector3(0f, -1f) * 0.2f * Time.deltaTime;

        }
        else
        if(counter >= 14f)
        {
            gameObject.transform.localScale = new Vector3(0f, 0f);
            Destroy(gameObject);
        }
    }

    public void ShootMissiles()
    {
        float rotation = Random.Range(0f, 20f);
        MakeMissile(rotation);
        MakeMissile(rotation + 20f);
        MakeMissile(rotation + 40f);
        MakeMissile(rotation + 60f);
        MakeMissile(rotation + 80f);
        MakeMissile(rotation + 100f);
        MakeMissile(rotation + 120f);
        MakeMissile(rotation + 140f);
        MakeMissile(rotation + 160f);
        MakeMissile(rotation + 180f);
        MakeMissile(rotation + 200f);
        MakeMissile(rotation + 220f);
        MakeMissile(rotation + 240f);
        MakeMissile(rotation + 260f);
        MakeMissile(rotation + 280f);
        MakeMissile(rotation + 300f);
        MakeMissile(rotation + 320f);
        MakeMissile(rotation + 340f);
    }

    public void MakeMissile(float rotation)
    {
        Quaternion rot = Quaternion.Euler(0f, 0f, rotation);
        Vector3 vec = new Vector3(Mathf.Sin(-rotation * Mathf.PI / 180f), Mathf.Cos(-rotation * Mathf.PI / 180f));

        vec = (vec.magnitude > 1) ? vec.normalized : vec;

        GameObject missile = (GameObject)Instantiate(missileGO);
        missile.GetComponent<SkeletonMageBoss_Missile>().SetWay(vec);
        missile.GetComponent<SkeletonMageBoss_Missile>().SetLevel(level);
        missile.transform.position = gameObject.transform.position + new Vector3(-vec.x * 0.1f, -vec.y * 0.1f);
        missile.transform.rotation = rot;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name.Equals("Player"))
        {
            collider.GetComponent<Player_Health>().DealDamage(Mathf.RoundToInt(dmg * Random.Range(0.8f, 1.2f)));
        }
    }

    public void BossDied()
    {
        counter = 14f - scale.x * 2f;
    }
}
