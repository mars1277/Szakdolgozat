using UnityEngine;
using System.Collections;

public class SkeletonMageBoss_MinionSpawn : MonoBehaviour {

    float timeCounter;
    float timeToSpawn;

    float min = -1f;
    float max = 1f;

    bool forwardMovementDone;

	void Start () {
        timeCounter = 0f;
	}

    void Update()
    {
        if (!forwardMovementDone)
            forwardMovementDone = gameObject.GetComponent<SkeletonMageBoss_Movement>().ForwardMovementDone();
        else
        {
            timeCounter += Time.deltaTime;
            if(timeCounter >= timeToSpawn)
            {
                float random = Random.Range(0f, 12f);
                GameObject enemy = new GameObject();
                if(random <= 4f)
                {
                    GameObject.Find("Datas").GetComponent<DayManager>().enemyNumber++;
                    GameObject.Find("Datas").GetComponent<DayManager>().enemyCounter++;
                    enemy = (GameObject)Instantiate(GameObject.Find("Datas").GetComponent<EnemyGOs>().Goblin_Level3_GO);
                    enemy.GetComponent<Goblin_Health>().SetStar(3);
                    Vector3 pos = GameObject.Find("Datas").GetComponent<EnemyGOs>().spawnPointGO.transform.position;
                    pos.x = Random.Range(min, max);
                    enemy.transform.position = pos;
                    timeToSpawn += 6f + Random.Range(0f, 4f);
                }
                else
                if(random <= 7f)
                {
                    GameObject.Find("Datas").GetComponent<DayManager>().enemyNumber++;
                    GameObject.Find("Datas").GetComponent<DayManager>().enemyCounter++;
                    enemy = (GameObject)Instantiate(GameObject.Find("Datas").GetComponent<EnemyGOs>().Archer_Level3_GO);
                    enemy.GetComponent<Archer_Health>().SetStar(1);
                    Vector3 pos = GameObject.Find("Datas").GetComponent<EnemyGOs>().spawnPointGO.transform.position;
                    pos.x = Random.Range(min, max);
                    enemy.transform.position = pos;
                    timeToSpawn += 6f + Random.Range(0f, 4f);
                }
                else
                if(random <= 9f)
                {
                    GameObject.Find("Datas").GetComponent<DayManager>().enemyNumber++;
                    GameObject.Find("Datas").GetComponent<DayManager>().enemyCounter++;
                    enemy = (GameObject)Instantiate(GameObject.Find("Datas").GetComponent<EnemyGOs>().Zombie_Level2_GO);
                    enemy.GetComponent<Zombie_Health>().SetStar(3);
                    Vector3 pos = GameObject.Find("Datas").GetComponent<EnemyGOs>().spawnPointGO.transform.position;
                    pos.x = Random.Range(min, max);
                    enemy.transform.position = pos;
                    timeToSpawn += 8f + Random.Range(0f, 4f);
                }
                else
                if(random <= 11f)
                {
                    GameObject.Find("Datas").GetComponent<DayManager>().enemyNumber++;
                    GameObject.Find("Datas").GetComponent<DayManager>().enemyCounter++;
                    enemy = (GameObject)Instantiate(GameObject.Find("Datas").GetComponent<EnemyGOs>().SkeletonMage_Level1_GO);
                    enemy.GetComponent<SkeletonMage_Health>().SetStar(3);
                    Vector3 pos = GameObject.Find("Datas").GetComponent<EnemyGOs>().spawnPointGO.transform.position;
                    pos.x = Random.Range(min, max);
                    enemy.transform.position = pos;

                    timeToSpawn += 6f + Random.Range(0f, 4f);
                }
                else
                if(random <= 12f)
                {
                    GameObject.Find("Datas").GetComponent<DayManager>().enemyNumber++;
                    GameObject.Find("Datas").GetComponent<DayManager>().enemyCounter++;
                    enemy = (GameObject)Instantiate(GameObject.Find("Datas").GetComponent<EnemyGOs>().Bat_Level2_GO);
                    enemy.GetComponent<Bat_Health>().SetStar(1);
                    Vector3 pos = GameObject.Find("Datas").GetComponent<EnemyGOs>().spawnPointGO.transform.position;
                    pos.x = Random.Range(min, max);
                    enemy.transform.position = pos;
                    timeToSpawn += 4f + Random.Range(0f, 4f);
                }
            }
        }
    }

}
