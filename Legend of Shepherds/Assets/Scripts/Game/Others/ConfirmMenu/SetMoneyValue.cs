using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMoneyValue : MonoBehaviour {

    public Text goldText;
    public Image goldImage;
    public Text silverText;
    public Image silverImage;
    public Text bronzeText;

    public void SetValue(int money)
    {
        SetMoneyValues(money);
    }

    public void SetMoneyValues(int money)
    {
        if (money >= 10000)
        {
            goldText.text = (money / 10000).ToString();
            silverText.text = (money % 10000 / 100).ToString();
            bronzeText.text = (money % 100).ToString();
            goldImage.color = new Color(1f, 1f, 1f, 1f);
            silverImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (money >= 100)
        {
            goldText.text = "";
            goldImage.color = new Color(1f, 1f, 1f, 0f);
            silverText.text = (money / 100).ToString();
            bronzeText.text = (money % 100).ToString();
            silverImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            goldText.text = "";
            silverText.text = "";
            goldImage.color = new Color(1f, 1f, 1f, 0f);
            silverImage.color = new Color(1f, 1f, 1f, 0f);
            bronzeText.text = money.ToString();
        }
    }
}
