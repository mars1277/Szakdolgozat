using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Encyclopedia : MonoBehaviour {

    public Text goblinName;
    public Text goblinLevelAndStar;
    public Image goblinImage;
    public Text goblinHealth;
    public Text goblinSpeed;
    public Text goblinSpecial;
    public Text goblinOptionalTitle;
    public Text goblinOptional;

    public Text archerName;
    public Text archerLevelAndStar;
    public Image archerImage;
    public Text archerHealth;
    public Text archerSpeed;
    public Text archerSpecial;
    public Text archerOptionalTitle;
    public Text archerOptional;

    public Text zombieName;
    public Text zombieLevelAndStar;
    public Image zombieImage;
    public Text zombieHealth;
    public Text zombieSpeed;
    public Text zombieSpecial;
    public Text zombieOptionalTitle;
    public Text zombieOptional;

    public Text batName;
    public Text batLevelAndStar;
    public Image batImage;
    public Text batHealth;
    public Text batSpeed;
    public Text batSpecial;
    public Text batOptionalTitle;
    public Text batOptional;

    public Text skeletonMageName;
    public Text skeletonMageLevelAndStar;
    public Image skeletonMageImage;
    public Text skeletonMageHealth;
    public Text skeletonMageSpeed;
    public Text skeletonMageSpecial;
    public Text skeletonMageOptionalTitle;
    public Text skeletonMageOptional;

    public Text skeletonMageBossName;
    public Text skeletonMageBossLevel;
    public Image skeletonMageBossImage;
    public GameObject skeletonMageBossDetailsGO;
    public Text skeletonMageBossHealth;
    public Text skeletonMageBossDamageMissile;
    public Text skeletonMageBossDamageBomb;

    int dayNumber;
    float dayPoint;
    int numberOfEnemyTypes;
    float[,] enemiesLevelsAndStars;

    void Start () {
        dayNumber = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_DayPassed") + 1;
        dayPoint = gameObject.GetComponent<Days>().GetDayPoint(dayNumber);
        numberOfEnemyTypes = gameObject.GetComponent<Days>().GetNumberOFEnemyTypes(dayPoint);
        enemiesLevelsAndStars = new float[numberOfEnemyTypes, 3];
        for (int i = 0; i < numberOfEnemyTypes; i++)
            for (int j = 0; j < 2; j++)
                enemiesLevelsAndStars[i, j] = 1;
        enemiesLevelsAndStars = gameObject.GetComponent<Days>().CalculateEnemiesLevelsAndStars(enemiesLevelsAndStars, numberOfEnemyTypes, dayPoint);

        SetEnemiesValues();
        SetBossesValues();
    }

    public void SetEnemiesValues()
    {
        switch (numberOfEnemyTypes)
        {
            case 1:
                goblinName.text = "Goblin";
                goblinLevelAndStar.text = "Level: " + enemiesLevelsAndStars[0, 0] + "\nStar: " + enemiesLevelsAndStars[0, 1];
                goblinImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Goblin", (int)enemiesLevelsAndStars[0, 0]);
                goblinHealth.text = (30 * (int)Mathf.Pow(6, enemiesLevelsAndStars[0, 0] - 1) * enemiesLevelsAndStars[0, 1]).ToString();
                goblinSpeed.text = "2";
                goblinSpecial.text = "none";
                goblinOptionalTitle.text = "";
                goblinOptional.text = "";

                archerName.text = "???";
                archerLevelAndStar.text = "Level: ?\nStar: ?";
                archerImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Unknown", 0);
                archerHealth.text = "???";
                archerSpeed.text = "???";
                archerSpecial.text = "???";
                archerOptionalTitle.text = "";
                archerOptional.text = "";

                zombieName.text = "???";
                zombieLevelAndStar.text = "Level: ?\nStar: ?";
                zombieImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Unknown", 0);
                zombieHealth.text = "???";
                zombieSpeed.text = "???";
                zombieSpecial.text = "???";
                zombieOptionalTitle.text = "";
                zombieOptional.text = "";

                batName.text = "???";
                batLevelAndStar.text = "Level: ?\nStar: ?";
                batImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Unknown", 0);
                batHealth.text = "???";
                batSpeed.text = "???";
                batSpecial.text = "???";
                batOptionalTitle.text = "";
                batOptional.text = "";

                skeletonMageName.text = "???";
                skeletonMageLevelAndStar.text = "Level: ?\nStar: ?";
                skeletonMageImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Unknown", 0);
                skeletonMageHealth.text = "???";
                skeletonMageSpeed.text = "???";
                skeletonMageSpecial.text = "???";
                skeletonMageOptionalTitle.text = "";
                skeletonMageOptional.text = "";
                break;
            case 2:
                goblinName.text = "Goblin";
                goblinLevelAndStar.text = "Level: " + enemiesLevelsAndStars[0, 0] + "\nStar: " + enemiesLevelsAndStars[0, 1];
                goblinImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Goblin", (int)enemiesLevelsAndStars[0, 0]);
                goblinHealth.text = (30 * (int)Mathf.Pow(6, enemiesLevelsAndStars[0, 0] - 1) * enemiesLevelsAndStars[0, 1]).ToString();
                goblinSpeed.text = "2";
                goblinSpecial.text = "none";
                goblinOptionalTitle.text = "";
                goblinOptional.text = "";

                archerName.text = "Archer";
                archerLevelAndStar.text = "Level: " + enemiesLevelsAndStars[1, 0] + "\nStar: " + enemiesLevelsAndStars[1, 1];
                archerImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Archer", (int)enemiesLevelsAndStars[1, 0]);
                archerHealth.text = (60 * (int)Mathf.Pow(6, enemiesLevelsAndStars[1, 0] - 1) * enemiesLevelsAndStars[1, 1]).ToString();
                archerSpeed.text = "1";
                archerSpecial.text = "shoots";
                archerOptionalTitle.text = "Damage: ";
                archerOptional.text = (40 * (int)Mathf.Pow(6, enemiesLevelsAndStars[1, 0] - 1) * enemiesLevelsAndStars[1, 1]).ToString();

                zombieName.text = "???";
                zombieLevelAndStar.text = "Level: ?\nStar: ?";
                zombieImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Unknown", 0);
                zombieHealth.text = "???";
                zombieSpeed.text = "???";
                zombieSpecial.text = "???";
                zombieOptionalTitle.text = "";
                zombieOptional.text = "";

                batName.text = "???";
                batLevelAndStar.text = "Level: ?\nStar: ?";
                batImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Unknown", 0);
                batHealth.text = "???";
                batSpeed.text = "???";
                batSpecial.text = "???";
                batOptionalTitle.text = "";
                batOptional.text = "";

                skeletonMageName.text = "???";
                skeletonMageLevelAndStar.text = "Level: ?\nStar: ?";
                skeletonMageImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Unknown", 0);
                skeletonMageHealth.text = "???";
                skeletonMageSpeed.text = "???";
                skeletonMageSpecial.text = "???";
                skeletonMageOptionalTitle.text = "";
                skeletonMageOptional.text = "";
                break;
            case 3:
                goblinName.text = "Goblin";
                goblinLevelAndStar.text = "Level: " + enemiesLevelsAndStars[0, 0] + "\nStar: " + enemiesLevelsAndStars[0, 1];
                goblinImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Goblin", (int)enemiesLevelsAndStars[0, 0]);
                goblinHealth.text = (30 * (int)Mathf.Pow(6, enemiesLevelsAndStars[0, 0] - 1) * enemiesLevelsAndStars[0, 1]).ToString();
                goblinSpeed.text = "2";
                goblinSpecial.text = "none";
                goblinOptionalTitle.text = "";
                goblinOptional.text = "";

                archerName.text = "Archer";
                archerLevelAndStar.text = "Level: " + enemiesLevelsAndStars[1, 0] + "\nStar: " + enemiesLevelsAndStars[1, 1];
                archerImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Archer", (int)enemiesLevelsAndStars[1, 0]);
                archerHealth.text = (60 * (int)Mathf.Pow(6, enemiesLevelsAndStars[1, 0] - 1) * enemiesLevelsAndStars[1, 1]).ToString();
                archerSpeed.text = "1";
                archerSpecial.text = "shoots";
                archerOptionalTitle.text = "Damage: ";
                archerOptional.text = (40 * (int)Mathf.Pow(6, enemiesLevelsAndStars[1, 0] - 1) * enemiesLevelsAndStars[1, 1]).ToString();

                zombieName.text = "Zombie";
                zombieLevelAndStar.text = "Level: " + enemiesLevelsAndStars[2, 0] + "\nStar: " + enemiesLevelsAndStars[2, 1];
                zombieImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Zombie", (int)enemiesLevelsAndStars[2, 0]);
                zombieHealth.text = (450 * (int)Mathf.Pow(6, enemiesLevelsAndStars[2, 0] - 1) * enemiesLevelsAndStars[2, 1]).ToString();
                zombieSpeed.text = "1";
                zombieSpecial.text = "none";
                zombieOptionalTitle.text = "";
                zombieOptional.text = "";

                batName.text = "???";
                batLevelAndStar.text = "Level: ?\nStar: ?";
                batImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Unknown", 0);
                batHealth.text = "???";
                batSpeed.text = "???";
                batSpecial.text = "???";
                batOptionalTitle.text = "";
                batOptional.text = "";

                skeletonMageName.text = "???";
                skeletonMageLevelAndStar.text = "Level: ?\nStar: ?";
                skeletonMageImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Unknown", 0);
                skeletonMageHealth.text = "???";
                skeletonMageSpeed.text = "???";
                skeletonMageSpecial.text = "???";
                skeletonMageOptionalTitle.text = "";
                skeletonMageOptional.text = "";
                break;
            case 4:
                goblinName.text = "Goblin";
                goblinLevelAndStar.text = "Level: " + enemiesLevelsAndStars[0, 0] + "\nStar: " + enemiesLevelsAndStars[0, 1];
                goblinImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Goblin", (int)enemiesLevelsAndStars[0, 0]);
                goblinHealth.text = (30 * (int)Mathf.Pow(6, enemiesLevelsAndStars[0, 0] - 1) * enemiesLevelsAndStars[0, 1]).ToString();
                goblinSpeed.text = "2";
                goblinSpecial.text = "none";
                goblinOptionalTitle.text = "";
                goblinOptional.text = "";

                archerName.text = "Archer";
                archerLevelAndStar.text = "Level: " + enemiesLevelsAndStars[1, 0] + "\nStar: " + enemiesLevelsAndStars[1, 1];
                archerImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Archer", (int)enemiesLevelsAndStars[1, 0]);
                archerHealth.text = (60 * (int)Mathf.Pow(6, enemiesLevelsAndStars[1, 0] - 1) * enemiesLevelsAndStars[1, 1]).ToString();
                archerSpeed.text = "1";
                archerSpecial.text = "shoots";
                archerOptionalTitle.text = "Damage: ";
                archerOptional.text = (40 * (int)Mathf.Pow(6, enemiesLevelsAndStars[1, 0] - 1) * enemiesLevelsAndStars[1, 1]).ToString();

                zombieName.text = "Zombie";
                zombieLevelAndStar.text = "Level: " + enemiesLevelsAndStars[2, 0] + "\nStar: " + enemiesLevelsAndStars[2, 1];
                zombieImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Zombie", (int)enemiesLevelsAndStars[2, 0]);
                zombieHealth.text = (450 * (int)Mathf.Pow(6, enemiesLevelsAndStars[2, 0] - 1) * enemiesLevelsAndStars[2, 1]).ToString();
                zombieSpeed.text = "1";
                zombieSpecial.text = "none";
                zombieOptionalTitle.text = "";
                zombieOptional.text = "";

                batName.text = "Bat";
                batLevelAndStar.text = "Level: " + enemiesLevelsAndStars[3, 0] + "\nStar: " + enemiesLevelsAndStars[3, 1];
                batImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Bat", (int)enemiesLevelsAndStars[3, 0]);
                batHealth.text = (250 * (int)Mathf.Pow(6, enemiesLevelsAndStars[3, 0] - 1) * enemiesLevelsAndStars[3, 1]).ToString();
                batSpeed.text = "4";
                batSpecial.text = "none";
                batOptionalTitle.text = "";
                batOptional.text = "";

                skeletonMageName.text = "???";
                skeletonMageLevelAndStar.text = "Level: ?\nStar: ?";
                skeletonMageImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Unknown", 0);
                skeletonMageHealth.text = "???";
                skeletonMageSpeed.text = "???";
                skeletonMageSpecial.text = "???";
                skeletonMageOptionalTitle.text = "";
                skeletonMageOptional.text = "";
                break;
            case 5:
                goblinName.text = "Goblin";
                goblinLevelAndStar.text = "Level: " + enemiesLevelsAndStars[0, 0] + "\nStar: " + enemiesLevelsAndStars[0, 1];
                goblinImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Goblin", (int)enemiesLevelsAndStars[0, 0]);
                goblinHealth.text = (30 * (int)Mathf.Pow(6, enemiesLevelsAndStars[0, 0] - 1) * enemiesLevelsAndStars[0, 1]).ToString();
                goblinSpeed.text = "2";
                goblinSpecial.text = "none";
                goblinOptionalTitle.text = "";
                goblinOptional.text = "";

                archerName.text = "Archer";
                archerLevelAndStar.text = "Level: " + enemiesLevelsAndStars[1, 0] + "\nStar: " + enemiesLevelsAndStars[1, 1];
                archerImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Archer", (int)enemiesLevelsAndStars[1, 0]);
                archerHealth.text = (60 * (int)Mathf.Pow(6, enemiesLevelsAndStars[1, 0] - 1) * enemiesLevelsAndStars[1, 1]).ToString();
                archerSpeed.text = "1";
                archerSpecial.text = "shoots";
                archerOptionalTitle.text = "Damage: ";
                archerOptional.text = (40 * (int)Mathf.Pow(6, enemiesLevelsAndStars[1, 0] - 1) * enemiesLevelsAndStars[1, 1]).ToString();

                zombieName.text = "Zombie";
                zombieLevelAndStar.text = "Level: " + enemiesLevelsAndStars[2, 0] + "\nStar: " + enemiesLevelsAndStars[2, 1];
                zombieImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Zombie", (int)enemiesLevelsAndStars[2, 0]);
                zombieHealth.text = (450 * (int)Mathf.Pow(6, enemiesLevelsAndStars[2, 0] - 1) * enemiesLevelsAndStars[2, 1]).ToString();
                zombieSpeed.text = "1";
                zombieSpecial.text = "none";
                zombieOptionalTitle.text = "";
                zombieOptional.text = "";

                batName.text = "Bat";
                batLevelAndStar.text = "Level: " + enemiesLevelsAndStars[3, 0] + "\nStar: " + enemiesLevelsAndStars[3, 1];
                batImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Bat", (int)enemiesLevelsAndStars[3, 0]);
                batHealth.text = (250 * (int)Mathf.Pow(6, enemiesLevelsAndStars[3, 0] - 1) * enemiesLevelsAndStars[3, 1]).ToString();
                batSpeed.text = "4";
                batSpecial.text = "none";
                batOptionalTitle.text = "";
                batOptional.text = "";

                skeletonMageName.text = "Skeleton Mage";
                skeletonMageLevelAndStar.text = "Level: " + enemiesLevelsAndStars[4, 0] + "\nStar: " + enemiesLevelsAndStars[4, 1];
                skeletonMageImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("SkeletonMage", (int)enemiesLevelsAndStars[4, 0]);
                skeletonMageHealth.text = (1200 * (int)Mathf.Pow(6, enemiesLevelsAndStars[4, 0] - 1) * enemiesLevelsAndStars[4, 1]).ToString();
                skeletonMageSpeed.text = "1";
                skeletonMageSpecial.text = "shoots";
                skeletonMageOptionalTitle.text = "Damage: ";
                skeletonMageOptional.text = (800 * (int)Mathf.Pow(6, enemiesLevelsAndStars[4, 0] - 1) * enemiesLevelsAndStars[4, 1]).ToString();
                break;
            default:
                break;
        }
        SetImageSize(goblinImage, 1.3f);
        SetImageSize(archerImage, 1.3f);
        SetImageSize(zombieImage, 1.3f);
        SetImageSize(batImage, 1.3f);
        SetImageSize(skeletonMageImage, 1.3f);
    }

    public void SetBossesValues()
    {
        if (dayNumber < 100)
        {
            skeletonMageBossName.text = "???";
            skeletonMageBossLevel.text = "Level: ?";
            skeletonMageBossImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetEnemySprite("Unknown", 0);
            Destroy(skeletonMageBossDetailsGO);
        }
        else
        if (dayNumber < 200)
        {
            skeletonMageBossName.text = "Skeleton Mage Boss";
            skeletonMageBossLevel.text = "Level: 1";
            skeletonMageBossImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetBossSprite("SkeletonMageBoss");
            skeletonMageBossHealth.text = "100000";
            skeletonMageBossDamageMissile.text = "2000";
            skeletonMageBossDamageBomb.text = "5000";
        }
        else
        if (dayNumber < 300)
        {
            skeletonMageBossName.text = "Skeleton Mage Boss";
            skeletonMageBossLevel.text = "Level: 2";
            skeletonMageBossImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetBossSprite("SkeletonMageBoss");
            skeletonMageBossHealth.text = "700000";
            skeletonMageBossDamageMissile.text = "14000";
            skeletonMageBossDamageBomb.text = "35000";
        }
        else
        if (dayNumber < 400)
        {
            skeletonMageBossName.text = "Skeleton Mage Boss";
            skeletonMageBossLevel.text = "Level: 3";
            skeletonMageBossImage.sprite = gameObject.GetComponent<EncyclopediaImages>().GetBossSprite("SkeletonMageBoss");
            skeletonMageBossHealth.text = "3500000";
            skeletonMageBossDamageMissile.text = "70000";
            skeletonMageBossDamageBomb.text = "175000";
        }
        SetImageSize(skeletonMageBossImage, 2f);
    }

    public void SetImageSize(Image image, float optimalSize)
    {
        float spriteX;
        float spriteY;
        float sizeMultiplier;
        spriteX = image.sprite.bounds.size.x;
        spriteY = image.sprite.bounds.size.y;
        if(spriteX > spriteY)
        {
            sizeMultiplier = optimalSize / spriteX;
            image.transform.localScale = new Vector3(optimalSize, spriteY * sizeMultiplier);
        }
        else
        {
            sizeMultiplier = optimalSize / spriteY;
            image.transform.localScale = new Vector3(spriteX * sizeMultiplier, optimalSize);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("GameplayMenu");
    }
}
