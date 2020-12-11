using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpecializationTest : MonoBehaviour
{
    public Text specializationBonus_text;
    public Text specializationPoints_text;

    public GameObject pathFinderGO;

    public Button H_1_1;
    public Button H_1_2;
    public Button H_1_4;
    public Button H_2_1;
    public Button H_2_2;
    public Button H_2_4;
    public Button H_3_1;
    public Button H_3_2;
    public Button H_3_4;
    public Button H_3_8;
    public Button H_3_16;
    public Button H_3_32;
    public Button H_4_1;
    public Button H_4_2;
    public Button H_4_4;
    public Button H_5_1;
    public Button H_5_2;
    public Button H_5_4;
    public Button H_5_8;

    public Button A_1_1;
    public Button A_1_2;
    public Button A_1_4;
    public Button A_2_1;
    public Button A_2_2;
    public Button A_2_4;
    public Button A_3_1;
    public Button A_3_2;
    public Button A_3_4;
    public Button A_3_8;
    public Button A_3_16;
    public Button A_3_32;
    public Button A_4_1;
    public Button A_4_2;
    public Button A_4_4;
    public Button A_5_1;
    public Button A_5_2;
    public Button A_5_4;
    public Button A_5_8;

    public Button S_1_1;
    public Button S_1_2;
    public Button S_1_4;
    public Button S_2_1;
    public Button S_2_2;
    public Button S_2_4;
    public Button S_3_1;
    public Button S_3_2;
    public Button S_3_4;
    public Button S_3_8;
    public Button S_3_16;
    public Button S_3_32;
    public Button S_4_1;
    public Button S_4_2;
    public Button S_4_4;
    public Button S_5_1;
    public Button S_5_2;
    public Button S_5_4;
    public Button S_5_8;

    public Text SaveOrResetSpecializationsText;
    public Text SaveOrResetSpecializationsText_back;

    public Image selectedSpecializationImage;

    public Sprite attackButton;
    public Sprite attackButtonLocked;
    public Sprite attackButtonActivated;
    

    public Button zoomButton;
    public Sprite zoomInButtonSprite;
    public Sprite zoomOutButtonSprite;

    public ScrollRect scrollView;

    int slotNumber;

    int spentSpecializationPoints;

    int nearbyTalentNumbers;

    int specializationPoints;
    int lockedSpecializationPoints;

    public bool zoomedIn;

    Vector2 basePosition;

    GameObject[] specializationButtons;

    GameObject selectedSpecialization;

    public Button addPointButton;
    public Button takePointButton;

    void Start()
    {
        GameObject.Find("Datas").GetComponent<SpecializationDatas>().Initialize();

        slotNumber = PlayerPrefs.GetInt("GameSlot");
        spentSpecializationPoints = 0;
        zoomedIn = false;
        scrollView.horizontal = false;
        scrollView.vertical = false;
        lockedSpecializationPoints = 0;

        specializationPoints = PlayerPrefs.GetInt("Slot" + slotNumber + "_Level") / 1;

        specializationButtons = GameObject.FindGameObjectsWithTag("SpecializationButton");

        foreach (GameObject b in specializationButtons)
        {
            Button button = b.GetComponent<Button>();
            button.GetComponent<SpecializationButtonStatus>().InitializeButton();
            button.GetComponent<Image>().sprite = attackButton;
        }

        SetHealthButtonsStatus();
        SetAttackDamageButtonsStatus();
        SetSkillPowerButtonsStatus();

        foreach (GameObject b in specializationButtons)
        {
            Button button = b.GetComponent<Button>();
            lockedSpecializationPoints += button.GetComponent<SpecializationButtonStatus>().GetPoints();
        }
        UpdateSpecializationPoints();
    }

    public bool HasAvailableSpecializationPoints()
    {
        if (specializationPoints - lockedSpecializationPoints - spentSpecializationPoints > 0)
            return true;
        else return false;
    }

    public void UpdateSpecializationPoints()
    {
        spentSpecializationPoints = 0;
        foreach (GameObject b in specializationButtons)
        {
            Button button = b.GetComponent<Button>();
            spentSpecializationPoints += button.GetComponent<SpecializationButtonStatus>().GetSpentPoints();
            if (button.GetComponent<SpecializationButtonStatus>().HasAvailableStacks())
                button.GetComponent<Image>().sprite = attackButtonActivated;
            else
                button.GetComponent<Image>().sprite = attackButton;
        }

        specializationPoints_text.text = "You can spend " + (specializationPoints - lockedSpecializationPoints - spentSpecializationPoints) + " specialization points";

        if (spentSpecializationPoints > 0)
        {
            SaveOrResetSpecializationsText.GetComponent<Text>().text = "Save spec.\n";
            SaveOrResetSpecializationsText_back.GetComponent<Text>().text = "Save spec.\n";
        }
        else
        {
            SaveOrResetSpecializationsText.GetComponent<Text>().text = "Reset spec.\n";
            SaveOrResetSpecializationsText_back.GetComponent<Text>().text = "Reset spec.\n";
        }
    }

    public void SetHealthButtonsStatus()
    {
        string talent = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_Health");

        H_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();

        int buttonTalentNum = int.Parse(talent.Substring(0, 1));

        if (buttonTalentNum != 0)
        {
            H_1_1.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            H_1_1.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            H_1_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            H_1_1.GetComponent<Image>().sprite = attackButtonLocked;
            H_1_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            H_1_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            H_3_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            H_3_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();

            A_1_1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber(100);
            S_1_1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber(100);
        }

        buttonTalentNum = int.Parse(talent.Substring(1, 1));

        if (buttonTalentNum != 0)
        {
            H_1_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_1_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_1_2.GetComponent<Image>().sprite = attackButtonLocked;
                H_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_2_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(2, 1));

        if (buttonTalentNum != 0)
        {
            H_1_4.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_1_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_1_4.GetComponent<Image>().sprite = attackButtonLocked;
                H_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_2_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_5_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(3, 1));

        if (buttonTalentNum != 0)
        {
            H_2_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_2_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_2_1.GetComponent<Image>().sprite = attackButtonLocked;
                H_1_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_2_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_1_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(4, 1));

        if (buttonTalentNum != 0)
        {
            H_2_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_2_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_2_2.GetComponent<Image>().sprite = attackButtonLocked;
                H_2_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_2_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(5, 1));

        if (buttonTalentNum != 0)
        {
            H_2_4.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            H_2_4.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            H_2_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            H_2_4.GetComponent<Image>().sprite = attackButtonLocked;
            H_2_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }

        buttonTalentNum = int.Parse(talent.Substring(6, 1));

        if (buttonTalentNum != 0)
        {
            H_3_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_3_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_3_1.GetComponent<Image>().sprite = attackButtonLocked;
                H_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_3_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_4_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(7, 1));

        if (buttonTalentNum != 0)
        {
            H_3_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_3_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_3_2.GetComponent<Image>().sprite = attackButtonLocked;
                H_3_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_3_16.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(8, 1));

        if (buttonTalentNum != 0)
        {
            H_3_4.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_3_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_3_4.GetComponent<Image>().sprite = attackButtonLocked;
                H_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_3_8.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_5_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(9, 1));

        if (buttonTalentNum != 0)
        {
            H_3_8.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_3_8.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_3_8.GetComponent<Image>().sprite = attackButtonLocked;
                H_3_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_3_16.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(10, 1));

        if (buttonTalentNum != 0)
        {
            H_3_16.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_3_16.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_3_16.GetComponent<Image>().sprite = attackButtonLocked;
                H_3_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_3_8.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_3_32.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(11, 1));

        if (buttonTalentNum != 0)
        {
            H_3_32.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            H_3_32.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            H_3_32.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            H_3_32.GetComponent<Image>().sprite = attackButtonLocked;
            H_3_16.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }

        buttonTalentNum = int.Parse(talent.Substring(12, 1));

        if (buttonTalentNum != 0)
        {
            H_4_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_4_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_4_1.GetComponent<Image>().sprite = attackButtonLocked;
                H_3_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_4_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(13, 1));

        if (buttonTalentNum != 0)
        {
            H_4_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_4_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_4_2.GetComponent<Image>().sprite = attackButtonLocked;
                H_4_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_4_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(14, 1));

        if (buttonTalentNum != 0)
        {
            H_4_4.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            H_4_4.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            H_4_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            H_4_4.GetComponent<Image>().sprite = attackButtonLocked;
            H_4_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }

        buttonTalentNum = int.Parse(talent.Substring(15, 1));

        if (buttonTalentNum != 0)
        {
            H_5_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_5_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_5_1.GetComponent<Image>().sprite = attackButtonLocked;
                H_3_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_5_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(16, 1));

        if (buttonTalentNum != 0)
        {
            H_5_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_5_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_5_2.GetComponent<Image>().sprite = attackButtonLocked;
                H_5_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_5_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_5_8.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(17, 1));

        if (buttonTalentNum != 0)
        {
            H_5_4.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            H_5_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                H_5_4.GetComponent<Image>().sprite = attackButtonLocked;
                H_5_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_1_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(18, 1));

        if (buttonTalentNum != 0)
        {
            H_5_8.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            H_5_8.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            H_5_8.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            H_5_8.GetComponent<Image>().sprite = attackButtonLocked;
            H_5_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }
    }

    public void SetAttackDamageButtonsStatus()
    {
        string talent = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_AttackDamage");

        A_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();

        int buttonTalentNum = int.Parse(talent.Substring(0, 1));

        if (buttonTalentNum != 0)
        {
            A_1_1.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            A_1_1.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            A_1_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            A_1_1.GetComponent<Image>().sprite = attackButtonLocked;
            A_1_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            A_1_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            A_3_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            A_3_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();

            H_1_1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber(100);
            S_1_1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber(100);
        }

        buttonTalentNum = int.Parse(talent.Substring(1, 1));

        if (buttonTalentNum != 0)
        {
            A_1_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_1_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_1_2.GetComponent<Image>().sprite = attackButtonLocked;
                A_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_2_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(2, 1));

        if (buttonTalentNum != 0)
        {
            A_1_4.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_1_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_1_4.GetComponent<Image>().sprite = attackButtonLocked;
                A_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_2_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_5_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(3, 1));

        if (buttonTalentNum != 0)
        {
            A_2_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_2_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_2_1.GetComponent<Image>().sprite = attackButtonLocked;
                A_1_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_2_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_1_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(4, 1));

        if (buttonTalentNum != 0)
        {
            A_2_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_2_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_2_2.GetComponent<Image>().sprite = attackButtonLocked;
                A_2_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_2_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(5, 1));

        if (buttonTalentNum != 0)
        {
            A_2_4.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            A_2_4.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            A_2_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            A_2_4.GetComponent<Image>().sprite = attackButtonLocked;
            A_2_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }

        buttonTalentNum = int.Parse(talent.Substring(6, 1));

        if (buttonTalentNum != 0)
        {
            A_3_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_3_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_3_1.GetComponent<Image>().sprite = attackButtonLocked;
                A_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_3_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_4_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(7, 1));

        if (buttonTalentNum != 0)
        {
            A_3_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_3_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_3_2.GetComponent<Image>().sprite = attackButtonLocked;
                A_3_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_3_16.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(8, 1));

        if (buttonTalentNum != 0)
        {
            A_3_4.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_3_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_3_4.GetComponent<Image>().sprite = attackButtonLocked;
                A_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_3_8.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_5_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(9, 1));

        if (buttonTalentNum != 0)
        {
            A_3_8.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_3_8.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_3_8.GetComponent<Image>().sprite = attackButtonLocked;
                A_3_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_3_16.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(10, 1));

        if (buttonTalentNum != 0)
        {
            A_3_16.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_3_16.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_3_16.GetComponent<Image>().sprite = attackButtonLocked;
                A_3_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_3_8.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_3_32.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(11, 1));

        if (buttonTalentNum != 0)
        {
            A_3_32.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            A_3_32.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            A_3_32.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            A_3_32.GetComponent<Image>().sprite = attackButtonLocked;
            A_3_16.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }

        buttonTalentNum = int.Parse(talent.Substring(12, 1));

        if (buttonTalentNum != 0)
        {
            A_4_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_4_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_4_1.GetComponent<Image>().sprite = attackButtonLocked;
                A_3_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_4_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(13, 1));

        if (buttonTalentNum != 0)
        {
            A_4_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_4_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_4_2.GetComponent<Image>().sprite = attackButtonLocked;
                A_4_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_4_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(14, 1));

        if (buttonTalentNum != 0)
        {
            A_4_4.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            A_4_4.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            A_4_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            A_4_4.GetComponent<Image>().sprite = attackButtonLocked;
            A_4_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }

        buttonTalentNum = int.Parse(talent.Substring(15, 1));

        if (buttonTalentNum != 0)
        {
            A_5_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_5_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_5_1.GetComponent<Image>().sprite = attackButtonLocked;
                A_3_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_5_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(16, 1));

        if (buttonTalentNum != 0)
        {
            A_5_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_5_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_5_2.GetComponent<Image>().sprite = attackButtonLocked;
                A_5_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_5_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_5_8.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(17, 1));

        if (buttonTalentNum != 0)
        {
            A_5_4.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            A_5_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                A_5_4.GetComponent<Image>().sprite = attackButtonLocked;
                A_5_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_1_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(18, 1));

        if (buttonTalentNum != 0)
        {
            A_5_8.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            A_5_8.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            A_5_8.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            A_5_8.GetComponent<Image>().sprite = attackButtonLocked;
            A_5_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }
    }

    public void SetSkillPowerButtonsStatus()
    {
        string talent = PlayerPrefs.GetString("Slot" + slotNumber + "_Specialization_SpellPower");

        S_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();

        int buttonTalentNum = int.Parse(talent.Substring(0, 1));

        if (buttonTalentNum != 0)
        {
            S_1_1.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            S_1_1.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            S_1_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            S_1_1.GetComponent<Image>().sprite = attackButtonLocked;
            S_1_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            S_1_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            S_3_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            S_3_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();

            H_1_1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber(100);
            A_1_1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber(100);
        }

        buttonTalentNum = int.Parse(talent.Substring(1, 1));

        if (buttonTalentNum != 0)
        {
            S_1_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_1_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_1_2.GetComponent<Image>().sprite = attackButtonLocked;
                S_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_2_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(2, 1));

        if (buttonTalentNum != 0)
        {
            S_1_4.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_1_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_1_4.GetComponent<Image>().sprite = attackButtonLocked;
                S_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_2_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                A_5_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(3, 1));

        if (buttonTalentNum != 0)
        {
            S_2_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_2_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_2_1.GetComponent<Image>().sprite = attackButtonLocked;
                S_1_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_2_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_1_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(4, 1));

        if (buttonTalentNum != 0)
        {
            S_2_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_2_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_2_2.GetComponent<Image>().sprite = attackButtonLocked;
                S_2_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_2_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(5, 1));

        if (buttonTalentNum != 0)
        {
            S_2_4.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            S_2_4.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            S_2_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            S_2_4.GetComponent<Image>().sprite = attackButtonLocked;
            S_2_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }

        buttonTalentNum = int.Parse(talent.Substring(6, 1));

        if (buttonTalentNum != 0)
        {
            S_3_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_3_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_3_1.GetComponent<Image>().sprite = attackButtonLocked;
                S_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_3_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_4_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(7, 1));

        if (buttonTalentNum != 0)
        {
            S_3_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_3_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_3_2.GetComponent<Image>().sprite = attackButtonLocked;
                S_3_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_3_16.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(8, 1));

        if (buttonTalentNum != 0)
        {
            S_3_4.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_3_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_3_4.GetComponent<Image>().sprite = attackButtonLocked;
                S_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_3_8.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_5_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(9, 1));

        if (buttonTalentNum != 0)
        {
            S_3_8.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_3_8.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_3_8.GetComponent<Image>().sprite = attackButtonLocked;
                S_3_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_3_16.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(10, 1));

        if (buttonTalentNum != 0)
        {
            S_3_16.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_3_16.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_3_16.GetComponent<Image>().sprite = attackButtonLocked;
                S_3_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_3_8.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_3_32.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(11, 1));

        if (buttonTalentNum != 0)
        {
            S_3_32.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            S_3_32.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            S_3_32.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            S_3_32.GetComponent<Image>().sprite = attackButtonLocked;
            S_3_16.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }

        buttonTalentNum = int.Parse(talent.Substring(12, 1));

        if (buttonTalentNum != 0)
        {
            S_4_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_4_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_4_1.GetComponent<Image>().sprite = attackButtonLocked;
                S_3_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_4_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(13, 1));

        if (buttonTalentNum != 0)
        {
            S_4_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_4_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_4_2.GetComponent<Image>().sprite = attackButtonLocked;
                S_4_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_4_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(14, 1));

        if (buttonTalentNum != 0)
        {
            S_4_4.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            S_4_4.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            S_4_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            S_4_4.GetComponent<Image>().sprite = attackButtonLocked;
            S_4_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }

        buttonTalentNum = int.Parse(talent.Substring(15, 1));

        if (buttonTalentNum != 0)
        {
            S_5_1.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_5_1.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_5_1.GetComponent<Image>().sprite = attackButtonLocked;
                S_3_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_5_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(16, 1));

        if (buttonTalentNum != 0)
        {
            S_5_2.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_5_2.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_5_2.GetComponent<Image>().sprite = attackButtonLocked;
                S_5_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_5_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                S_5_8.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(17, 1));

        if (buttonTalentNum != 0)
        {
            S_5_4.GetComponent<SpecializationButtonStatus>().SetLockedStacks(buttonTalentNum);
            S_5_4.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            if (buttonTalentNum == 3)
            {
                S_5_4.GetComponent<Image>().sprite = attackButtonLocked;
                S_5_2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                H_1_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
            }
        }

        buttonTalentNum = int.Parse(talent.Substring(18, 1));

        if (buttonTalentNum != 0)
        {
            S_5_8.GetComponent<SpecializationButtonStatus>().SetMajorSpecializationFull();
            S_5_8.GetComponent<SpecializationButtonStatus>().SetLockedMajorSpecialization();
            S_5_8.GetComponent<SpecializationButtonStatus>().SetAvailable(true);
            S_5_8.GetComponent<Image>().sprite = attackButtonLocked;
            S_5_4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
        }
    }

    public void SaveOrResetSpecializations()
    {
        if (spentSpecializationPoints > 0)
        {
            PlayerPrefs.SetInt("Slot" + slotNumber + "_Specialization_ActivatedNumber", lockedSpecializationPoints + spentSpecializationPoints);
            SaveSpecializations();
            SceneManager.LoadScene("SpecializationMenuTest");
        }
        else
        {
            GameObject.Find("Datas").GetComponent<Save_Load>().ResetSpecialization(slotNumber);
            SceneManager.LoadScene("SpecializationMenuTest");
        }
        
    }

    public void Zoom()
    {
        if (!zoomedIn)
        {
            basePosition = GameObject.Find("Specialization").transform.position;
            GameObject.Find("Specialization").transform.localScale *= 2.5f;
            scrollView.horizontal = true;
            scrollView.vertical = true;
            zoomButton.GetComponent<Image>().sprite = zoomOutButtonSprite;
            zoomedIn = true;
        }
        else
        {
            GameObject.Find("Specialization").transform.position = basePosition;
            GameObject.Find("Specialization").transform.localScale /= 2.5f;
            scrollView.horizontal = false;
            scrollView.vertical = false;
            zoomButton.GetComponent<Image>().sprite = zoomInButtonSprite;
            zoomedIn = false;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("GameplayMenu");
    }

    public void SaveSpecializations()
    {
        string specializationText = "";

        specializationText += H_1_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_1_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_1_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_2_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_2_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_2_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_3_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_3_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_3_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_3_8.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_3_16.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_3_32.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_4_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_4_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_4_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_5_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_5_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_5_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += H_5_8.GetComponent<SpecializationButtonStatus>().GetPoints();


        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_Health", specializationText);


        specializationText = "";

        specializationText += A_1_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_1_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_1_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_2_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_2_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_2_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_3_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_3_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_3_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_3_8.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_3_16.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_3_32.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_4_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_4_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_4_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_5_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_5_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_5_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += A_5_8.GetComponent<SpecializationButtonStatus>().GetPoints();

        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_AttackDamage", specializationText);


        specializationText = "";

        specializationText += S_1_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_1_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_1_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_2_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_2_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_2_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_3_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_3_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_3_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_3_8.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_3_16.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_3_32.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_4_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_4_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_4_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_5_1.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_5_2.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_5_4.GetComponent<SpecializationButtonStatus>().GetPoints();
        specializationText += S_5_8.GetComponent<SpecializationButtonStatus>().GetPoints();

        PlayerPrefs.SetString("Slot" + slotNumber + "_Specialization_SpellPower", specializationText);
    }

    public bool PathFinder(Button first, Button starter)
    {
        GameObject pathFinder = (GameObject)Instantiate(pathFinderGO);
        pathFinder.GetComponent<SpecializationPathFinder>().SetStarterButtons(first, starter);
        bool pathFound = pathFinder.GetComponent<SpecializationPathFinder>().PathFinder_Main();
        Destroy(pathFinder);
        return pathFound;
    }

    public void AddPoint()
    {
        if (selectedSpecialization != null)
            selectedSpecialization.GetComponent<SpecializationButtonStatus>().AddPoint();
    }

    public void TakePoint()
    {
        if (selectedSpecialization != null)
            selectedSpecialization.GetComponent<SpecializationButtonStatus>().TakePoint();
    }

    public void AddPointAvailable(bool available)
    {
        if (available)
            addPointButton.interactable = true;
        else
            addPointButton.interactable = false;
    }

    public void TakePointAvailable(bool available)
    {
        if (available)
            takePointButton.interactable = true;
        else
            takePointButton.interactable = false;
    }

    public void SetSelectedSpecialization(GameObject spec)
    {
        selectedSpecializationImage.transform.position = spec.transform.position;
        selectedSpecializationImage.transform.localScale = spec.transform.localScale;
        selectedSpecialization = spec;
    }

    public void SetSelectedSpecText(string str)
    {
        specializationBonus_text.text = str;
    }
}
