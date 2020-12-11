using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class SpecializationPathFinder : MonoBehaviour {

    Button first;
    Button starter;

    Dictionary<string, int> buttons = new Dictionary<string, int>();

    public void SetStarterButtons(Button f, Button s)
    {
        first = f;
        starter = s;
    }


    public bool PathFinder_Main()
    {
        buttons.Add(starter.name.Substring(7), 1);
        buttons.Add(first.name.Substring(7), 1);
        PathFinder_Recursive(first);

        if (starter.name.Substring(7).Equals("H_1_1"))
            buttons.Remove("H_1_1");
        if (starter.name.Substring(7).Equals("A_1_1"))
            buttons.Remove("A_1_1");
        if (starter.name.Substring(7).Equals("S_1_1"))
            buttons.Remove("S_1_1");

        return buttons.ContainsKey("H_1_1") || buttons.ContainsKey("A_1_1") || buttons.ContainsKey("S_1_1");
    }

    void PathFinder_Recursive(Button button)
    {
        int neighbourButtonNumber = button.GetComponent<SpecializationButtonStatus>().GetNeighbourButtonNumber();

        switch (neighbourButtonNumber)
        {
            case 1:
                if(button.GetComponent<SpecializationButtonStatus>().button1.GetComponent<SpecializationButtonStatus>().IsFull() && !buttons.ContainsKey(button.GetComponent<SpecializationButtonStatus>().button1.name.Substring(7)))
                {
                    buttons.Add(button.GetComponent<SpecializationButtonStatus>().button1.name.Substring(7), 1);
                    PathFinder_Recursive(button.GetComponent<SpecializationButtonStatus>().button1);
                }
                break;
            case 2:
                if (button.GetComponent<SpecializationButtonStatus>().button1.GetComponent<SpecializationButtonStatus>().IsFull() && !buttons.ContainsKey(button.GetComponent<SpecializationButtonStatus>().button1.name.Substring(7)))
                {
                    buttons.Add(button.GetComponent<SpecializationButtonStatus>().button1.name.Substring(7), 1);
                    PathFinder_Recursive(button.GetComponent<SpecializationButtonStatus>().button1);
                }
                if (button.GetComponent<SpecializationButtonStatus>().button2.GetComponent<SpecializationButtonStatus>().IsFull() && !buttons.ContainsKey(button.GetComponent<SpecializationButtonStatus>().button2.name.Substring(7)))
                {
                    buttons.Add(button.GetComponent<SpecializationButtonStatus>().button2.name.Substring(7), 1);
                    PathFinder_Recursive(button.GetComponent<SpecializationButtonStatus>().button2);
                }
                break;
            case 3:
                if (button.GetComponent<SpecializationButtonStatus>().button1.GetComponent<SpecializationButtonStatus>().IsFull() && !buttons.ContainsKey(button.GetComponent<SpecializationButtonStatus>().button1.name.Substring(7)))
                {
                    buttons.Add(button.GetComponent<SpecializationButtonStatus>().button1.name.Substring(7), 1);
                    PathFinder_Recursive(button.GetComponent<SpecializationButtonStatus>().button1);
                }
                if (button.GetComponent<SpecializationButtonStatus>().button2.GetComponent<SpecializationButtonStatus>().IsFull() && !buttons.ContainsKey(button.GetComponent<SpecializationButtonStatus>().button2.name.Substring(7)))
                {
                    buttons.Add(button.GetComponent<SpecializationButtonStatus>().button2.name.Substring(7), 1);
                    PathFinder_Recursive(button.GetComponent<SpecializationButtonStatus>().button2);
                }
                if (button.GetComponent<SpecializationButtonStatus>().button3.GetComponent<SpecializationButtonStatus>().IsFull() && !buttons.ContainsKey(button.GetComponent<SpecializationButtonStatus>().button3.name.Substring(7)))
                {
                    buttons.Add(button.GetComponent<SpecializationButtonStatus>().button3.name.Substring(7), 1);
                    PathFinder_Recursive(button.GetComponent<SpecializationButtonStatus>().button3);
                }
                break;
            case 4:
                if (button.GetComponent<SpecializationButtonStatus>().button1.GetComponent<SpecializationButtonStatus>().IsFull() && !buttons.ContainsKey(button.GetComponent<SpecializationButtonStatus>().button1.name.Substring(7)))
                {
                    buttons.Add(button.GetComponent<SpecializationButtonStatus>().button1.name.Substring(7), 1);
                    PathFinder_Recursive(button.GetComponent<SpecializationButtonStatus>().button1);
                }
                if (button.GetComponent<SpecializationButtonStatus>().button2.GetComponent<SpecializationButtonStatus>().IsFull() && !buttons.ContainsKey(button.GetComponent<SpecializationButtonStatus>().button2.name.Substring(7)))
                {
                    buttons.Add(button.GetComponent<SpecializationButtonStatus>().button2.name.Substring(7), 1);
                    PathFinder_Recursive(button.GetComponent<SpecializationButtonStatus>().button2);
                }
                if (button.GetComponent<SpecializationButtonStatus>().button3.GetComponent<SpecializationButtonStatus>().IsFull() && !buttons.ContainsKey(button.GetComponent<SpecializationButtonStatus>().button3.name.Substring(7)))
                {
                    buttons.Add(button.GetComponent<SpecializationButtonStatus>().button3.name.Substring(7), 1);
                    PathFinder_Recursive(button.GetComponent<SpecializationButtonStatus>().button3);
                }
                if (button.GetComponent<SpecializationButtonStatus>().button4.GetComponent<SpecializationButtonStatus>().IsFull() && !buttons.ContainsKey(button.GetComponent<SpecializationButtonStatus>().button4.name.Substring(7)))
                {
                    buttons.Add(button.GetComponent<SpecializationButtonStatus>().button4.name.Substring(7), 1);
                    PathFinder_Recursive(button.GetComponent<SpecializationButtonStatus>().button4);
                }
                break;
            default:
                break;
        }
    }
}
