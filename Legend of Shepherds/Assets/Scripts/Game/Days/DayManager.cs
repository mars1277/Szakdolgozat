using UnityEngine;
using System.Collections;


public class DayManager : MonoBehaviour {

    public GameObject gameTimeBar;

    int dayPassedNumber;
    bool dayStarted;

    bool dayEnded = false;
    bool dayPassed = false;

    public int enemyNumber;
    public int enemyKilled = 0;

    Days.Enemy[] enemies;
    public int enemyCounter = 0;

    float counter = 0f;
    float nextEnemyTime;

    float countdownCounter;
    bool dayReadyToEnd;

    void Start()
    {
        Time.timeScale = 1f;
        dayStarted = false;
        GameObject.Find("Datas").GetComponent<SpecializationDatas>().Initialize();
        GameObject.Find("Datas").GetComponent<AttributeCalculator>().Initialize();

        GameObject.Find("Player").GetComponent<Player_Attack>().Initialize();
        GameObject.Find("Player").GetComponent<Player_Health>().Initialize();
        GameObject.Find("Player").GetComponent<Player_Movement>().Initialize();

        GameObject.Find("WeaponSkill").GetComponent<WeaponSkills>().Initialize();

        dayPassedNumber = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_DayPassed");
        if (dayPassedNumber > 99)
            dayPassedNumber = 99;
        enemies = GameObject.Find("Datas").GetComponent<Days>().GetDay(dayPassedNumber + 1);

        enemyNumber = enemies.Length;
        counter = 0f;
        nextEnemyTime = enemies[0].time;

        enemyCounter = 0;

        countdownCounter = 0f;
        dayReadyToEnd = false;

        dayStarted = true;

    }



    void Update()
    {
        float gameTime = counter / 125f;
        gameTimeBar.transform.localScale = new Vector3(gameTime, gameTimeBar.transform.localScale.y, gameTimeBar.transform.localScale.z);
        if (dayStarted)
        {
            counter += Time.deltaTime;

            if ((enemyCounter >= enemyNumber))
            {
                dayEnded = true;
            }

            if (enemyKilled >= enemyNumber)
            {
                dayPassed = true;
                enemyKilled = 0;
            }

            if (dayPassed)
            {
                if (!dayReadyToEnd)
                {
                    countdownCounter += Time.deltaTime;
                    if(countdownCounter > 1f)
                    {
                        if (GameObject.FindGameObjectsWithTag("Assumable").Length > 0)
                            countdownCounter = 0f;
                        else
                            dayReadyToEnd = true;
                    }
                }
                else
                {
                    GameObject.Find("VirtualJoystickContainer").GetComponent<VirtualJoystick>().Stop();
                    if (dayPassedNumber >= 99)
                        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_DayPassed", dayPassedNumber);
                    else
                        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_DayPassed", dayPassedNumber + 1);

                    GameObject enemy = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().dayPassedGO);
                    dayPassed = false;
                    GameObject.Find("Player").GetComponent<Player_Level>().SaveChanges();
                }
            }

            if (!dayEnded)
            {

                nextEnemyTime = enemies[enemyCounter].time;

                if (counter >= nextEnemyTime)
                {
                    SpawnEnemy();
                    enemyCounter++;
                }
            }
        }
    }

    public void SpawnEnemy()
    {
        string enemyName = enemies[enemyCounter].name.Substring(0, enemies[enemyCounter].name.Length - 3);
        if (enemyName.Equals("Goblin_"))
        {
            GameObject goblin;
            switch (enemies[enemyCounter].name.Substring(0, enemies[enemyCounter].name.Length - 2))
            {
                case "Goblin_1":
                    goblin = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Goblin_Level1_GO);
                    break;
                case "Goblin_2":
                    goblin = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Goblin_Level2_GO);
                    break;
                case "Goblin_3":
                    goblin = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Goblin_Level3_GO);
                    break;
                case "Goblin_4":
                    goblin = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Goblin_Level4_GO);
                    break;
                case "Goblin_5":
                    goblin = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Goblin_Level5_GO);
                    break;
                default:
                    goblin = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Goblin_Level5_GO);
                    break;
            }
            goblin.GetComponent<Goblin_Health>().SetStar(int.Parse(enemies[enemyCounter].name.Substring(enemies[enemyCounter].name.Length - 1, 1)));
            Vector3 pos = gameObject.GetComponent<EnemyGOs>().spawnPointGO.transform.position;
            pos.x = enemies[enemyCounter].coordinate;
            goblin.transform.position = pos;
        }
        else

        if (enemyName.Equals("Archer_"))
        {
            GameObject archer;
            switch (enemies[enemyCounter].name.Substring(0, enemies[enemyCounter].name.Length - 2))
            {
                case "Archer_1":
                    archer = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Archer_Level1_GO);
                    break;
                case "Archer_2":
                    archer = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Archer_Level2_GO);
                    break;
                case "Archer_3":
                    archer = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Archer_Level3_GO);
                    break;
                case "Archer_4":
                    archer = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Archer_Level4_GO);
                    break;
                case "Archer_5":
                    archer = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Archer_Level5_GO);
                    break;
                default:
                    archer = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Archer_Level4_GO);
                    break;
            }
            archer.GetComponent<Archer_Health>().SetStar(int.Parse(enemies[enemyCounter].name.Substring(enemies[enemyCounter].name.Length - 1, 1)));
            Vector3 pos = gameObject.GetComponent<EnemyGOs>().spawnPointGO.transform.position;
            pos.x = enemies[enemyCounter].coordinate;
            archer.transform.position = pos;
        }
        else

        if (enemyName.Equals("Zombie_"))
        {
            GameObject zombie;
            switch (enemies[enemyCounter].name.Substring(0, enemies[enemyCounter].name.Length - 2))
            {
                case "Zombie_1":
                    zombie = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Zombie_Level1_GO);
                    break;
                case "Zombie_2":
                    zombie = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Zombie_Level2_GO);
                    break;
                case "Zombie_3":
                    zombie = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Zombie_Level3_GO);
                    break;
                case "Zombie_4":
                    zombie = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Zombie_Level4_GO);
                    break;
                default:
                    zombie = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Zombie_Level3_GO);
                    break;
            }
            zombie.GetComponent<Zombie_Health>().SetStar(int.Parse(enemies[enemyCounter].name.Substring(enemies[enemyCounter].name.Length - 1, 1)));
            Vector3 pos = gameObject.GetComponent<EnemyGOs>().spawnPointGO.transform.position;
            pos.x = enemies[enemyCounter].coordinate;
            zombie.transform.position = pos;
        }
        else

        if (enemyName.Equals("Bat_"))
        {
            GameObject bat;
            switch (enemies[enemyCounter].name.Substring(0, enemies[enemyCounter].name.Length - 2))
            {
                case "Bat_1":
                    bat = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Bat_Level1_GO);
                    break;
                case "Bat_2":
                    bat = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Bat_Level2_GO);
                    break;
                case "Bat_3":
                    bat = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Bat_Level3_GO);
                    break;
                case "Bat_4":
                    bat = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Bat_Level4_GO);
                    break;
                default:
                    bat = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().Bat_Level3_GO);
                    break;
            }
            bat.GetComponent<Bat_Health>().SetStar(int.Parse(enemies[enemyCounter].name.Substring(enemies[enemyCounter].name.Length - 1, 1)));
            Vector3 pos = gameObject.GetComponent<EnemyGOs>().spawnPointGO.transform.position;
            pos.x = enemies[enemyCounter].coordinate;
            bat.transform.position = pos;
        }
        else

        if (enemyName.Equals("SkeletonMage_"))
        {
            GameObject skeletonMage;
            switch (enemies[enemyCounter].name.Substring(0, enemies[enemyCounter].name.Length - 2))
            {
                case "SkeletonMage_1":
                    skeletonMage = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().SkeletonMage_Level1_GO);
                    break;
                case "SkeletonMage_2":
                    skeletonMage = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().SkeletonMage_Level2_GO);
                    break;
                case "SkeletonMage_3":
                    skeletonMage = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().SkeletonMage_Level3_GO);
                    break;
                default:
                    skeletonMage = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().SkeletonMage_Level3_GO);
                    break;
            }
            skeletonMage.GetComponent<SkeletonMage_Health>().SetStar(int.Parse(enemies[enemyCounter].name.Substring(enemies[enemyCounter].name.Length - 1, 1)));
            Vector3 pos = gameObject.GetComponent<EnemyGOs>().spawnPointGO.transform.position;
            pos.x = enemies[enemyCounter].coordinate;
            skeletonMage.transform.position = pos;
        }
        else

        if (enemies[enemyCounter].name.Equals("SkeletonMageBoss"))
        {
            GameObject skeletonMageBoss = (GameObject)Instantiate(gameObject.GetComponent<EnemyGOs>().SkeletonMageBoss_GO);
            switch(dayPassedNumber + 1)
            {
                case 100:
                    skeletonMageBoss.GetComponent<SkeletonMageBoss_Health>().SetLevel(1);
                    break;
                case 200:
                    skeletonMageBoss.GetComponent<SkeletonMageBoss_Health>().SetLevel(2);
                    break;
                case 300:
                    skeletonMageBoss.GetComponent<SkeletonMageBoss_Health>().SetLevel(3);
                    break;
            }
            Vector3 pos = gameObject.GetComponent<EnemyGOs>().spawnPointGO.transform.position;
            pos.x = enemies[enemyCounter].coordinate;
            pos.y = 4f;
            skeletonMageBoss.transform.position = pos;
        }
    }
}
