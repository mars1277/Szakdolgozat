using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayMenu : MonoBehaviour {

    public Text goldText;
    public Text silverText;
    public Text bronzeText;
    public Text fairyShardText;

    public Text playerNameFront;
    public Text playerNameBack;
    public Image characterImage;
    public Text levelText;
    public Text dayPassedText;

    public Button itemsButton;
    public Button skillsButton;
    public Button specializationButton;

    public GameObject UnlockGO;

    bool skillsButtonBlink;
    bool specializationButtonBlink;

    float timer;
    float blinkerSpeed;

    int slotNumber;
    int level;

    void Start()
    {
        slotNumber = PlayerPrefs.GetInt("GameSlot");
        playerNameFront.text = PlayerPrefs.GetString("Slot" + slotNumber + "_Name");
        playerNameBack.text = playerNameFront.text;
        characterImage.sprite = gameObject.GetComponent<NewCharacterMenuImages>().GetCharacterSprite(true, PlayerPrefs.GetString("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Character"));
        level = PlayerPrefs.GetInt("Slot" + slotNumber + "_Level");
        levelText.text = "Level " + level;
        int nextDayNumber = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_DayPassed") + 1;
        dayPassedText.text = "Next day: " + nextDayNumber;
        CheckRequiments();
        timer = 0f;
        blinkerSpeed = 0.8f;

        SetConsumablesValues();

        int availableSkillsNumber;
        if (level >= 10)
            availableSkillsNumber = 4;
        else if (level >= 7)
            availableSkillsNumber = 3;
        else if (level >= 4)
            availableSkillsNumber = 2;
        else
            availableSkillsNumber = 1;
        if (PlayerPrefs.GetInt("Slot" + slotNumber + "_SkillsNumber") < availableSkillsNumber)
            skillsButtonBlink = true;
        else
            skillsButtonBlink = false;

        if (PlayerPrefs.GetInt("Slot" + slotNumber + "_Level") > PlayerPrefs.GetInt("Slot" + slotNumber + "_Specialization_ActivatedNumber"))
            specializationButtonBlink = true;
        else
            specializationButtonBlink = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (skillsButtonBlink)
            if (timer < blinkerSpeed)
                skillsButton.GetComponent<Image>().color = new Color((blinkerSpeed - timer) / blinkerSpeed * 0.8f + 0.2f, 1f, (blinkerSpeed - timer) / blinkerSpeed * 0.8f + 0.2f);
            else if (timer < blinkerSpeed * 2)
                skillsButton.GetComponent<Image>().color = new Color((timer - blinkerSpeed) / blinkerSpeed * 0.8f + 0.2f, 1f, (timer - blinkerSpeed) / blinkerSpeed * 0.8f + 0.2f);
            else
                timer = 0f;

        if (specializationButtonBlink)
            if (timer < blinkerSpeed)
                specializationButton.GetComponent<Image>().color = new Color((blinkerSpeed - timer) / blinkerSpeed * 0.8f + 0.2f, 1f, (blinkerSpeed - timer) / blinkerSpeed * 0.8f + 0.2f);
            else if (timer < blinkerSpeed * 2)
                specializationButton.GetComponent<Image>().color = new Color((timer - blinkerSpeed) / blinkerSpeed * 0.8f + 0.2f, 1f, (timer - blinkerSpeed) / blinkerSpeed * 0.8f + 0.2f);
            else
                timer = 0f;
    }

    public void SetConsumablesValues()
    {
        int gold = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold");
        if (gold >= 10000)
        {
            goldText.text = (gold / 10000).ToString();
            silverText.text = (gold % 10000 / 100).ToString();
            bronzeText.text = (gold % 100).ToString();
        }
        else if (gold >= 100)
        {
            goldText.text = "0";
            silverText.text = (gold / 100).ToString();
            bronzeText.text = (gold % 100).ToString();
        }
        else
        {
            goldText.text = "0";
            silverText.text = "0";
            bronzeText.text = gold.ToString();
        }

        fairyShardText.text = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard").ToString();
    }

    public void CheckRequiments()
    {

    }

    public void StartNextDay()
    {
        SceneManager.LoadScene("Game");
    }

    public void Items()
    {
        SceneManager.LoadScene("ItemsMenu");
    }

    public void Characters()
    {
        SceneManager.LoadScene("Characters");
    }

    public void Skills()
    {
        SceneManager.LoadScene("SkillMenu");
    }

    public void Specialization()
    {
        SceneManager.LoadScene("SpecializationMenuTest");
    }

    public void Attributes()
    {
        SceneManager.LoadScene("AttributesMenu");
    }

    public void FairyShop()
    {
        SceneManager.LoadScene("FairyShop");
    }

    public void Achievements()
    {
        SceneManager.LoadScene("Achievements");
    }

    public void Encyclopedia()
    {
        SceneManager.LoadScene("Encyclopedia");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
