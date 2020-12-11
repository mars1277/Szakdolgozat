using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpecializationButtonStatus : MonoBehaviour
{

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public Button H_1_1;
    public Button A_1_1;
    public Button S_1_1;

    Image stack_1;
    Image stack_2;
    Image stack_3;

    public bool majorSpecialization;
    public bool fullMajorSpec;
    public bool lockedMajorSpecialization;

    public int stacks;
    public int lockedStacks;
    public bool available;
    public bool full;

    public int neighbourButtonNumber;
    public int neighbourActiveButtonsNumber;

    void Update()
    {
        if (neighbourActiveButtonsNumber <= 0 && (!majorSpecialization || !fullMajorSpec))
        {
            if (available)
            {
                available = false;
                GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().UpdateSpecializationPoints();
            }
        }
        else
        {
            if (!available)
            {
                available = true;
                GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().UpdateSpecializationPoints();
            }
        }

        Color color;
        if (available || fullMajorSpec)
        {
            color = gameObject.GetComponent<Image>().color;
            color.a = 1f;
            gameObject.GetComponent<Image>().color = color;
        }
        else
        {
            color = gameObject.GetComponent<Image>().color;
            color.a = 0.6f;
            gameObject.GetComponent<Image>().color = color;
        }
    }

    public void InitializeButton()
    {
        string n = name.Substring(9);
        if (n.Equals("1_1") || n.Equals("2_4") || n.Equals("3_32") || n.Equals("4_4") || n.Equals("5_8"))
            majorSpecialization = true;
        else
            majorSpecialization = false;
        if (!majorSpecialization)
        {
            stack_1 = gameObject.transform.parent.GetChild(0).GetChild(1).GetComponent<Image>();
            stack_2 = gameObject.transform.parent.GetChild(0).GetChild(2).GetComponent<Image>();
            stack_3 = gameObject.transform.parent.GetChild(0).GetChild(3).GetComponent<Image>();
        }

        stacks = 0;
        available = false;
        full = false;
        fullMajorSpec = false;
        lockedMajorSpecialization = false;
        neighbourActiveButtonsNumber = 0;
        neighbourButtonNumber = 0;
        if (button1 != null)
            neighbourButtonNumber++;
        if (button2 != null)
            neighbourButtonNumber++;
        if (button3 != null)
            neighbourButtonNumber++;
        if (button4 != null)
            neighbourButtonNumber++;
        if (!majorSpecialization)
            SetStackImages();
    }

    public int GetPoints()
    {
        if (majorSpecialization && fullMajorSpec)
            return 1;
        return stacks;
    }

    public int GetSpentPoints()
    {
        if (majorSpecialization && fullMajorSpec && !lockedMajorSpecialization)
            return 1;
        return stacks - lockedStacks;
    }

    public void SetLockedStacks(int num)
    {
        if (!majorSpecialization)
        {
            lockedStacks = num;
            stacks = num;
            if (lockedStacks == 3)
                full = true;
            switch (lockedStacks)
            {
                case 0:
                    stack_1.color = new Color(1f, 1f, 1f);
                    stack_2.color = new Color(1f, 1f, 1f);
                    stack_3.color = new Color(1f, 1f, 1f);
                    break;
                case 1:
                    stack_1.color = new Color(1f, 0f, 1f);
                    stack_2.color = new Color(1f, 1f, 1f);
                    stack_3.color = new Color(1f, 1f, 1f);
                    break;
                case 2:
                    stack_1.color = new Color(1f, 0f, 1f);
                    stack_2.color = new Color(1f, 0f, 1f);
                    stack_3.color = new Color(1f, 1f, 1f);
                    break;
                case 3:
                    stack_1.color = new Color(1f, 0f, 1f);
                    stack_2.color = new Color(1f, 0f, 1f);
                    stack_3.color = new Color(1f, 0f, 1f);
                    break;
                default:
                    stack_1.color = new Color(1f, 1f, 1f);
                    stack_2.color = new Color(1f, 1f, 1f);
                    stack_3.color = new Color(1f, 1f, 1f);
                    break;
            }
            SetStackImages();
        }
    }

    public void SetAvailable(bool a)
    {
        available = a;
    }


    public bool IsFull()
    {
        return full || fullMajorSpec;
    }

    public bool HasStacks()
    {
        if ((stacks > 0) || fullMajorSpec)
            return true;
        else return false;
    }

    public bool HasAvailableStacks()
    {
        if ((stacks > lockedStacks) || (fullMajorSpec && !lockedMajorSpecialization))
            return true;
        else return false;
    }

    public void SetMajorSpecializationFull()
    {
        fullMajorSpec = true;
    }

    public void SetLockedMajorSpecialization()
    {
        lockedMajorSpecialization = true;
    }

    public void IncFullNeighbourNumber(int num = 1)
    {
        neighbourActiveButtonsNumber += num;
    }

    public void DecFullNeighbourNumber(int num = 1)
    {
        neighbourActiveButtonsNumber -= num;
    }
    void SetStackImages()
    {
        Color color;
        switch (stacks)
        {
            case 0:
                color = stack_1.color;
                color.a = 0f;
                stack_1.color = color;

                color = stack_2.color;
                color.a = 0f;
                stack_2.color = color;

                color = stack_3.color;
                color.a = 0f;
                stack_3.color = color;
                break;
            case 1:
                color = stack_1.color;
                color.a = 1f;
                stack_1.color = color;

                color = stack_2.color;
                color.a = 0f;
                stack_2.color = color;

                color = stack_3.color;
                color.a = 0f;
                stack_3.color = color;
                break;
            case 2:
                color = stack_1.color;
                color.a = 1f;
                stack_1.color = color;

                color = stack_2.color;
                color.a = 1f;
                stack_2.color = color;

                color = stack_3.color;
                color.a = 0f;
                stack_3.color = color;
                break;
            case 3:
                color = stack_1.color;
                color.a = 1f;
                stack_1.color = color;

                color = stack_2.color;
                color.a = 1f;
                stack_2.color = color;

                color = stack_3.color;
                color.a = 1f;
                stack_3.color = color;
                break;
            default:
                color = stack_1.color;
                color.a = 0f;
                stack_1.color = color;

                color = stack_2.color;
                color.a = 0f;
                stack_2.color = color;

                color = stack_3.color;
                color.a = 0f;
                stack_3.color = color;
                break;
        }
    }

    public int GetActiveNeighbourButtonNumber()
    {
        return neighbourActiveButtonsNumber;
    }

    public int GetNeighbourButtonNumber()
    {
        return neighbourButtonNumber;
    }

    public bool HasMoreActiveNeighbours()
    {
        return ((HasStacks() && (GetActiveNeighbourButtonNumber() > 1)) || (!HasStacks()));
    }

    void IncNeighboursActiveNumber()
    {
        switch (neighbourButtonNumber)
        {
            case 0:
                return;
            case 1:
                button1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                return;
            case 2:
                button1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                button2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                return;
            case 3:
                button1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                button2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                button3.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                return;
            case 4:
                button1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                button2.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                button3.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                button4.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber();
                return;
            default: return;
        }
    }

    public void Button_OnClick()
    {
        string name = gameObject.name;

        name = name.Substring(7);

        GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().SetSelectedSpecText(GameObject.Find("Datas").GetComponent<SpecializationDatas>().GetSpecBonuses(name));
        GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().SetSelectedSpecialization(gameObject);

        if (!available || full || fullMajorSpec || !GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().HasAvailableSpecializationPoints())
            GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().AddPointAvailable(false);
        else
            GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().AddPointAvailable(true);

        if (available && CheckRequirementsForTakingPoint())
            GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().TakePointAvailable(true);
        else
            GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().TakePointAvailable(false);
    }


    public void AddPoint()
    {
        if (GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().HasAvailableSpecializationPoints())
        {
            if (!majorSpecialization)
            {
                stacks++;
                if (stacks == 3)
                {
                    full = true;
                    IncNeighboursActiveNumber();
                }
                GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().UpdateSpecializationPoints();
                SetStackImages();
            }
            else
            {
                fullMajorSpec = true;
                IncNeighboursActiveNumber();
                string n = name.Substring(9);
                if (n.Equals("1_1"))
                {
                    DecFullNeighbourNumber();
                    string buttonName = name.Substring(7);
                    if (!buttonName.Equals("H_1_1"))
                    {
                        H_1_1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber(100);
                    }
                    if (!buttonName.Equals("A_1_1"))
                    {
                        A_1_1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber(100);
                    }
                    if (!buttonName.Equals("S_1_1"))
                    {
                        S_1_1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber(100);
                    }
                }
                GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().UpdateSpecializationPoints();
            }
            Button_OnClick();
        }
    }

    public void TakePoint()
    {
        if (!majorSpecialization)
        {
            if (stacks == 3)
            {
                DecNeighboursActiveNumber();
            }
            stacks--;
            full = false;
            GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().UpdateSpecializationPoints();
            SetStackImages();
        }
        else
        {
            fullMajorSpec = false;
            DecNeighboursActiveNumber();
            string n = name.Substring(9);
            if (n.Equals("1_1"))
            {
                IncFullNeighbourNumber();
                string buttonName = name.Substring(7);
                if (!buttonName.Equals("H_1_1"))
                {
                    H_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber(100);
                }
                if (!buttonName.Equals("A_1_1"))
                {
                    A_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber(100);
                }
                if (!buttonName.Equals("S_1_1"))
                {
                    S_1_1.GetComponent<SpecializationButtonStatus>().IncFullNeighbourNumber(100);
                }
            }
            GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().UpdateSpecializationPoints();
        }
        Button_OnClick();
    }

    public bool CheckRequirementsForTakingPoint()
    {
        bool pathFound = true;

        switch (neighbourButtonNumber)
        {
            case 1:
                if (button1.GetComponent<SpecializationButtonStatus>().HasStacks())
                    pathFound = GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().PathFinder(button1, gameObject.GetComponent<Button>());
                break;
            case 2:
                if (button1.GetComponent<SpecializationButtonStatus>().HasStacks())
                    pathFound = GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().PathFinder(button1, gameObject.GetComponent<Button>());
                if (button2.GetComponent<SpecializationButtonStatus>().HasStacks())
                    pathFound = pathFound && GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().PathFinder(button2, gameObject.GetComponent<Button>());
                break;
            case 3:
                if (button1.GetComponent<SpecializationButtonStatus>().HasStacks())
                    pathFound = GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().PathFinder(button1, gameObject.GetComponent<Button>());
                if (button2.GetComponent<SpecializationButtonStatus>().HasStacks())
                    pathFound = pathFound && GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().PathFinder(button2, gameObject.GetComponent<Button>());
                if (button3.GetComponent<SpecializationButtonStatus>().HasStacks())
                    pathFound = pathFound && GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().PathFinder(button3, gameObject.GetComponent<Button>());
                break;
            case 4:
                if (button1.GetComponent<SpecializationButtonStatus>().HasStacks())
                    pathFound = GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().PathFinder(button1, gameObject.GetComponent<Button>());
                if (button2.GetComponent<SpecializationButtonStatus>().HasStacks())
                    pathFound = pathFound && GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().PathFinder(button2, gameObject.GetComponent<Button>());
                if (button3.GetComponent<SpecializationButtonStatus>().HasStacks())
                    pathFound = pathFound && GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().PathFinder(button3, gameObject.GetComponent<Button>());
                if (button4.GetComponent<SpecializationButtonStatus>().HasStacks())
                    pathFound = pathFound && GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().PathFinder(button4, gameObject.GetComponent<Button>());
                break;
            default:
                break;
        }
        return HasAvailableStacks() && pathFound;
    }

    void DecNeighboursActiveNumber()
    {
        switch (neighbourButtonNumber)
        {
            case 0:
                return;
            case 1:
                button1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber();
                return;
            case 2:
                button1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber();
                button2.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber();
                return;
            case 3:
                button1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber();
                button2.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber();
                button3.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber();
                return;
            case 4:
                button1.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber();
                button2.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber();
                button3.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber();
                button4.GetComponent<SpecializationButtonStatus>().DecFullNeighbourNumber();
                return;
            default: return;
        }
    }
}
