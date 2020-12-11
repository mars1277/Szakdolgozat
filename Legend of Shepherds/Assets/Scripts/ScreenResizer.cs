using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenResizer : MonoBehaviour
{
    bool game_skillButtonsNeedsToBePositioned = false;

    public Sprite bigBackgroudWithHoleForSpecialization;
    public GameObject BackgroundWithHoleBig;

    void Start()
    {
        if (Screen.height / (float) Screen.width > 2)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "MainMenu":
                    GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 1800);
                    GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition + new Vector3(0, 100, 0);
                    GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition + new Vector3(0, 100, 0);
                    GameObject.Find("Button_Options").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_Options").GetComponent<RectTransform>().localPosition + new Vector3(0, -50, 0);
                    GameObject.Find("Coming_soon").GetComponent<RectTransform>().localPosition = GameObject.Find("Coming_soon").GetComponent<RectTransform>().localPosition + new Vector3(0, -50, 0);
                    GameObject.Find("Button_Quit").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_Quit").GetComponent<RectTransform>().localPosition + new Vector3(0, -100, 0);
                    break;
                case "StartGameMenu":
                    GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 1800);
                    GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition + new Vector3(0, -100, 0);
                    GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localScale = new Vector3(1.3f, 1.3f, 1f);
                    GameObject.Find("Slot1").GetComponent<RectTransform>().localPosition = GameObject.Find("Slot1").GetComponent<RectTransform>().localPosition + new Vector3(0, 80, 0);
                    GameObject.Find("Slot2").GetComponent<RectTransform>().localPosition = GameObject.Find("Slot2").GetComponent<RectTransform>().localPosition + new Vector3(0, 80, 0);
                    GameObject.Find("Slot3").GetComponent<RectTransform>().localPosition = GameObject.Find("Slot3").GetComponent<RectTransform>().localPosition + new Vector3(0, 40, 0);
                    GameObject.Find("Slot4").GetComponent<RectTransform>().localPosition = GameObject.Find("Slot4").GetComponent<RectTransform>().localPosition + new Vector3(0, 40, 0);
                    break;
                case "NewCharacterMenu":
                    GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 1800);
                    GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition + new Vector3(0, 100, 0);
                    GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition + new Vector3(0, 100, 0);
                    GameObject.Find("NameChooser").GetComponent<RectTransform>().localPosition = GameObject.Find("NameChooser").GetComponent<RectTransform>().localPosition + new Vector3(0, 80, 0);
                    GameObject.Find("SpellSamples").GetComponent<RectTransform>().localPosition = GameObject.Find("SpellSamples").GetComponent<RectTransform>().localPosition + new Vector3(0, 60, 0);
                    GameObject.Find("Description").GetComponent<RectTransform>().localPosition = GameObject.Find("Description").GetComponent<RectTransform>().localPosition + new Vector3(0, -50, 0);
                    GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition + new Vector3(0, -50, 0);
                    GameObject.Find("Button_GameStarter").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_GameStarter").GetComponent<RectTransform>().localPosition + new Vector3(0, -50, 0);
                    break;
                case "GameplayMenu":
                    GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 1800);
                    GameObject.Find("Consumables").GetComponent<RectTransform>().localPosition = GameObject.Find("Consumables").GetComponent<RectTransform>().localPosition + new Vector3(0, 150, 0);
                    GameObject.Find("PlayerName_Back").GetComponent<RectTransform>().localPosition = GameObject.Find("PlayerName_Back").GetComponent<RectTransform>().localPosition + new Vector3(0, 80, 0);
                    GameObject.Find("PlayerName_Front").GetComponent<RectTransform>().localPosition = GameObject.Find("PlayerName_Front").GetComponent<RectTransform>().localPosition + new Vector3(0, 80, 0);
                    GameObject.Find("CharacterImage").GetComponent<RectTransform>().localPosition = GameObject.Find("CharacterImage").GetComponent<RectTransform>().localPosition + new Vector3(0, 50, 0);
                    GameObject.Find("LevelTitle").GetComponent<RectTransform>().localPosition = GameObject.Find("LevelTitle").GetComponent<RectTransform>().localPosition + new Vector3(0, 20, 0);

                    GameObject.Find("Button_StartNextDay").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_StartNextDay").GetComponent<RectTransform>().localPosition + new Vector3(0, -20, 0);
                    GameObject.Find("Button_Attributes").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_Attributes").GetComponent<RectTransform>().localPosition + new Vector3(0, -20, 0);
                    GameObject.Find("Button_Specialization").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_Specialization").GetComponent<RectTransform>().localPosition + new Vector3(0, -60, 0);
                    GameObject.Find("Button_Items").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_Items").GetComponent<RectTransform>().localPosition + new Vector3(0, -60, 0);
                    GameObject.Find("Button_Characters").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_Characters").GetComponent<RectTransform>().localPosition + new Vector3(0, -100, 0);
                    GameObject.Find("Button_Skills").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_Skills").GetComponent<RectTransform>().localPosition + new Vector3(0, -100, 0);
                    GameObject.Find("Button_FairyShop").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_FairyShop").GetComponent<RectTransform>().localPosition + new Vector3(0, -140, 0);
                    GameObject.Find("Button_Achievements").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_Achievements").GetComponent<RectTransform>().localPosition + new Vector3(0, -140, 0);
                    GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition + new Vector3(0, -180, 0);
                    GameObject.Find("Button_Encyclopedia").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_Encyclopedia").GetComponent<RectTransform>().localPosition + new Vector3(0, -180, 0);

                    GameObject.Find("Button_StartNextDay").GetComponent<RectTransform>().sizeDelta = new Vector2(325, 110);
                    GameObject.Find("Button_Attributes").GetComponent<RectTransform>().sizeDelta = new Vector2(325, 110);
                    GameObject.Find("Button_Specialization").GetComponent<RectTransform>().sizeDelta = new Vector2(325, 110);
                    GameObject.Find("Button_Items").GetComponent<RectTransform>().sizeDelta = new Vector2(325, 110);
                    GameObject.Find("Button_Characters").GetComponent<RectTransform>().sizeDelta = new Vector2(325, 110);
                    GameObject.Find("Button_Skills").GetComponent<RectTransform>().sizeDelta = new Vector2(325, 110);
                    GameObject.Find("Button_FairyShop").GetComponent<RectTransform>().sizeDelta = new Vector2(325, 110);
                    GameObject.Find("Button_Achievements").GetComponent<RectTransform>().sizeDelta = new Vector2(325, 110);
                    GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().sizeDelta = new Vector2(325, 110);
                    GameObject.Find("Button_Encyclopedia").GetComponent<RectTransform>().sizeDelta = new Vector2(325, 110);
                    break;
                case "AttributesMenu":
                    GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 1800);
                    GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition + new Vector3(0, 50, 0);
                    GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition + new Vector3(0, 50, 0);
                    GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition + new Vector3(0, -150, 0);

                    GameObject.Find("CharacterAttribute").GetComponent<RectTransform>().localPosition = GameObject.Find("CharacterAttribute").GetComponent<RectTransform>().localPosition + new Vector3(0, -10, 0);
                    GameObject.Find("LevelAttrubute").GetComponent<RectTransform>().localPosition = GameObject.Find("LevelAttrubute").GetComponent<RectTransform>().localPosition + new Vector3(0, -20, 0);
                    GameObject.Find("DayPassedAttribute").GetComponent<RectTransform>().localPosition = GameObject.Find("DayPassedAttribute").GetComponent<RectTransform>().localPosition + new Vector3(0, -30, 0);
                    GameObject.Find("HealthAttribute").GetComponent<RectTransform>().localPosition = GameObject.Find("HealthAttribute").GetComponent<RectTransform>().localPosition + new Vector3(0, -40, 0);
                    GameObject.Find("HealthRegenAttribute").GetComponent<RectTransform>().localPosition = GameObject.Find("HealthRegenAttribute").GetComponent<RectTransform>().localPosition + new Vector3(0, -50, 0);
                    GameObject.Find("ArmorAttribute").GetComponent<RectTransform>().localPosition = GameObject.Find("ArmorAttribute").GetComponent<RectTransform>().localPosition + new Vector3(0, -60, 0);
                    GameObject.Find("AttackDamageAttribute").GetComponent<RectTransform>().localPosition = GameObject.Find("AttackDamageAttribute").GetComponent<RectTransform>().localPosition + new Vector3(0, -70, 0);
                    GameObject.Find("AttackSpeedAttribute").GetComponent<RectTransform>().localPosition = GameObject.Find("AttackSpeedAttribute").GetComponent<RectTransform>().localPosition + new Vector3(0, -80, 0);
                    GameObject.Find("CritChanceAttribute").GetComponent<RectTransform>().localPosition = GameObject.Find("CritChanceAttribute").GetComponent<RectTransform>().localPosition + new Vector3(0, -90, 0);
                    GameObject.Find("MagicPowerAttribute").GetComponent<RectTransform>().localPosition = GameObject.Find("MagicPowerAttribute").GetComponent<RectTransform>().localPosition + new Vector3(0, -100, 0);
                    GameObject.Find("CooldownReductionAttribute").GetComponent<RectTransform>().localPosition = GameObject.Find("CooldownReductionAttribute").GetComponent<RectTransform>().localPosition + new Vector3(0, -110, 0);
                    GameObject.Find("RunSpeedAttribute").GetComponent<RectTransform>().localPosition = GameObject.Find("RunSpeedAttribute").GetComponent<RectTransform>().localPosition + new Vector3(0, -120, 0);
                    break;
                case "SpecializationMenuTest":
                    GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 1800);
                    GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition + new Vector3(0, -150, 0);
                    GameObject.Find("Button_ResetSpecialization").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_ResetSpecialization").GetComponent<RectTransform>().localPosition + new Vector3(0, -150, 0);
                    GameObject.Find("SpecializationButtonBonusBox").GetComponent<RectTransform>().localPosition = GameObject.Find("SpecializationButtonBonusBox").GetComponent<RectTransform>().localPosition + new Vector3(0, -80, 0);
                    GameObject.Find("SpecializationPointsBox").GetComponent<RectTransform>().localPosition = GameObject.Find("SpecializationPointsBox").GetComponent<RectTransform>().localPosition + new Vector3(0, 60, 0);
                    GameObject.Find("Button_Zoom").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_Zoom").GetComponent<RectTransform>().localPosition + new Vector3(0, 60, 0);     
                    GameObject.Find("BackgroundWithHole").SetActive(false);
                    BackgroundWithHoleBig.SetActive(true);
                    GameObject.Find("SpecializationTreeBg").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 800);
                    break;
                case "ItemsMenu":
                    break;
                case "SkillMenu":
                    GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 1800);
                    GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition + new Vector3(0, 50, 0);
                    GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition + new Vector3(0, 50, 0);
                    GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition + new Vector3(0, -150, 0);
                    GameObject.Find("Button_ResetSpecialization").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_ResetSpecialization").GetComponent<RectTransform>().localPosition + new Vector3(0, -150, 0);
                    GameObject.Find("SkillDescriptionBox").GetComponent<RectTransform>().localPosition = GameObject.Find("SkillDescriptionBox").GetComponent<RectTransform>().localPosition + new Vector3(0, -80, 0);
                    break;
                case "FairyShop":
                    GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 1800);
                    GameObject.Find("Consumables").GetComponent<RectTransform>().localPosition = GameObject.Find("Consumables").GetComponent<RectTransform>().localPosition + new Vector3(0, 150, 0);
                    GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition + new Vector3(0, 50, 0);
                    GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition + new Vector3(0, 50, 0);
                    GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition + new Vector3(0, -150, 0);
                    GameObject.Find("ListBackGround").GetComponent<RectTransform>().localPosition = GameObject.Find("ListBackGround").GetComponent<RectTransform>().localPosition + new Vector3(0, -50, 0);
                    GameObject.Find("ListBackGround").GetComponent<RectTransform>().sizeDelta = new Vector2(740, 1150);
                    GameObject.Find("List").GetComponent<RectTransform>().offsetMin = GameObject.Find("List").GetComponent<RectTransform>().offsetMin + new Vector2(0, -100);
                    break;
                case "Encyclopedia":
                    GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 1800);
                    GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Front").GetComponent<RectTransform>().localPosition + new Vector3(0, 50, 0);
                    GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition = GameObject.Find("Title_Back").GetComponent<RectTransform>().localPosition + new Vector3(0, 50, 0);
                    GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition = GameObject.Find("Button_BackToMenu").GetComponent<RectTransform>().localPosition + new Vector3(0, -150, 0);
                    GameObject.Find("ListBackGround").GetComponent<RectTransform>().localPosition = GameObject.Find("ListBackGround").GetComponent<RectTransform>().localPosition + new Vector3(0, -50, 0);
                    GameObject.Find("ListBackGround").GetComponent<RectTransform>().sizeDelta = new Vector2(740, 1200);
                    GameObject.Find("List").GetComponent<RectTransform>().offsetMin = GameObject.Find("List").GetComponent<RectTransform>().offsetMin + new Vector2(0, -100);
                    break;
                case "Game":
                    GameObject.Find("WallImage").GetComponent<RectTransform>().localPosition = GameObject.Find("WallImage").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);
                    GameObject.Find("Bars").GetComponent<RectTransform>().localPosition = GameObject.Find("Bars").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);
                    GameObject.Find("PauseButton").GetComponent<RectTransform>().localPosition = GameObject.Find("PauseButton").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);
                    GameObject.Find("AttackButton").GetComponent<RectTransform>().localPosition = GameObject.Find("AttackButton").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);
                    GameObject.Find("Money").GetComponent<RectTransform>().localPosition = GameObject.Find("Money").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);
                    GameObject.Find("SkillSlots").GetComponent<RectTransform>().localPosition = GameObject.Find("SkillSlots").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);
                    GameObject.Find("WeaponSkill").GetComponent<RectTransform>().localPosition = GameObject.Find("WeaponSkill").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);

                    GameObject.Find("Left").transform.localPosition = GameObject.Find("Left").transform.localPosition + new Vector3(0.25f, 0, 0);
                    GameObject.Find("Right").transform.localPosition = GameObject.Find("Right").transform.localPosition + new Vector3(-0.25f, 0, 0);
                    GameObject.Find("Wall").transform.localPosition = GameObject.Find("Wall").transform.localPosition + new Vector3(0.1f, 0, 0);
                    game_skillButtonsNeedsToBePositioned = true;
                    break;
                default:
                    break;
            }
        }
    }

    bool medallionIsSet = false;
    int skillIsSetCounter = 0;
    bool skillIsSet = false;

    private void Update()
    {
        if (game_skillButtonsNeedsToBePositioned)
        {
            bool medallionIsAvailable = GameObject.Find("Player").GetComponent<Player_Attack>().medallionIsAvailable;
            if (!medallionIsSet && GameObject.Find("MedallionPower") != null)
            {
                GameObject.Find("MedallionPower").GetComponent<RectTransform>().localPosition = GameObject.Find("MedallionPower").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);
                medallionIsSet = true;
            }

            int skillCount = GameObject.Find("Player").GetComponent<Player_Attack>().skillCount;
            if (skillCount > 0)
            {
                if (skillIsSetCounter < 1 && GameObject.Find("FirstSkill") != null)
                {
                    GameObject.Find("FirstSkill").GetComponent<RectTransform>().localPosition = GameObject.Find("FirstSkill").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);
                    skillIsSetCounter++;
                }
                if (skillIsSetCounter < 2 && GameObject.Find("SecondSkill") != null)
                {
                    GameObject.Find("SecondSkill").GetComponent<RectTransform>().localPosition = GameObject.Find("SecondSkill").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);
                    skillIsSetCounter++;
                }
                if (skillIsSetCounter < 3 && GameObject.Find("ThirdSkill") != null)
                {
                    GameObject.Find("ThirdSkill").GetComponent<RectTransform>().localPosition = GameObject.Find("ThirdSkill").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);
                    skillIsSetCounter++;
                }
                if (skillIsSetCounter < 4 && GameObject.Find("FourthSkill") != null)
                {
                    GameObject.Find("FourthSkill").GetComponent<RectTransform>().localPosition = GameObject.Find("FourthSkill").GetComponent<RectTransform>().localPosition + new Vector3(0, -179, 0);
                    skillIsSetCounter++;
                }
                if (skillCount == skillIsSetCounter)
                {
                    skillIsSet = true;
                }
            }
            if(medallionIsSet && skillIsSet)
            {
                game_skillButtonsNeedsToBePositioned = false;
            }
        }
    }
}
