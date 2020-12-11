using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FairyShopMenu : MonoBehaviour {

    int slotNumber;
    int level;

    int money;
    int fairyShards;

    public Text goldText;
    public Text silverText;
    public Text bronzeText;
    public Text fairyShardText;

    public Text bronzeValue_smallBagOfMoney;
    public Text silverValue_smallBagOfMoney;
    public Text goldValue_smallBagOfMoney;
    public Image goldImage_smallBagOfMoney;

    public Text bronzeValue_mediumBagOfMoney;
    public Text silverValue_mediumBagOfMoney;
    public Text goldValue_mediumBagOfMoney;
    public Image goldImage_mediumBagOfMoney;

    public Text bronzeValue_largeBagOfMoney;
    public Text silverValue_largeBagOfMoney;
    public Text goldValue_largeBagOfMoney;
    public Image goldImage_largeBagOfMoney;

    public int smallBagOfMoneyValue;
    public int mediumBagOfMoneyValue;
    public int largeBagOfMoneyValue;

    void Start () {
        slotNumber = PlayerPrefs.GetInt("GameSlot");
        level = PlayerPrefs.GetInt("Slot" + slotNumber + "_Level");

        SetConsumablesValues();

        SetSmallBagOfMoneyValues();
        SetMediumBagOfMoneyValues();
        SetLargeBagOfMoneyValues();
    }

    public void SetConsumablesValues()
    {
        money = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold");
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

        fairyShards = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard");
        fairyShardText.text = fairyShards.ToString();

    }

    public void SetSmallBagOfMoneyValues()
    {
        smallBagOfMoneyValue = Mathf.RoundToInt(300 * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(level));
        if(smallBagOfMoneyValue >= 10000)
        {
            goldImage_smallBagOfMoney.color = new Color(1f, 1f, 1f, 1f);
            goldValue_smallBagOfMoney.text = (smallBagOfMoneyValue / 10000).ToString();
            silverValue_smallBagOfMoney.text = (smallBagOfMoneyValue % 10000 / 100).ToString();
            bronzeValue_smallBagOfMoney.text = (smallBagOfMoneyValue % 100).ToString();
        }
        else
        {
            goldImage_smallBagOfMoney.color = new Color(1f, 1f, 1f, 0f);
            goldValue_smallBagOfMoney.text = "";
            silverValue_smallBagOfMoney.text = (smallBagOfMoneyValue / 100).ToString();
            bronzeValue_smallBagOfMoney.text = (smallBagOfMoneyValue % 100).ToString();
        }
    }

    public void SetMediumBagOfMoneyValues()
    {
        mediumBagOfMoneyValue = Mathf.RoundToInt(1800 * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(level));
        if (mediumBagOfMoneyValue >= 10000)
        {
            goldImage_mediumBagOfMoney.color = new Color(1f, 1f, 1f, 1f);
            goldValue_mediumBagOfMoney.text = (mediumBagOfMoneyValue / 10000).ToString();
            silverValue_mediumBagOfMoney.text = (mediumBagOfMoneyValue % 10000 / 100).ToString();
            bronzeValue_mediumBagOfMoney.text = (mediumBagOfMoneyValue % 100).ToString();
        }
        else
        {
            goldImage_mediumBagOfMoney.color = new Color(1f, 1f, 1f, 0f);
            goldValue_mediumBagOfMoney.text = "";
            silverValue_mediumBagOfMoney.text = (mediumBagOfMoneyValue / 100).ToString();
            bronzeValue_mediumBagOfMoney.text = (mediumBagOfMoneyValue % 100).ToString();
        }
    }

    public void SetLargeBagOfMoneyValues()
    {
        largeBagOfMoneyValue = Mathf.RoundToInt(10500 * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(level));
        if (largeBagOfMoneyValue >= 10000)
        {
            goldImage_largeBagOfMoney.color = new Color(1f, 1f, 1f, 1f);
            goldValue_largeBagOfMoney.text = (largeBagOfMoneyValue / 10000).ToString();
            silverValue_largeBagOfMoney.text = (largeBagOfMoneyValue % 10000 / 100).ToString();
            bronzeValue_largeBagOfMoney.text = (largeBagOfMoneyValue % 100).ToString();
        }
        else
        {
            goldImage_largeBagOfMoney.color = new Color(1f, 1f, 1f, 0f);
            goldValue_largeBagOfMoney.text = "";
            silverValue_largeBagOfMoney.text = (largeBagOfMoneyValue / 100).ToString();
            bronzeValue_largeBagOfMoney.text = (largeBagOfMoneyValue % 100).ToString();
        }
    }

    public void BuySmallBagOfMoney()
    {
        if(fairyShards >= 20)
        {
            fairyShards -= 20;
            PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard", fairyShards);
            money += smallBagOfMoneyValue;
            PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold", money);
            SetConsumablesValues();
        }
    }

    public void BuyMediumBagOfMoney()
    {
        if (fairyShards >= 100)
        {
            fairyShards -= 100;
            PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard", fairyShards);
            money += mediumBagOfMoneyValue;
            PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold", money);
            SetConsumablesValues();
        }
    }

    public void BuyLargeBagOfMoney()
    {
        if (fairyShards >= 500)
        {
            fairyShards -= 500;
            PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard", fairyShards);
            money += largeBagOfMoneyValue;
            PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold", money);
            SetConsumablesValues();
        }
    }

    public void BuyTinyBagOfFairyShards()
    {
        fairyShards += 60;
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard", fairyShards);
        SetConsumablesValues();
    }

    public void BuySmallBagOfFairyShards()
    {
        fairyShards += 330;
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard", fairyShards);
        SetConsumablesValues();
    }

    public void BuyMediumBagOfFairyShards()
    {
        fairyShards += 720;
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard", fairyShards);
        SetConsumablesValues();
    }

    public void BuyLargeBagOfFairyShards()
    {
        fairyShards += 1560;
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard", fairyShards);
        SetConsumablesValues();
    }

    public void BuyHugeBagOfFairyShards()
    {
        fairyShards += 4200;
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard", fairyShards);
        SetConsumablesValues();
    }

    public void BuyGiantBagOfFairyShards()
    {
        fairyShards += 9000 ;
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard", fairyShards);
        SetConsumablesValues();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("GameplayMenu");
    }
}
