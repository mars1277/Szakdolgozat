using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AssumableController : MonoBehaviour {

    public GameObject smallBronze;
    public GameObject mediumBronze;
    public GameObject bigBronze;
    public GameObject largeBronze;

    public GameObject smallSilver;
    public GameObject mediumSilver;
    public GameObject bigSilver;
    public GameObject largeSilver;

    public GameObject smallGold;
    public GameObject mediumGold;
    public GameObject bigGold;
    public GameObject largeGold;

    int playerLevel;

    void Start()
    {
        playerLevel = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Level");
    }

    public void MakeGold(Vector3 vec)
    {
        System.Random r = new System.Random();
        int amount = r.Next(1, 4);
        GameObject gold;
        switch (amount)
        {
            case 1:
                gold = Instantiate(smallGold);
                amount = r.Next(4, 11) * playerLevel;
                gold.GetComponent<Gold>().SetGold(amount);
                gold.transform.position = vec;
                break;
            case 2:
                gold = Instantiate(mediumGold);
                amount = r.Next(11, 21) * playerLevel;
                gold.GetComponent<Gold>().SetGold(amount);
                gold.transform.position = vec;
                break;
            case 3:
                gold = Instantiate(bigGold);
                amount = r.Next(21, 31) * playerLevel;
                gold.GetComponent<Gold>().SetGold(amount);
                gold.transform.position = vec;
                break;
            default:
                break;
        }
    }
    public void MakeGold(Vector3 vec, int amount)
    {
        GameObject money;
        if (amount < 25)
        {
            money = Instantiate(smallBronze);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
        else if (amount < 50)
        {
            money = Instantiate(mediumBronze);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
        else if (amount < 75)
        {
            money = Instantiate(bigBronze);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
        else if (amount < 100)
        {
            money = Instantiate(largeBronze);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
        else if (amount < 2500)
        {
            money = Instantiate(smallSilver);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
        else if (amount < 5000)
        {
            money = Instantiate(mediumSilver);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
        else if (amount < 7500)
        {
            money = Instantiate(bigSilver);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
        else if (amount < 10000)
        {
            money = Instantiate(largeSilver);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
        else if (amount < 250000)
        {
            money = Instantiate(smallGold);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
        else if (amount < 500000)
        {
            money = Instantiate(mediumGold);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
        else if (amount < 750000)
        {
            money = Instantiate(bigGold);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
        else
        {
            money = Instantiate(largeGold);
            money.GetComponent<Gold>().SetGold(amount);
            money.transform.position = vec;
        }
    }
}
