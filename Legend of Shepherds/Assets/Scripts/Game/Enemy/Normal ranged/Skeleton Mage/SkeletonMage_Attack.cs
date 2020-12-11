using UnityEngine;
using System.Collections;

public class SkeletonMage_Attack : MonoBehaviour {

    GameObject player;
    public GameObject missileGO;
    public GameObject shootingPosition_1;
    public GameObject shootingPosition_2;

    float attackTimer;
    float attackSpeed;

    int star;

    void Start()
    {
        player = GameObject.Find("Player");
        attackSpeed = 3f;
        attackTimer = -4f;
        star = 1;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer > attackSpeed)
        {
            ShootLeftHandMissile();
            ShootRightHandMissile();

            attackTimer = 0f;
        }

    }

    public void ShootLeftHandMissile()
    {
        //shoot_1
        Vector3 diff = player.transform.position - shootingPosition_1.transform.position;
        float rotation = 0f;
        //rotation
        if (diff.x == 0f)
        {
            if (diff.y <= 0f)
                rotation = 0f;
            if (diff.y > 0f)
                rotation = 180f;
        }
        if (diff.x < 0f)
        {
            if (diff.y < 0f)
                rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI;
            if (diff.y == 0f)
            {
                rotation = 0f;
            }
            if (diff.y > 0f)
                rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI + 180f;
        }

        if (diff.x > 0f)
        {
            if (diff.y < 0f)
                rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI;
            if (diff.y == 0f)
            {
                rotation = 0f;
            }
            if (diff.y > 0f)
                rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI + 180f;
        }

        Quaternion rot = Quaternion.Euler(0f, 0f, rotation);
        Vector3 vec = new Vector3(Mathf.Sin(-rotation * Mathf.PI / 180f), Mathf.Cos(-rotation * Mathf.PI / 180f));

        vec = (vec.magnitude > 1) ? vec.normalized : vec;

        GameObject missile_1 = (GameObject)Instantiate(missileGO);
        missile_1.GetComponent<SkeletonMage_Missile>().SetWay(vec);
        missile_1.GetComponent<SkeletonMage_Missile>().SetStar(star);
        missile_1.transform.position = shootingPosition_1.transform.position + new Vector3(-vec.x * 0.06f, -vec.y * 0.06f);
        missile_1.transform.rotation = rot;
    }

    public void ShootRightHandMissile()
    {
        //shoot_2
        Vector3 diff = player.transform.position - shootingPosition_2.transform.position;
        float rotation = 0f;
        //rotation
        if (diff.x == 0f)
        {
            if (diff.y <= 0f)
                rotation = 0f;
            if (diff.y > 0f)
                rotation = 180f;
        }
        if (diff.x < 0f)
        {
            if (diff.y < 0f)
                rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI;
            if (diff.y == 0f)
            {
                rotation = 0f;
            }
            if (diff.y > 0f)
                rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI + 180f;
        }

        if (diff.x > 0f)
        {
            if (diff.y < 0f)
                rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI;
            if (diff.y == 0f)
            {
                rotation = 0f;
            }
            if (diff.y > 0f)
                rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI + 180f;
        }

        Quaternion rot = Quaternion.Euler(0f, 0f, rotation);
        Vector3 vec = new Vector3(Mathf.Sin(-rotation * Mathf.PI / 180f), Mathf.Cos(-rotation * Mathf.PI / 180f));

        vec = (vec.magnitude > 1) ? vec.normalized : vec;

        GameObject missile_2 = (GameObject)Instantiate(missileGO);
        missile_2.GetComponent<SkeletonMage_Missile>().SetWay(vec);
        missile_2.GetComponent<SkeletonMage_Missile>().SetStar(star);
        missile_2.transform.position = shootingPosition_2.transform.position + new Vector3(-vec.x * 0.06f, -vec.y * 0.06f);
        missile_2.transform.rotation = rot;
    }

    public void SetStar(int s)
    {
        star = s;
    }
}
