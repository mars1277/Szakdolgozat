using UnityEngine;
using System.Collections;

public class Days : MonoBehaviour {

    public struct Enemy
    {
        public string name;
        public float time;
        public float coordinate;
    }


    float min = -1f;
    float max = 1f;
    int enemyCounter = 0;
    Enemy[] enemies;

    public void SetEnemy(string n, float t, float c)
    {
        enemies[enemyCounter].name = n;
        enemies[enemyCounter].time = t;
        enemies[enemyCounter].coordinate = c;
        enemyCounter++;
    }

    public Enemy[] GetDay(int dayNumber)
    {
        float dayPoint = GetDayPoint(dayNumber);
        int numberOfEnemyTypes = GetNumberOFEnemyTypes(dayPoint);
        float[,] enemiesLevelsAndStars = new float[numberOfEnemyTypes, 3];
        for (int i = 0; i < numberOfEnemyTypes; i++)
            for (int j = 0; j < 2; j++)
                enemiesLevelsAndStars[i, j] = 1;
        enemiesLevelsAndStars = CalculateEnemiesLevelsAndStars(enemiesLevelsAndStars, numberOfEnemyTypes, dayPoint);
        return GetDayEnemies(enemiesLevelsAndStars, numberOfEnemyTypes, dayNumber);
    }

    public float GetDayPoint(int dayNumber)
    {
        bool foundDay = false;
        int dayIdx = -1;
        while (!foundDay)
        {
            dayIdx++;
            if (gameObject.GetComponent<Datas>().Get_DayValues(dayIdx, 0) > dayNumber)
                foundDay = true;
        }
        float num = Mathf.Pow(gameObject.GetComponent<Datas>().Get_DayValues(dayIdx, 2), dayNumber - gameObject.GetComponent<Datas>().Get_DayValues(dayIdx - 1, 0));
        num *= gameObject.GetComponent<Datas>().Get_DayValues(dayIdx - 1, 1) * 10;
        return num;
    }

    public int GetNumberOFEnemyTypes(float dayPoint)
    {
        bool numFound = false;
        int num = -1;
        while (!numFound)
        {
            num++;
            if (gameObject.GetComponent<Datas>().Get_EnemyPoints(num) > dayPoint)
                numFound = true;
        }
        return num;
    }

    public float[,] CalculateEnemiesLevelsAndStars(float[,] enemiesLevelsAndStars, int numberOfEnemyTypes, float dayPoint)
    {
        for(int i = 0; i < numberOfEnemyTypes; i++)
        {
            int level = 1;
            int star = 1;
            int enemyPoint = gameObject.GetComponent<Datas>().Get_EnemyPoints(i);
            bool foundEnemyPoint = false;
            int nextEnemyPoint = enemyPoint;
            while (!foundEnemyPoint)
            {
                switch (star)
                {
                    case 1:
                        nextEnemyPoint *= 2;
                        break;
                    case 2:
                        nextEnemyPoint = nextEnemyPoint / 2 * 3;
                        break;
                    case 3:
                        nextEnemyPoint *= 2;
                        break;
                    default:
                        break;
                }
                if (nextEnemyPoint > dayPoint)
                {
                    foundEnemyPoint = true;
                    enemiesLevelsAndStars[i, 0] = level;
                    enemiesLevelsAndStars[i, 1] = star;
                    enemiesLevelsAndStars[i, 2] = (dayPoint - (float)enemyPoint) / ((float)nextEnemyPoint - (float)enemyPoint);
                }
                else
                {
                    star++;
                    if (star == 4)
                    {
                        star = 1;
                        level++;
                    }
                    enemyPoint = nextEnemyPoint;
                }
            }
        }
        return enemiesLevelsAndStars;
    }

    public Enemy[] GetDayEnemies(float[,] enemiesLevelsAndStars, int numberOfEnemyTypes, int dayNumber)
    {
        if(dayNumber == 100 || dayNumber == 200 || dayNumber == 300)
            enemies = new Enemy[86];
        else
            enemies = new Enemy[85];
        for(int i = 0; i < 31; i++)
        {
            int random = Random.Range(0, numberOfEnemyTypes);
            string tmpName = "";
            switch (random)
            {
                case 0:
                    tmpName = "Goblin_";
                    break;
                case 1:
                    tmpName = "Archer_";
                    break;
                case 2:
                    tmpName = "Zombie_";
                    break;
                case 3:
                    tmpName = "Bat_";
                    break;
                case 4:
                    tmpName = "SkeletonMage_";
                    break;
                default:
                    break;
            }
            float r = Random.value;
            int level =  Mathf.RoundToInt(enemiesLevelsAndStars[random, 0]);
            int star = Mathf.RoundToInt(enemiesLevelsAndStars[random, 1]);
            if (r <= enemiesLevelsAndStars[random, 2])
            {
                star++;
                if (star == 4)
                {
                    star = 1;
                    level++;
                }
            }
            tmpName += level + "_" + star;
            SetEnemy(tmpName, (i * 4f), Random.Range(min, max));
        }
        SetEnemyFormation(13f, enemiesLevelsAndStars, numberOfEnemyTypes);
        SetEnemyFormation(27f, enemiesLevelsAndStars, numberOfEnemyTypes);
        SetEnemyFormation(43f, enemiesLevelsAndStars, numberOfEnemyTypes);
        SetEnemyFormation(59f, enemiesLevelsAndStars, numberOfEnemyTypes);
        SetEnemyFormation(67f, enemiesLevelsAndStars, numberOfEnemyTypes);
        SetEnemyFormation(83f, enemiesLevelsAndStars, numberOfEnemyTypes);
        SetEnemyFormation(99f, enemiesLevelsAndStars, numberOfEnemyTypes);
        SetEnemyFormation(115f , enemiesLevelsAndStars, numberOfEnemyTypes);
        SetEnemyFormation(123f, enemiesLevelsAndStars, numberOfEnemyTypes);
        if (dayNumber == 100 || dayNumber == 200 || dayNumber == 300)
            SetEnemy("SkeletonMageBoss", 125f, 0f);
        SortEnemies();
        return enemies;
    }

    public void SetEnemyFormation(float time, float[,] enemiesLevelsAndStars, int numberOfEnemyTypes)
    {
        int random = Random.Range(0, numberOfEnemyTypes);
        int timeBetweenLanes = 1;
        string tmpName = "";
        switch (random)
        {
            case 0:
                tmpName = "Goblin_";
                timeBetweenLanes = 2;
                break;
            case 1:
                tmpName = "Archer_";
                timeBetweenLanes = 4;
                break;
            case 2:
                tmpName = "Zombie_";
                timeBetweenLanes = 1;
                break;
            case 3:
                tmpName = "Bat_";
                timeBetweenLanes = 1;
                break;
            case 4:
                tmpName = "SkeletonMage_";
                timeBetweenLanes = 1;
                break;
            default:
                break;
        }
        for(int i = 0; i < 3; i++)
        {
            float r = Random.value;
            int level = Mathf.RoundToInt(enemiesLevelsAndStars[random, 0]);
            int star = Mathf.RoundToInt(enemiesLevelsAndStars[random, 1]);
            if (r <= enemiesLevelsAndStars[random, 2])
            {
                star++;
                if (star == 4)
                {
                    star = 1;
                    level++;
                }
            }
            string newTmpName = tmpName + level + "_" + star;
            SetEnemy(newTmpName, time, -0.7f + i * 0.7f);
        }
        for (int i = 0; i < 3; i++)
        {
            float r = Random.value;
            int level = Mathf.RoundToInt(enemiesLevelsAndStars[random, 0]);
            int star = Mathf.RoundToInt(enemiesLevelsAndStars[random, 1]);
            if (r <= enemiesLevelsAndStars[random, 2])
            {
                star++;
                if (star == 4)
                {
                    star = 1;
                    level++;
                }
            }
            string newTmpName = tmpName + level + "_" + star;
            SetEnemy(newTmpName, time + timeBetweenLanes, -0.7f + i * 0.7f);
        }
    }

    public void SortEnemies()
    {
        for(int i = 0; i < 84; i++)
            for(int j = i + 1; j < 85; j++)          
                if(enemies[j].time < enemies[i].time)
                {
                    Enemy tmp = enemies[i];
                    enemies[i] = enemies[j];
                    enemies[j] = tmp;
                }       
    }

     public Enemy[] GetDay1Enemies()
     {
         enemies = new Enemy[13];
         SetEnemy("Goblin_1_1", 5f, 0f);
         SetEnemy("Goblin_1_2", 11f, Random.Range(min, max));
         SetEnemy("Goblin_1_3", 19f, Random.Range(min, max));
         SetEnemy("Goblin_1_1", 22f, Random.Range(min, max));
         SetEnemy("Goblin_1_2", 28f, Random.Range(min, max));
         SetEnemy("Goblin_1_3", 36f, Random.Range(min, max));
         SetEnemy("Goblin_1_1", 39f, Random.Range(min, max));
         SetEnemy("Goblin_1_2", 44f, Random.Range(min, max));
         SetEnemy("Goblin_1_3", 47f, Random.Range(min, max));
         SetEnemy("Goblin_1_1", 55f, -0.4f);
         SetEnemy("Goblin_1_2", 55f, 0.4f);
         SetEnemy("Goblin_1_3", 58f, -1f);
         SetEnemy("Goblin_1_1", 58f, 1f);
         return enemies;
     }

    public Enemy[] GetDay2Enemies()
    {
        enemies = new Enemy[28];
        SetEnemy("Goblin_1_1", 5f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 10f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 14f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 18f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 22f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 25f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 28f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 36f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 42f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 50f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 54f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 60f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 64f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 70f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 74f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 78f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 82f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 90f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 96f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 100f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 104f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 108f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 112f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 118f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 124f, -0.4f);
        SetEnemy("Goblin_1_1", 124f, 0.4f);
        SetEnemy("Archer_1_1", 127f, -1f);
        SetEnemy("Archer_1_1", 127f, 1f);
        return enemies;
    }

    public Enemy[] GetDay3Enemies()
    {
        enemies = new Enemy[36];
        SetEnemy("Archer_1_1", 5f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 10f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 15f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 22f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 29f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 36f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 43f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 48f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 53f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 58f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 63f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 68f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 73f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 78f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 83f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 88f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 93f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 98f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 103f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 108f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 115f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 120f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 125f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 130f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 135f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 140f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 145f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 150f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 157f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 164f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 169f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 174f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 179f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 188f, 0f);
        SetEnemy("Zombie_1_1", 194f, -0.6f);
        SetEnemy("Zombie_1_1", 194f, 0.6f);
        return enemies;
    }

    public Enemy[] GetDay4Enemies()
    {
        enemies = new Enemy[34];
        SetEnemy("Bat_1_1", 5f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 8f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 15f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 18f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 23f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 26f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 31f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 38f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 43f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 48f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 53f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 60f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 65f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 70f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 73f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 76f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 81f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 84f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 89f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 94f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 97f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 104f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 109f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 114f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 121f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 124f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 129f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 132f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 137f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 144f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 149f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 158f, 0f);
        SetEnemy("Bat_1_1", 162f, -0.6f);
        SetEnemy("Bat_1_1", 162f, 0.6f);
        return enemies;
    }

    public Enemy[] GetDay5Enemies()
    {
        enemies = new Enemy[37];
        SetEnemy("Zombie_1_1", 5f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 12f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 17f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 24f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 31f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 38f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 41f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 46f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 51f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 56f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 59f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 64f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 71f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 76f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 83f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 90f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 93f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 98f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 105f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 110f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 115f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 120f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 125f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 128f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 131f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 136f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 141f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 146f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 151f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 156f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 159f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 164f, Random.Range(min, max));
        SetEnemy("Goblin_1_1", 170f, -0.6f);
        SetEnemy("Goblin_1_1", 170f, 0.6f);
        SetEnemy("SkeletonMage_1_1", 174f, 0);
        SetEnemy("Goblin_1_1", 180f, -0.6f);
        SetEnemy("Goblin_1_1", 180f, 0.6f);
        return enemies;
    }

    public Enemy[] GetDay6Enemies()
    {
        enemies = new Enemy[35];
        SetEnemy("Archer_1_1", 5f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 10f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 17f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 22f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 27f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 32f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 35f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 40f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 45f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 52f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 57f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 62f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 67f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 72f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 75f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 80f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 85f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 90f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 95f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 102f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 107f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 112f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 115f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 118f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 125f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 128f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 133f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 140f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 143f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 148f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 153f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 158f, Random.Range(min, max));
        SetEnemy("Archer_1_1", 163f, Random.Range(min, max));
        SetEnemy("Zombie_1_1", 168f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 175f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay7Enemies()
    {
        enemies = new Enemy[35];
        SetEnemy("Goblin_2_1", 5f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 10f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 15f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 18f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 23f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 28f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 33f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 36f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 39f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 44f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 49f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 52f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 57f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 62f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 67f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 72f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 77f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 82f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 87f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 92f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 95f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 100f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 103f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 106f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 111f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 116f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 121f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 126f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 131f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 136f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 139f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 144f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 149f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 154f, Random.Range(min, max));
        SetEnemy("Bat_1_1", 157f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay8Enemies()
    {
        enemies = new Enemy[36];
        SetEnemy("SkeletonMage_1_1", 5f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 10f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 17f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 22f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 29f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 34f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 39f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 44f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 51f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 58f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 63f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 68f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 73f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 78f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 83f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 88f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 93f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 98f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 103f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 108f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 113f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 120f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 127f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 132f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 139f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 144f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 149f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 154f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 161f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 166f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 171f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 176f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 181f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 188f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 193f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 200f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay9Enemies()
    {
        enemies = new Enemy[42];
        SetEnemy("Goblin_2_1", 5f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 10f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 17f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 20f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 25f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 30f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 35f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 40f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 45f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 50f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 53f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 58f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 63f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 68f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 75f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 80f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 85f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 88f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 93f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 98f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 103f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 110f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 113f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 118f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 123f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 126f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 131f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 134f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 137f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 142f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 147f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 152f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 155f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 162f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 169f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 176f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 179f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 184f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 187f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 194f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 197f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 204f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay10Enemies()
    {
        enemies = new Enemy[50];
        SetEnemy("Zombie_2_1", 5f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 12f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 17f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 22f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 25f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 30f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 33f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 36f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 41f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 46f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 51f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 54f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 59f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 64f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 69f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 72f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 75f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 82f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 87f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 90f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 95f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 100f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 105f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 108f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 113f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 116f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 121f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 128f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 133f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 138f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 143f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 148f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 153f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 160f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 165f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 172f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 179f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 186f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 193f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 196f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 201f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 208f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 215f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 222f, Random.Range(min, max));
        SetEnemy("SkeletonMage_1_1", 227f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 232f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 237f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 244f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 247f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 250f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay11Enemies()
    {
        enemies = new Enemy[48];
        SetEnemy("Zombie_2_1", 5f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 12f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 15f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 22f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 29f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 32f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 35f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 40f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 45f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 50f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 55f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 60f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 65f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 70f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 75f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 80f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 87f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 92f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 97f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 102f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 107f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 112f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 117f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 122f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 127f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 134f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 137f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 142f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 147f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 150f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 153f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 160f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 163f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 170f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 175f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 180f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 183f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 188f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 193f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 200f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 203f, Random.Range(min, max));
        SetEnemy("Goblin_2_1", 208f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 213f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 218f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 223f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 228f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 233f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 238f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay12Enemies()
    {
        enemies = new Enemy[46];
        SetEnemy("Bat_2_1", 5f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 8f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 13f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 18f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 21f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 26f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 31f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 36f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 39f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 44f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 49f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 56f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 61f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 66f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 71f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 76f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 81f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 84f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 89f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 94f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 101f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 106f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 111f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 114f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 117f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 122f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 127f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 134f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 137f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 142f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 147f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 152f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 157f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 160f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 165f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 170f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 175f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 180f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 185f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 192f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 199f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 206f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 213f, Random.Range(min, max));
        SetEnemy("Archer_2_1", 218f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 223f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 230f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay13Enemies()
    {
        enemies = new Enemy[52];
        SetEnemy("Archer_3_1", 5f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 10f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 15f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 18f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 23f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 28f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 35f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 40f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 45f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 50f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 57f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 62f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 69f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 74f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 77f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 82f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 87f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 90f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 95f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 100f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 103f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 108f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 113f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 116f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 121f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 126f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 131f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 136f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 143f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 148f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 153f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 158f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 161f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 166f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 171f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 176f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 181f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 184f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 189f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 194f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 199f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 204f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 209f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 214f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 217f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 222f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 225f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 230f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 235f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 240f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 245f, Random.Range(min, max));
        SetEnemy("Bat_2_1", 250f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay14Enemies()
    {
        enemies = new Enemy[48];
        SetEnemy("Bat_3_1", 5f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 8f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 13f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 18f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 21f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 26f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 33f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 38f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 43f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 46f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 51f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 56f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 63f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 70f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 77f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 82f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 87f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 92f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 97f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 102f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 109f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 114f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 119f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 124f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 129f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 132f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 139f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 144f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 149f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 154f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 159f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 164f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 171f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 176f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 179f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 184f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 187f, Random.Range(min, max));
        SetEnemy("Zombie_2_1", 192f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 199f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 202f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 207f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 210f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 213f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 218f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 223f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 228f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 231f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 236f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay15Enemies()
    {
        enemies = new Enemy[42];
        SetEnemy("Bat_3_1", 5f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 8f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 13f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 18f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 23f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 30f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 35f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 40f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 45f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 50f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 57f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 64f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 71f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 78f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 83f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 86f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 91f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 96f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 101f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 106f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 109f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 114f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 119f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 124f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 129f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 136f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 141f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 146f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 149f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 154f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 159f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 162f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 165f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 172f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 177f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 182f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 189f, Random.Range(min, max));
        SetEnemy("Goblin_3_1", 194f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 199f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 204f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 211f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 218f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay16Enemies()
    {
        enemies = new Enemy[52];
        SetEnemy("Bat_3_1", 5f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 8f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 13f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 18f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 23f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 28f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 33f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 38f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 45f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 50f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 55f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 58f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 63f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 68f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 75f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 80f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 83f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 88f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 91f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 96f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 101f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 108f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 113f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 118f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 123f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 128f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 133f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 138f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 145f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 150f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 157f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 160f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 165f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 168f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 175f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 178f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 183f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 188f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 193f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 198f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 203f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 208f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 213f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 218f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 221f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 228f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 233f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 238f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 245f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 250f, Random.Range(min, max));
        SetEnemy("SkeletonMage_2_1", 255f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 260f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay17Enemies()
    {
        enemies = new Enemy[46];
        SetEnemy("Goblin_4_1", 5f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 10f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 15f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 22f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 29f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 36f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 39f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 46f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 51f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 56f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 61f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 64f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 69f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 72f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 77f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 84f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 87f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 92f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 97f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 102f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 109f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 114f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 119f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 124f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 129f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 134f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 139f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 146f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 151f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 158f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 165f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 170f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 175f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 180f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 185f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 192f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 195f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 200f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 205f, Random.Range(min, max));
        SetEnemy("Goblin_4_1", 210f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 215f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 218f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 223f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 228f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 233f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 238f, Random.Range(min, max));

        return enemies;
    }

    public Enemy[] GetDay18Enemies()
    {
        enemies = new Enemy[54];
        SetEnemy("Archer_3_1", 5f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 10f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 13f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 16f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 21f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 28f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 35f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 40f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 45f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 50f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 55f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 60f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 67f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 72f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 75f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 80f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 87f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 92f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 99f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 102f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 107f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 112f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 117f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 122f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 127f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 134f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 139f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 142f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 145f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 150f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 153f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 158f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 163f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 168f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 171f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 176f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 179f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 182f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 185f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 190f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 195f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 198f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 203f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 206f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 211f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 216f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 221f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 226f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 231f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 236f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 241f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 246f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 249f, Random.Range(min, max));
        SetEnemy("Archer_3_1", 254f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay19Enemies()
    {
        enemies = new Enemy[56];
        SetEnemy("Goblin_5_1", 5f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 10f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 15f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 22f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 27f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 32f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 37f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 42f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 49f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 52f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 57f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 62f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 67f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 72f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 79f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 84f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 91f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 96f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 101f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 106f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 111f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 114f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 119f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 124f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 129f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 136f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 141f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 146f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 153f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 158f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 163f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 168f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 173f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 180f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 183f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 188f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 193f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 200f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 207f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 212f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 217f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 222f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 227f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 232f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 235f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 242f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 247f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 250f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 255f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 258f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 263f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 268f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 273f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 278f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 283f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 288f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay20Enemies()
    {
        enemies = new Enemy[49];
        SetEnemy("Archer_4_1", 5f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 10f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 15f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 20f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 23f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 30f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 35f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 42f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 47f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 54f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 59f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 66f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 69f, Random.Range(min, max));
        SetEnemy("Zombie_3_1", 76f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 83f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 88f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 93f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 98f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 103f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 108f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 113f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 116f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 121f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 126f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 131f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 136f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 141f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 146f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 151f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 156f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 159f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 162f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 165f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 168f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 173f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 178f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 183f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 186f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 191f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 196f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 201f, Random.Range(min, max));
        SetEnemy("SkeletonMage_3_1", 206f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 211f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 216f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 221f, Random.Range(min, max));
        SetEnemy("Archer_4_1", 224f, Random.Range(min, max));
        SetEnemy("Bat_3_1", 229f, Random.Range(min, max));
        SetEnemy("Goblin_5_1", 232f, Random.Range(min, max));
        SetEnemy("SkeletonMageBoss", 254f, 0f);
        return enemies;
    }

    public Enemy[] GetDay21Enemies()
    {
        enemies = new Enemy[1];
        SetEnemy("SkeletonMageBoss", 5f, 0f);
        return enemies;
    }

    public Enemy[] GetDay22Enemies()
    {
        enemies = new Enemy[1];
        SetEnemy("Goblin_1_1", 5f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay23Enemies()
    {
        enemies = new Enemy[1];
        SetEnemy("Goblin_1_1", 5f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay24Enemies()
    {
        enemies = new Enemy[1];
        SetEnemy("Goblin_1_1", 5f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay25Enemies()
    {
        enemies = new Enemy[1];
        SetEnemy("Goblin_1_1", 5f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay26Enemies()
    {
        enemies = new Enemy[1];
        SetEnemy("Goblin_1_1", 5f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay27Enemies()
    {
        enemies = new Enemy[1];
        SetEnemy("Goblin_1_1", 5f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay28Enemies()
    {
        enemies = new Enemy[1];
        SetEnemy("Goblin_1_1", 5f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay29Enemies()
    {
        enemies = new Enemy[1];
        SetEnemy("Goblin_1_1", 5f, Random.Range(min, max));
        return enemies;
    }

    public Enemy[] GetDay30Enemies()
    {
        enemies = new Enemy[1];
        SetEnemy("Goblin_1_1", 5f, Random.Range(min, max));
        return enemies;
    }


}
