using UnityEngine;
using System.Collections;

public class Player_Level : MonoBehaviour {

    int slotNumber;
    int currentLevel;
    int currentXP;
    int nextLvlXP;

    public GameObject tenPercent;
    public GameObject onePercent;

    public GameObject XPBar;
    GameObject player;

    public GameObject levelUpCanvas;

    void Start()
    {
        player = GameObject.Find("Player");
        slotNumber = PlayerPrefs.GetInt("GameSlot");
        currentLevel = PlayerPrefs.GetInt("Slot" + slotNumber + "_Level");
        currentXP = PlayerPrefs.GetInt("Slot" + slotNumber + "_XP");
        nextLvlXP = GameObject.Find("Datas").GetComponent<Datas>().Get_ExperiencePoints_Player(currentLevel);
        float XPPercentage = currentXP / (float)nextLvlXP;
        XPBar.transform.localScale = new Vector3(XPPercentage, XPBar.transform.localScale.y, XPBar.transform.localScale.z);
        AddXP(0);
    }

    public int GetLevel()
    {
        return currentLevel;
    }


    public void AddXP(int xp)
    {
        currentXP += xp;

        if (currentLevel == 50)
            currentXP = nextLvlXP - 1;

        if (currentXP >= nextLvlXP)
        {
            LevelUp();
        }
        float XPPercentage = currentXP / (float)nextLvlXP;
        XPBar.transform.localScale = new Vector3(XPPercentage, XPBar.transform.localScale.y, XPBar.transform.localScale.z);

        int XPPercent = Mathf.FloorToInt(XPPercentage * 100);
        if (XPPercent / 10 == 0)
            tenPercent.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
        else
            tenPercent.GetComponent<ChangeDigitSprite>().ChangeSprite(XPPercent / 10);
        onePercent.GetComponent<ChangeDigitSprite>().ChangeSprite(XPPercent % 10);
    }

    public void LevelUp()
    {
        currentXP -= nextLvlXP;
        currentLevel++;
        nextLvlXP = GameObject.Find("Datas").GetComponent<Datas>().Get_ExperiencePoints_Player(currentLevel);
        GameObject.Find("Datas").GetComponent<AttributeCalculator>().Reinitialize(currentLevel);
        float newHealth = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_Health();
        player.GetComponent<Player_Health>().SetLevelUpHealth(newHealth);
        GameObject lvlUpCanvas = Instantiate(levelUpCanvas);
    }

    public void SaveChanges()
    {
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Level", currentLevel);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_XP", currentXP);
        int extraGold = Mathf.RoundToInt(GameObject.Find("Player").GetComponent<Player_Health>().GetGold() * (0.5f + GameObject.Find("Wall").GetComponent<Wall_Health>().GetHealhtPercentage() / 2f));
        int gold = PlayerPrefs.GetInt("Slot" + slotNumber + "_Gold");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Gold", gold + extraGold);
    }

    public void SaveChanges_PlayerLost()
    {
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Level", currentLevel);
        PlayerPrefs.SetInt("Slot" + slotNumber + "_XP", currentXP);
        int extraGold = Mathf.RoundToInt(GameObject.Find("Player").GetComponent<Player_Health>().GetGold() * 0.5f);
        int gold = PlayerPrefs.GetInt("Slot" + slotNumber + "_Gold");
        PlayerPrefs.SetInt("Slot" + slotNumber + "_Gold", gold + extraGold);
    }
}
