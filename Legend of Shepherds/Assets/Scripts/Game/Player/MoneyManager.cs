using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

    int money;

    public Text goldText;
    public Text silverText;
    public Text bronzeText;

    public Text wallHealthPercent;

    void Start () {
        wallHealthPercent.text = (!GameObject.Find("Player").GetComponent<Player_Health>().failed ? Mathf.FloorToInt(GameObject.Find("Wall").GetComponent<Wall_Health>().GetHealhtPercentage() * 100).ToString() : "0") + "%";
        money = Mathf.RoundToInt(GameObject.Find("Player").GetComponent<Player_Health>().GetGold() * (0.5f + GameObject.Find("Wall").GetComponent<Wall_Health>().GetHealhtPercentage() / 2f));
        SetMoneyValues();
	}
	
    public void SetMoneyValues()
    {
        if (money >= 10000)
        {
            goldText.text = (money / 10000).ToString();
            silverText.text = (money % 10000 / 100).ToString();
            bronzeText.text = (money % 100).ToString();
        }
        else if (money >= 100)
        {
            goldText.text = "0";
            silverText.text = (money / 100).ToString();
            bronzeText.text = (money % 100).ToString();
        }
        else
        {
            goldText.text = "0";
            silverText.text = "0";
            bronzeText.text = money.ToString();
        }
    }
}
