using UnityEngine;
using System.Collections;

public class SkeletonMageBoss_Attack : MonoBehaviour {

    GameObject player;
    public GameObject missileGO;
    public GameObject bombGO;
    public GameObject shootingPosition_1;
    public GameObject shootingPosition_2;
    public GameObject shootingPosition_3;

    float attackTimer_Missile;
    float attackSpeed_Missile;
    int missileCounter;

    bool forwardMovementDone;
    bool shotBomb;

    int level;

    void Start()
    {
        forwardMovementDone = false;
        shotBomb = false;
        player = GameObject.Find("Player");
        attackSpeed_Missile = 5f;
        attackTimer_Missile = -6f;
    }

    public void SetLevel(int l)
    {
        level = l;
    }

    void Update()
    {
        if (!forwardMovementDone)
            forwardMovementDone = gameObject.GetComponent<SkeletonMageBoss_Movement>().ForwardMovementDone();
        else
        {
            if (!gameObject.GetComponent<SkeletonMageBoss_Movement>().StoppedToCastBomb())
            {
                shotBomb = false;
                attackTimer_Missile += Time.deltaTime;

                if ((attackTimer_Missile > attackSpeed_Missile) && (missileCounter < 7))
                {
                    ShootLeftHandMissile();
                    ShootRightHandMissile();
                    attackTimer_Missile = 0f;
                    missileCounter++;
                }
            }
            else
            {
                if (!shotBomb)
                {
                    ShootBomb();
                    attackTimer_Missile = -6f;
                    missileCounter = 0;
                    shotBomb = true;
                }
            }
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

        rotation -= 3f;

        MakeMissile(rotation, shootingPosition_1);
        MakeMissile(rotation + 20f, shootingPosition_1);
        MakeMissile(rotation - 20f, shootingPosition_1);
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

        rotation += 3f;

        MakeMissile(rotation, shootingPosition_2);
        MakeMissile(rotation + 20f, shootingPosition_2);
        MakeMissile(rotation - 20f, shootingPosition_2);
    }

    public void MakeMissile(float rotation, GameObject shootingPosition)
    {
        Quaternion rot = Quaternion.Euler(0f, 0f, rotation);
        Vector3 vec = new Vector3(Mathf.Sin(-rotation * Mathf.PI / 180f), Mathf.Cos(-rotation * Mathf.PI / 180f));

        vec = (vec.magnitude > 1) ? vec.normalized : vec;

        GameObject missile = (GameObject)Instantiate(missileGO);
        missile.GetComponent<SkeletonMageBoss_Missile>().SetWay(vec);
        missile.GetComponent<SkeletonMageBoss_Missile>().SetLevel(level);
        missile.transform.position = shootingPosition.transform.position + new Vector3(-vec.x * 0.06f, -vec.y * 0.06f);
        missile.transform.rotation = rot;
    }

    public void ShootBomb()
    {
        GameObject bomb = (GameObject)Instantiate(bombGO);
        bomb.GetComponent<SkeletonMageBoss_Bomb>().SetLevel(level);
        bomb.transform.position = shootingPosition_3.transform.position;
    }

}
