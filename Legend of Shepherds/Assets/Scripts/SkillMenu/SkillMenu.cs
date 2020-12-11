using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkillMenu : MonoBehaviour {

    public GameObject skill_1_1;
    public GameObject skill_1_2;
    public GameObject arrow_1;
    public GameObject savedSkillBorder_1;
    public GameObject skill_2_1;
    public GameObject skill_2_2;
    public GameObject arrow_2;
    public Text unlockText_2;
    public GameObject savedSkillBorder_2;
    public GameObject skill_3_1;
    public GameObject skill_3_2;
    public GameObject arrow_3;
    public Text unlockText_3;
    public GameObject savedSkillBorder_3;
    public GameObject skill_4_1;
    public GameObject skill_4_2;
    public GameObject arrow_4;
    public Text unlockText_4;
    public GameObject savedSkillBorder_4;

    public Text skillNameText;
    public Text skillDescriptionText;
    public Text save_reset_skillsText;
    public Text save_reset_skillsText_back;

    public Sprite arrowSelected;
    public Sprite arrowUnselected;

    int slotNumber;
    int level;
    string character;

    public int availableSkillsNumber;
    public int selectedSkillsNumber;
    public int activatedSkillsNumber;
    int lockedSkillsNumber;

    public bool savable;

    void Start()
    {
        GameObject.Find("Datas").GetComponent<SpecializationDatas>().Initialize();
        GameObject.Find("Datas").GetComponent<AttributeCalculator>().Initialize();

        slotNumber = PlayerPrefs.GetInt("GameSlot");
        level = PlayerPrefs.GetInt("Slot" + slotNumber + "_Level");
        character = PlayerPrefs.GetString("Slot" + slotNumber + "_Character");

        selectedSkillsNumber = 0;
        activatedSkillsNumber = 0;

        if (level >= 10)
            availableSkillsNumber = 4;
        else if (level >= 7)
            availableSkillsNumber = 3;
        else if (level >= 4)
            availableSkillsNumber = 2;
        else
            availableSkillsNumber = 1;

        SetAvaibleSkills();
        SetSelectedSkills();
        SetSkillDescription(1, 1);
    }

    void Update()
    {
        activatedSkillsNumber = 0;
        if (skill_1_1.GetComponent<SkillChooser>().IsActive())
            activatedSkillsNumber++;
        if (skill_1_2.GetComponent<SkillChooser>().IsActive())
            activatedSkillsNumber++;
        if (skill_2_1.GetComponent<SkillChooser>().IsActive())
            activatedSkillsNumber++;
        if (skill_2_2.GetComponent<SkillChooser>().IsActive())
            activatedSkillsNumber++;
        if (skill_3_1.GetComponent<SkillChooser>().IsActive())
            activatedSkillsNumber++;
        if (skill_3_2.GetComponent<SkillChooser>().IsActive())
            activatedSkillsNumber++;
        if (skill_4_1.GetComponent<SkillChooser>().IsActive())
            activatedSkillsNumber++;
        if (skill_4_2.GetComponent<SkillChooser>().IsActive())
            activatedSkillsNumber++;

        lockedSkillsNumber = 0;
        if (skill_1_1.GetComponent<SkillChooser>().IsLocked())
            lockedSkillsNumber++;
        if (skill_1_2.GetComponent<SkillChooser>().IsLocked())
            lockedSkillsNumber++;
        if (skill_2_1.GetComponent<SkillChooser>().IsLocked())
            lockedSkillsNumber++;
        if (skill_2_2.GetComponent<SkillChooser>().IsLocked())
            lockedSkillsNumber++;
        if (skill_3_1.GetComponent<SkillChooser>().IsLocked())
            lockedSkillsNumber++;
        if (skill_3_2.GetComponent<SkillChooser>().IsLocked())
            lockedSkillsNumber++;
        if (skill_4_1.GetComponent<SkillChooser>().IsLocked())
            lockedSkillsNumber++;
        if (skill_4_2.GetComponent<SkillChooser>().IsLocked())
            lockedSkillsNumber++;

        if (activatedSkillsNumber > 0)
            if (activatedSkillsNumber == availableSkillsNumber)
            {
                if (lockedSkillsNumber == availableSkillsNumber)
                {
                    save_reset_skillsText.text = "Reset skills\n";
                    save_reset_skillsText_back.text = "Reset skills\n";
                    savable = false;
                }
                else
                {
                    save_reset_skillsText.text = "Save skills\n";
                    save_reset_skillsText_back.text = "Save skills\n";
                    savable = true;
                }
            }
            else
            {
                save_reset_skillsText.text = "Reset skills\n";
                save_reset_skillsText_back.text = "Reset skills\n";
                savable = false;
            }
    }

    public void SetAvaibleSkills()
    {
        switch (availableSkillsNumber)
        {
            case 1:
                skill_1_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_1_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                arrow_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                skill_2_1.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                skill_2_2.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                arrow_2.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                unlockText_2.color = new Color(1f, 1f, 1f, 1f);

                skill_3_1.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                skill_3_2.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                arrow_3.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                unlockText_3.color = new Color(1f, 1f, 1f, 1f);

                skill_4_1.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                skill_4_2.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                arrow_4.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                unlockText_4.color = new Color(1f, 1f, 1f, 1f);
                break;
            case 2:
                skill_1_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_1_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                arrow_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                skill_2_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_2_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                arrow_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                unlockText_2.color = new Color(1f, 1f, 1f, 0f);
                unlockText_2.transform.position = Vector3.zero;

                skill_3_1.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                skill_3_2.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                arrow_3.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                unlockText_3.color = new Color(1f, 1f, 1f, 1f);

                skill_4_1.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                skill_4_2.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                arrow_4.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                unlockText_4.color = new Color(1f, 1f, 1f, 1f);
                break;
            case 3:
                skill_1_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_1_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                arrow_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                skill_2_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_2_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                arrow_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                unlockText_2.color = new Color(1f, 1f, 1f, 0f);
                unlockText_2.transform.position = Vector3.zero;

                skill_3_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_3_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                arrow_3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                unlockText_3.color = new Color(1f, 1f, 1f, 0f);
                unlockText_3.transform.position = Vector3.zero;

                skill_4_1.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                skill_4_2.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                arrow_4.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 0.4f);
                unlockText_4.color = new Color(1f, 1f, 1f, 1f);
                break;
            case 4:
                skill_1_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_1_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                arrow_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                skill_2_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_2_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                arrow_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                unlockText_2.color = new Color(1f, 1f, 1f, 0f);
                unlockText_2.transform.position = Vector3.zero;

                skill_3_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_3_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                arrow_3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                unlockText_3.color = new Color(1f, 1f, 1f, 0f);
                unlockText_3.transform.position = Vector3.zero;

                skill_4_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skill_4_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                arrow_4.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                unlockText_4.color = new Color(1f, 1f, 1f, 0f);
                unlockText_4.transform.position = Vector3.zero;
                break;
            default:
                break;
        }
    }

    public void SetSelectedSkills()
    {
        GameObject DataGO = GameObject.Find("Datas");
        int skills = PlayerPrefs.GetInt("Slot" + slotNumber + "_Skills");

        int tmp = skills / 1000;
        switch (tmp)
        {
            case 1:
                skill_1_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 1, true, character);
                skill_1_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 2, false, character);
                skill_1_1.GetComponent<SkillChooser>().SetLocked(true);
                skill_1_1.GetComponent<SkillChooser>().SetActive(true);
                arrow_1.GetComponent<Image>().sprite = arrowSelected;
                arrow_1.transform.localScale = new Vector3(1f, 1f);
                savedSkillBorder_1.transform.position = skill_1_1.transform.position;
                savedSkillBorder_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                selectedSkillsNumber++;
                break;
            case 2:
                skill_1_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 1, false, character);
                skill_1_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 2, true, character);
                skill_1_2.GetComponent<SkillChooser>().SetLocked(true);
                skill_1_2.GetComponent<SkillChooser>().SetActive(true);
                arrow_1.GetComponent<Image>().sprite = arrowSelected;
                arrow_1.transform.localScale = new Vector3(-1f, 1f);
                savedSkillBorder_1.transform.position = skill_1_2.transform.position;
                savedSkillBorder_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                selectedSkillsNumber++;
                break;
            case 9:
                skill_1_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 1, false, character);
                skill_1_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 2, false, character);
                arrow_1.GetComponent<Image>().sprite = arrowUnselected;
                arrow_1.transform.localScale = new Vector3(1f, 1f);
                savedSkillBorder_1.transform.position = new Vector3(0f, 0f);
                savedSkillBorder_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                break;
            default:
                skill_1_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 1, false, character);
                skill_1_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 2, false, character);
                arrow_1.GetComponent<Image>().sprite = arrowUnselected;
                arrow_1.transform.localScale = new Vector3(1f, 1f);
                savedSkillBorder_1.transform.position = new Vector3(0f, 0f);
                savedSkillBorder_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                break;
        }

        tmp = skills / 100 % 10;
        switch (tmp)
        {
            case 1:
                skill_2_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 1, true, character);
                skill_2_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 2, false, character);
                skill_2_1.GetComponent<SkillChooser>().SetLocked(true);
                skill_2_1.GetComponent<SkillChooser>().SetActive(true);
                arrow_2.GetComponent<Image>().sprite = arrowSelected;
                arrow_2.transform.localScale = new Vector3(1f, 1f);
                savedSkillBorder_2.transform.position = skill_2_1.transform.position;
                savedSkillBorder_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                selectedSkillsNumber++;
                break;
            case 2:
                skill_2_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 1, false, character);
                skill_2_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 2, true, character);
                skill_2_2.GetComponent<SkillChooser>().SetLocked(true);
                skill_2_2.GetComponent<SkillChooser>().SetActive(true);
                arrow_2.GetComponent<Image>().sprite = arrowSelected;
                arrow_2.transform.localScale = new Vector3(-1f, 1f);
                savedSkillBorder_2.transform.position = skill_2_2.transform.position;
                savedSkillBorder_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                selectedSkillsNumber++;
                break;
            case 9:
                skill_2_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 1, false, character);
                skill_2_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 2, false, character);
                arrow_2.GetComponent<Image>().sprite = arrowUnselected;
                arrow_2.transform.localScale = new Vector3(1f, 1f);
                savedSkillBorder_2.transform.position = new Vector3(0f, 0f);
                savedSkillBorder_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                break;
            default:
                skill_2_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 1, false, character);
                skill_2_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 2, false, character);
                arrow_2.GetComponent<Image>().sprite = arrowUnselected;
                savedSkillBorder_2.transform.position = new Vector3(0f, 0f);
                savedSkillBorder_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                arrow_2.transform.localScale = new Vector3(1f, 1f);
                break;
        }

        tmp = skills / 10 % 10;
        switch (tmp)
        {
            case 1:
                skill_3_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 1, true, character);
                skill_3_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 2, false, character);
                skill_3_1.GetComponent<SkillChooser>().SetLocked(true);
                skill_3_1.GetComponent<SkillChooser>().SetActive(true);
                arrow_3.GetComponent<Image>().sprite = arrowSelected;
                arrow_3.transform.localScale = new Vector3(1f, 1f);
                savedSkillBorder_3.transform.position = skill_3_1.transform.position;
                savedSkillBorder_3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                selectedSkillsNumber++;
                break;
            case 2:
                skill_3_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 1, false, character);
                skill_3_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 2, true, character);
                skill_3_2.GetComponent<SkillChooser>().SetLocked(true);
                skill_3_2.GetComponent<SkillChooser>().SetActive(true);
                arrow_3.GetComponent<Image>().sprite = arrowSelected;
                arrow_3.transform.localScale = new Vector3(-1f, 1f);
                savedSkillBorder_3.transform.position = skill_3_2.transform.position;
                savedSkillBorder_3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                selectedSkillsNumber++;
                break;
            case 9:
                skill_3_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 1, false, character);
                skill_3_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 2, false, character);
                arrow_3.GetComponent<Image>().sprite = arrowUnselected;
                arrow_3.transform.localScale = new Vector3(1f, 1f);
                savedSkillBorder_3.transform.position = new Vector3(0f, 0f);
                savedSkillBorder_3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                break;
            default:
                skill_3_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 1, false, character);
                skill_3_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 2, false, character);
                arrow_3.GetComponent<Image>().sprite = arrowUnselected;
                arrow_3.transform.localScale = new Vector3(1f, 1f);
                savedSkillBorder_3.transform.position = new Vector3(0f, 0f);
                savedSkillBorder_3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                break;
        }

        tmp = skills % 10;
        switch (tmp)
        {
            case 1:
                skill_4_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 1, true, character);
                skill_4_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 2, false, character);
                skill_4_1.GetComponent<SkillChooser>().SetLocked(true);
                skill_4_1.GetComponent<SkillChooser>().SetActive(true);
                arrow_4.GetComponent<Image>().sprite = arrowSelected;
                arrow_4.transform.localScale = new Vector3(1f, 1f);
                savedSkillBorder_4.transform.position = skill_4_1.transform.position;
                savedSkillBorder_4.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                selectedSkillsNumber++;
                break;
            case 2:
                skill_4_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 1, false, character);
                skill_4_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 2, true, character);
                skill_4_2.GetComponent<SkillChooser>().SetLocked(true);
                skill_4_2.GetComponent<SkillChooser>().SetActive(true);
                arrow_4.GetComponent<Image>().sprite = arrowSelected;
                arrow_4.transform.localScale = new Vector3(-1f, 1f);
                savedSkillBorder_4.transform.position = skill_4_2.transform.position;
                savedSkillBorder_4.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                selectedSkillsNumber++;
                break;
            case 9:
                skill_4_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 1, false, character);
                skill_4_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 2, false, character);
                arrow_4.GetComponent<Image>().sprite = arrowUnselected;
                arrow_4.transform.localScale = new Vector3(1f, 1f);
                savedSkillBorder_4.transform.position = new Vector3(0f, 0f);
                savedSkillBorder_4.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                break;
            default:
                skill_4_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 1, false, character);
                skill_4_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 2, false, character);
                arrow_4.GetComponent<Image>().sprite = arrowUnselected;
                arrow_4.transform.localScale = new Vector3(1f, 1f);
                savedSkillBorder_4.transform.position = new Vector3(0f, 0f);
                savedSkillBorder_4.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                break;
        }
    }

    public bool IsActivatableSkill(int skillLvl, int skillNr)
    {
        switch (skillLvl)
        {
            case 1:
                return !(skill_1_1.GetComponent<SkillChooser>().IsLocked() || skill_1_2.GetComponent<SkillChooser>().IsLocked());
            case 2:
                if (availableSkillsNumber >= 2)
                    return !(skill_2_1.GetComponent<SkillChooser>().IsLocked() || skill_2_2.GetComponent<SkillChooser>().IsLocked());
                else
                    return false;
            case 3:
                if (availableSkillsNumber >= 3)
                    return !(skill_3_1.GetComponent<SkillChooser>().IsLocked() || skill_3_2.GetComponent<SkillChooser>().IsLocked());
                else
                    return false;
            case 4:
                if (availableSkillsNumber >= 4)
                    return !(skill_4_1.GetComponent<SkillChooser>().IsLocked() || skill_4_2.GetComponent<SkillChooser>().IsLocked());
                else
                    return false;
            default:
                return false;
        }
    }

    public void ActivateSkill(int skillLvl, int skillNr)
    {
        GameObject DataGO = GameObject.Find("Datas");

        switch (skillLvl)
        {
            case 1:
                if (skillNr == 1)
                {
                    skill_1_1.GetComponent<SkillChooser>().SetActive(true);
                    skill_1_2.GetComponent<SkillChooser>().SetActive(false);
                    skill_1_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 1, true, character);
                    skill_1_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 2, false, character);
                    arrow_1.GetComponent<Image>().sprite = arrowSelected;
                    arrow_1.transform.localScale = new Vector3(1f, 1f);
                }
                else
                {
                    skill_1_1.GetComponent<SkillChooser>().SetActive(false);
                    skill_1_2.GetComponent<SkillChooser>().SetActive(true);
                    skill_1_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 1, false, character);
                    skill_1_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(1, 2, true, character);
                    arrow_1.GetComponent<Image>().sprite = arrowSelected;
                    arrow_1.transform.localScale = new Vector3(-1f, 1f);
                }
                break;
            case 2:
                if (skillNr == 1)
                {
                    skill_2_1.GetComponent<SkillChooser>().SetActive(true);
                    skill_2_2.GetComponent<SkillChooser>().SetActive(false);
                    skill_2_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 1, true, character);
                    skill_2_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 2, false, character);
                    arrow_2.GetComponent<Image>().sprite = arrowSelected;
                    arrow_2.transform.localScale = new Vector3(1f, 1f);
                }
                else
                {
                    skill_2_1.GetComponent<SkillChooser>().SetActive(false);
                    skill_2_2.GetComponent<SkillChooser>().SetActive(true);
                    skill_2_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 1, false, character);
                    skill_2_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(2, 2, true, character);
                    arrow_2.GetComponent<Image>().sprite = arrowSelected;
                    arrow_2.transform.localScale = new Vector3(-1f, 1f);
                }
                break;
            case 3:
                if (skillNr == 1)
                {
                    skill_3_1.GetComponent<SkillChooser>().SetActive(true);
                    skill_3_2.GetComponent<SkillChooser>().SetActive(false);
                    skill_3_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 1, true, character);
                    skill_3_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 2, false, character);
                    arrow_3.GetComponent<Image>().sprite = arrowSelected;
                    arrow_3.transform.localScale = new Vector3(1f, 1f);
                }
                else
                {
                    skill_3_1.GetComponent<SkillChooser>().SetActive(false);
                    skill_3_2.GetComponent<SkillChooser>().SetActive(true);
                    skill_3_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 1, false, character);
                    skill_3_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(3, 2, true, character);
                    arrow_3.GetComponent<Image>().sprite = arrowSelected;
                    arrow_3.transform.localScale = new Vector3(-1f, 1f);
                }
                break;
            case 4:
                if (skillNr == 1)
                {
                    skill_4_1.GetComponent<SkillChooser>().SetActive(true);
                    skill_4_2.GetComponent<SkillChooser>().SetActive(false);
                    skill_4_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 1, true, character);
                    skill_4_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 2, false, character);
                    arrow_4.GetComponent<Image>().sprite = arrowSelected;
                    arrow_4.transform.localScale = new Vector3(1f, 1f);
                }
                else
                {
                    skill_4_1.GetComponent<SkillChooser>().SetActive(false);
                    skill_4_2.GetComponent<SkillChooser>().SetActive(true);
                    skill_4_1.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 1, false, character);
                    skill_4_2.GetComponent<Image>().sprite = DataGO.GetComponent<SkillMenuImages>().GetSkillSprite(4, 2, true, character);
                    arrow_4.GetComponent<Image>().sprite = arrowSelected;
                    arrow_4.transform.localScale = new Vector3(-1f, 1f);
                }
                break;
            default:
                break;
        }
    }

    public void SetSkillDescription(int skillLvl, int skillNr)
    {
        if (availableSkillsNumber >= skillLvl)
        {
            skillNameText.text = GameObject.Find("Datas").GetComponent<SkillMenuSkillNames>().GetSkillName(skillLvl, skillNr, character);
            skillDescriptionText.text = GameObject.Find("Datas").GetComponent<SkillMenuDescriptions>().GetSkillDescription(skillLvl, skillNr, character);
        }
    }

    public void ActivateOrResetSkills()
    {
        if (savable)
        {
            PlayerPrefs.SetInt("Slot" + slotNumber + "_SkillsNumber", activatedSkillsNumber);
            ActivateSkills();
            SceneManager.LoadScene("SkillMenu");
        }
        else
        {
            GameObject.Find("Datas").GetComponent<Save_Load>().ResetSkills(slotNumber);
            SceneManager.LoadScene("SkillMenu");
        }

    }

    public void ActivateSkills()
    {
        int skillNumber = 0;

        if (skill_1_1.GetComponent<SkillChooser>().IsActive())
            skillNumber += 1000;
        else
        if (skill_1_2.GetComponent<SkillChooser>().IsActive())
            skillNumber += 2000;
        else
            skillNumber += 9000;

        if (skill_2_1.GetComponent<SkillChooser>().IsActive())
            skillNumber += 100;
        else
        if (skill_2_2.GetComponent<SkillChooser>().IsActive())
            skillNumber += 200;
        else
            skillNumber += 900;

        if (skill_3_1.GetComponent<SkillChooser>().IsActive())
            skillNumber += 10;
        else
         if (skill_3_2.GetComponent<SkillChooser>().IsActive())
            skillNumber += 20;
        else
            skillNumber += 90;

        if (skill_4_1.GetComponent<SkillChooser>().IsActive())
            skillNumber += 1;
        else
         if (skill_4_2.GetComponent<SkillChooser>().IsActive())
            skillNumber += 2;
        else
            skillNumber += 9;

        PlayerPrefs.SetInt("Slot" + slotNumber + "_Skills", skillNumber);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("GameplayMenu");
    }
}
