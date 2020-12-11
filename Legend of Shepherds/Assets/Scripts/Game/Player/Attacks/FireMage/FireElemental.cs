using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireElemental : MonoBehaviour {

    public Image beamImage;

    GameObject[] enemies;
    GameObject aimedEnemy;
    int index;
    float closestEnemyPos;

    float attackTimer;
    float lifeSpanTimer;

    float beamTimer;
    bool beamFired;
    float beamLength;

    float dmg;

    void Start () {
        index = FindClosestEnemy();
        attackTimer = 0f;
        lifeSpanTimer = 0f;

        beamFired = false;

        dmg = GameObject.Find("Datas").GetComponent<Datas>().Get_FireElementalDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GameObject.Find("Player").GetComponent<Player_Level>().GetLevel());
        dmg += GameObject.Find("Datas").GetComponent<Datas>().Get_FireElementalDamage_Player(0) * GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_MagicPower();
        dmg *= (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMagicDamage() / 100f);
        dmg *= (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusAllDmgDone() / 100f);

        if((Screen.height / (float)Screen.width) > 2)
        { 
            beamImage.rectTransform.position = beamImage.rectTransform.position + new Vector3(0, beamImage.rectTransform.position.y * 0.25f, 0);
        }
    }
	
	void Update () {
        index = FindClosestEnemy();
        attackTimer += Time.deltaTime;
        beamTimer += Time.deltaTime;
        lifeSpanTimer += Time.deltaTime;

        if(index != -1)
        {
            if(attackTimer > 1f)
            {
                if (enemies[index].transform.position.y < 2.35f)
                {
                    aimedEnemy = enemies[index];

                    //shoot
                    Vector3 diff = enemies[index].transform.position - new Vector3(-0.1875f, -1.6275f);

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
                            rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI + 180f;
                        if (diff.y == 0f)
                        {
                            rotation = 0f;
                        }
                        if (diff.y > 0f)
                            rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI;
                    }

                    if (diff.x > 0f)
                    {
                        if (diff.y < 0f)
                            rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI + 180f;
                        if (diff.y == 0f)
                        {
                            rotation = 0f;
                        }
                        if (diff.y > 0f)
                            rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI;
                    }
                    Quaternion rot = Quaternion.Euler(0f, 0f, rotation);
                    beamImage.transform.rotation = rot;

                    //length
                    beamLength = Mathf.Sqrt(diff.x * diff.x + diff.y * diff.y * (((Screen.height / (float)Screen.width) > 2) ? 1.25f * 1.25f : 1)) * 1.4f;

                    beamFired = true;
                    beamTimer = 0f;

                    attackTimer = 0f;
                }
            }
        }

        if (beamFired)
        {
            if(beamTimer >= 0.5f)
            {
                beamImage.transform.localScale = new Vector3(1f, 0f);
                beamFired = false;
                if(aimedEnemy != null)
                    aimedEnemy.gameObject.GetComponent<DamageDealer>().DealDamage(Mathf.RoundToInt(dmg * Random.Range(0.8f, 1.2f)));
            }
            else
            {
                float length = beamLength * beamTimer * 2f;
                beamImage.transform.localScale = new Vector3(1f, length);
            }
        }

        if (lifeSpanTimer > 20.9f)
        {
            Destroy(gameObject);
        }
    }


    public int FindClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            int idx = 0;
            closestEnemyPos = enemies[0].transform.position.y;
            for (int i = 1; i < enemies.Length; i++)
                if (enemies[i].transform.position.y < closestEnemyPos)
                {
                    idx = i;
                    closestEnemyPos = enemies[i].transform.position.y;
                }
            return idx;
        }

        else return -1;
    }
}
