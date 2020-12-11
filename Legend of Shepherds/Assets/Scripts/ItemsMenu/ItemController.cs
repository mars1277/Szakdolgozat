using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;

    [Serializable]
    class SerializationItems
    {
        public SerializationItems() { }

        public Item[] equippedItemsSer;
        public Item[] storedItemsSer;
        public Item[] shopItemsSer;
    }

public class ItemController : MonoBehaviour
{
    public Item[] equippedItems;
    public Item[] storedItems;
    public Item[] shopItems;

    public GameObject[] equippedItemGOs;
    public GameObject[] storedItemGOs;
    public GameObject[] shopItemGOs;

    string mode;

    public string[] equippedItemTypes;

    public GameObject itemGO;

    public GameObject coveredSlot;
    string selectedItemPlace;
    int selectedItemIndex;

    public static RaycastHit2D[] hits;

    public Button equipmentButton;
    public Button shopButton;
    public GameObject equipmentSlotsGO;
    public GameObject shopSlotsGO;

    public Text goldText;
    public Image goldImage;
    public Text silverText;
    public Image silverImage;
    public Text bronzeText;

    public Button buy_sell_equip_unequipButton;
    public Button upgrage_Button;

    public Sprite buyButtonSprite;
    public Sprite sellButtonSprite;
    public Sprite equipButtonSprite;
    public Sprite unequipButtonSprite;

    public bool pressedYes;
    public bool waitingForPress;
    public GameObject confirmGO;
    public int confirmResult;

    public Text selectedItemText;
    public Image[] selectedItemStarsBG;
    public Image[] selectedItemStars;
    public Text equippedItemText;
    public Image[] equippedItemStarsBG;
    public Image[] equippedItemStars;

    public Text refreshShopCostText;
    public Text fairyShardText;
    public Text costText;
    public Text costGoldText;
    public Image costGoldImage;
    public Text costSilverText;
    public Image costSilverImage;
    public Text costBronzeText;
    public Image costBronzeImage;

    public Text allItemStatusText;

    public Text shopRefreshClock;
    DateTime endOfTheDay;
    DateTime now;
    TimeSpan remainingTime;
    public Button refreshShopItemsButton;

    void Start()
    {
        equippedItems = new Item[8];
        storedItems = new Item[28];
        shopItems = new Item[9];

        ChooseEquipment();

        equippedItemGOs = new GameObject[8];
        storedItemGOs = new GameObject[28];
        shopItemGOs = new GameObject[9];

        equippedItemTypes = new string[8];
        InicializeEquippedItemTypes();
        LoadItems();

        confirmResult = -1;

        SetFairyShards(PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard"));
        SetMoneyValues(PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold"));

        selectedItemPlace = "nothing";
        selectedItemIndex = -1;
        upgrage_Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        buy_sell_equip_unequipButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        SetCostValues(-1);

        waitingForPress = true;

        endOfTheDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
        now = DateTime.Now;
        remainingTime = endOfTheDay - now;
        shopRefreshClock.text = remainingTime.ToString().Substring(0, 8);

        DateTime today = DateTime.Today;
        string todayStr = today.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));

        if (!PlayerPrefs.GetString("Slot" + PlayerPrefs.GetInt("GameSlot") + "_ItemShopRefreshedDate").Equals(todayStr))
        {
            RefreshShopItems();
            PlayerPrefs.SetString("Slot" + PlayerPrefs.GetInt("GameSlot") + "_ItemShopRefreshedDate", todayStr);
            PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_ItemShopManuallyRefreshed", 0);
        }
        refreshShopCostText.text = (5 * (int)Mathf.Pow(2, PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_ItemShopManuallyRefreshed"))).ToString();
    }

    void SetAllItemStatus()
    {
        float allHP = 0;
        float allAD = 0;
        float allMP = 0;
        for (int i = 0; i < 8; i++)
        {
            if ((equippedItems[i] != null) && !equippedItems[i].GetRarity().Equals("non"))
            {
                float[] attributes = equippedItems[i].GetAttributes();
                allHP += attributes[0];
                allAD += attributes[1];
                allMP += attributes[2];
            }
        }
        float dps = ((Weapon)equippedItems[2]).GetAttackDamage() / ((Weapon)equippedItems[2]).GetAttackSpeed() + allAD / 10f;
        string allItemStatusStr = "DPS:\n      " + dps.ToString("0.0") + "\n";
        allItemStatusStr += "Health:\n      " + (GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues((PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Level"))) * 120f + allHP) + "\n";
        if (((Weapon)equippedItems[2]).GetWeaponType().Equals("Staff"))
            allItemStatusStr += "Magic Power:\n      " + Mathf.RoundToInt(allMP * 1.15f);
        else
            allItemStatusStr += "Magic Power:\n      " + allMP;
        allItemStatusText.text = allItemStatusStr;
    }

    void Update()
    {
        endOfTheDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0).AddDays(1);
        now = DateTime.Now;
        remainingTime = endOfTheDay - now;
        shopRefreshClock.text = remainingTime.ToString().Substring(0, 8);
        if (remainingTime.ToString().Substring(0, 8).Equals("23:59:59")){
            DateTime today = DateTime.Today;
            string todayStr = today.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));
            RefreshShopItems();
            PlayerPrefs.SetString("Slot" + PlayerPrefs.GetInt("GameSlot") + "_ItemShopRefreshedDate", todayStr);
            PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_ItemShopManuallyRefreshed", 0);
        }

        SetFairyShards(PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard"));
        SetMoneyValues(PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold"));
        hits = Physics2D.RaycastAll(Input.mousePosition, Vector2.zero);

        bool foundGO = false;
        foreach (RaycastHit2D r in hits)
        {
            if (r.collider.name.Substring(0, 4).Equals("Shop") || r.collider.name.Substring(0, 6).Equals("Stored") || r.collider.name.Substring(0, 8).Equals("Equipped") )
            {
                coveredSlot = r.collider.gameObject;
                foundGO = true;
            }
        }
        if (!foundGO)
        {
            coveredSlot = null;
        }
    }

    public void RefreshShop()
    {
        int fairyShards = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard");
        int numberOfRefreshes = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_ItemShopManuallyRefreshed");
        int cost = 5 * (int)Mathf.Pow(2, numberOfRefreshes);
        if (fairyShards >= cost)
        {
            fairyShards -= cost;
            PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_FairyShard", fairyShards);
            PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_ItemShopManuallyRefreshed", numberOfRefreshes + 1);
            refreshShopCostText.text = cost.ToString();
            RefreshShopItems();
        }
    }

    public void SetFairyShards(int fairyShard)
    {
        fairyShardText.text = fairyShard.ToString();
    }

    public void SetMoneyValues(int gold)
    {
        if (gold >= 10000)
        {
            goldText.text = (gold / 10000).ToString();
            silverText.text = (gold % 10000 / 100).ToString();
            bronzeText.text = (gold % 100).ToString();
            goldImage.color = new Color(1f, 1f, 1f, 1f);
            silverImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (gold >= 100)
        {
            goldText.text = "";
            goldImage.color = new Color(1f, 1f, 1f, 0f);
            silverText.text = (gold / 100).ToString();
            bronzeText.text = (gold % 100).ToString();
            silverImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            goldText.text = "";
            silverText.text = "";
            goldImage.color = new Color(1f, 1f, 1f, 0f);
            silverImage.color = new Color(1f, 1f, 1f, 0f);
            bronzeText.text = gold.ToString();
        }
    }

    public void HitRaycast()
    {
        hits = Physics2D.RaycastAll(Input.mousePosition, Vector2.zero);

        bool foundGO = false;
        foreach (RaycastHit2D r in hits)
        {
            if (r.collider.name.Substring(0, 4).Equals("Shop") || r.collider.name.Substring(0, 6).Equals("Stored") || r.collider.name.Substring(0, 8).Equals("Equipped"))
            {
                coveredSlot = r.collider.gameObject;
                foundGO = true;
            }
        }
        if (!foundGO)
        {
            coveredSlot = null;
        }
    }

    public void BuySellEquipUnequip()
    {
        if (mode.Equals("equipment"))
        {
            if (selectedItemPlace.Equals("stored"))
            {
                int index = GetTypeFromValue(storedItems[selectedItemIndex].GetItemType());
                EquipItem(selectedItemIndex + 1, index + 1);
            }
            else
            if (selectedItemPlace.Equals("equipped"))
            {
                if(!equippedItems[selectedItemIndex].GetItemType().Equals("Weapon"))
                    if(GetFirstEmptyStoredItemSpace() > 0)
                    {
                        UnequipItem(selectedItemIndex + 1, GetFirstEmptyStoredItemSpace());
                    }
            }
        }
        else if (mode.Equals("shop"))
        {
            if (selectedItemPlace.Equals("stored"))
            {
                SellItem(selectedItemIndex + 1);
            }
            else if (selectedItemPlace.Equals("shop"))
            {
                if (shopItems[selectedItemIndex].GetPrice() <= PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold"))
                    if (GetFirstEmptyStoredItemSpace() > 0)
                    {
                        BuyItem(selectedItemIndex + 1, GetFirstEmptyStoredItemSpace());
                    }
            }
        }
        SetItemSelected(null);
        SaveItems();
    }

    public void Upgrade()
    {
        int g = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold");
        switch (selectedItemPlace)
        {
            case "stored":
                if (mode.Equals("equipment"))
                {           
                    if ((storedItems[selectedItemIndex].GetStar() < 5) && (g >= storedItems[selectedItemIndex].GetUpgradePrice()))
                    {
                        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold", g - storedItems[selectedItemIndex].GetUpgradePrice());
                        storedItems[selectedItemIndex].Upgrade();
                        selectedItemText.text = storedItems[selectedItemIndex].GetDescription();
                        RefreshStarsOnSelectedItem(storedItems[selectedItemIndex].GetStar());
                        if (storedItems[selectedItemIndex].GetStar() < 5)
                        {
                            upgrage_Button.interactable = true;
                            SetCostValues(storedItems[selectedItemIndex].GetUpgradePrice());
                            costText.text = "Upgrade cost: ";
                        }
                        else
                        {
                            upgrage_Button.interactable = false;
                            SetCostValues(-1);
                            costText.text = "Max upgrade";
                        }
                        SaveItems();
                    }
                }
                break;
            case "equipped":

                if ((equippedItems[selectedItemIndex].GetStar() < 5) && (g >= equippedItems[selectedItemIndex].GetUpgradePrice()))
                {
                    PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold", g - equippedItems[selectedItemIndex].GetUpgradePrice());
                    equippedItems[selectedItemIndex].Upgrade();
                    selectedItemText.text = equippedItems[selectedItemIndex].GetDescription();
                    RefreshStarsOnSelectedItem(equippedItems[selectedItemIndex].GetStar());
                   if (equippedItems[selectedItemIndex].GetStar() < 5)
                    {
                        upgrage_Button.interactable = true;
                        SetCostValues(equippedItems[selectedItemIndex].GetUpgradePrice());
                        costText.text = "Upgrade cost: ";
                    }
                    else
                    {
                        upgrage_Button.interactable = false;
                        SetCostValues(-1);
                        costText.text = "Max upgrade";
                    }
                    SaveItems();
                }
                break;
            default:
                break;
        }
    }

    public void RefreshStarsOnSelectedItem(int starCount)
    {
        if (starCount < 0)
        {
            for (int i = 0; i < 5; i++)
            {
                selectedItemStarsBG[i].color = new Color(selectedItemStarsBG[i].color.r, selectedItemStarsBG[i].color.g, selectedItemStarsBG[i].color.b, 0f);
                selectedItemStars[i].color = new Color(selectedItemStars[i].color.r, selectedItemStars[i].color.g, selectedItemStars[i].color.b, 0f);
            }
        }
        else
        {
            for (int i = 0; i < starCount; i++)
            {
                selectedItemStarsBG[i].color = new Color(selectedItemStarsBG[i].color.r, selectedItemStarsBG[i].color.g, selectedItemStarsBG[i].color.b, 1f);
                selectedItemStars[i].color = new Color(selectedItemStars[i].color.r, selectedItemStars[i].color.g, selectedItemStars[i].color.b, 0f);
            }
            for (int i = starCount; i < 5; i++)
            {
                selectedItemStarsBG[i].color = new Color(selectedItemStarsBG[i].color.r, selectedItemStarsBG[i].color.g, selectedItemStarsBG[i].color.b, 1f);
                selectedItemStars[i].color = new Color(selectedItemStars[i].color.r, selectedItemStars[i].color.g, selectedItemStars[i].color.b, 1f);
            }
        }
    }

    public void RefreshStarsOnEquippedItem(int starCount)
    {
        if (starCount < 0)
        {
            for(int i = 0; i < 5; i++)
            {
                equippedItemStarsBG[i].color = new Color(equippedItemStarsBG[i].color.r, equippedItemStarsBG[i].color.g, equippedItemStarsBG[i].color.b, 0f);
                equippedItemStars[i].color = new Color(equippedItemStars[i].color.r, equippedItemStars[i].color.g, equippedItemStars[i].color.b, 0f);
            }
        }
        else
        {
            for (int i = 0; i < starCount; i++)
            {
                equippedItemStarsBG[i].color = new Color(equippedItemStarsBG[i].color.r, equippedItemStarsBG[i].color.g, equippedItemStarsBG[i].color.b, 1f);
                equippedItemStars[i].color = new Color(equippedItemStars[i].color.r, equippedItemStars[i].color.g, equippedItemStars[i].color.b, 0f);
            }
            for (int i = starCount; i < 5; i++)
            {
                equippedItemStarsBG[i].color = new Color(equippedItemStarsBG[i].color.r, equippedItemStarsBG[i].color.g, equippedItemStarsBG[i].color.b, 1f);
                equippedItemStars[i].color = new Color(equippedItemStars[i].color.r, equippedItemStars[i].color.g, equippedItemStars[i].color.b, 1f);
            }
        }
    }

    public void SetCostValues(int gold)
    {
        if (gold < 0)
        {
            costText.text = "";
            costGoldText.text = "";
            costSilverText.text = "";
            costBronzeText.text = "";
            costGoldImage.color = new Color(1f, 1f, 1f, 0f);
            costSilverImage.color = new Color(1f, 1f, 1f, 0f);
            costBronzeImage.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            if(gold >= 10000)
            {
                costGoldText.text = (gold / 10000).ToString();
                costSilverText.text = (gold % 10000 / 100).ToString();
                costBronzeText.text = (gold % 100).ToString();
                costGoldImage.color = new Color(1f, 1f, 1f, 1f);
                costSilverImage.color = new Color(1f, 1f, 1f, 1f);
                costBronzeImage.color = new Color(1f, 1f, 1f, 1f);
            }
            else if(gold >= 100)
            {
                costGoldText.text = "";
                costGoldImage.color = new Color(1f, 1f, 1f, 0f);
                costSilverText.text = (gold / 100).ToString();
                costBronzeText.text = (gold % 100).ToString();
                costSilverImage.color = new Color(1f, 1f, 1f, 1f);
                costBronzeImage.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                costGoldText.text = "";
                costSilverText.text = "";
                costGoldImage.color = new Color(1f, 1f, 1f, 0f);
                costSilverImage.color = new Color(1f, 1f, 1f, 0f);
                costBronzeText.text = gold.ToString();
                costBronzeImage.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public void CreateConfirmMenu(string text, string type, int value)
    {
        GameObject confirmMenu = Instantiate(confirmGO);
        confirmMenu = confirmMenu.transform.GetChild(0).gameObject;
        confirmMenu.GetComponent<ConfirmMenu>().SetText(text);
        confirmMenu.GetComponent<ConfirmMenu>().SetOptionalValue(type, value);
    }

    public void ConfirmDone(int result)
    {
        confirmResult = result;
    }

    public void SetItemSelected(GameObject slot)
    {
        if (slot == null)
        {
            selectedItemPlace = "nothing";
            selectedItemIndex = -1;
            upgrage_Button.interactable = false;
            upgrage_Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            buy_sell_equip_unequipButton.interactable = false;
            buy_sell_equip_unequipButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            selectedItemText.text = "";
            equippedItemText.text = "";
            costText.text = "";
            SetCostValues(-1);
            RefreshStarsOnEquippedItem(-1);
            RefreshStarsOnSelectedItem(-1);
        }
        else if (slot.name.Substring(0, 4).Equals("Shop"))
        {
            selectedItemPlace = "shop";
            selectedItemIndex = int.Parse(slot.name.Substring(9, 2)) - 1;
            buy_sell_equip_unequipButton.GetComponent<Image>().sprite = buyButtonSprite;
            upgrage_Button.interactable = false;
            upgrage_Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            if (shopItems[selectedItemIndex].GetPrice() > PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold"))
            {
                buy_sell_equip_unequipButton.interactable = false;
            } else
            {
                buy_sell_equip_unequipButton.interactable = true;
            }
            buy_sell_equip_unequipButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            selectedItemText.text = shopItems[selectedItemIndex].GetDescription();
            if (equippedItems[GetTypeFromValue(shopItems[selectedItemIndex].GetItemType())] != null && !equippedItems[GetTypeFromValue(shopItems[selectedItemIndex].GetItemType())].GetRarity().Equals("non"))
            {
                equippedItemText.text = equippedItems[GetTypeFromValue(shopItems[selectedItemIndex].GetItemType())].GetDescription();
                RefreshStarsOnEquippedItem(equippedItems[GetTypeFromValue(shopItems[selectedItemIndex].GetItemType())].GetStar());
            }
            else
            {
                equippedItemText.text = "";
                RefreshStarsOnEquippedItem(-1);
            }
            costText.text = "Buy cost: ";
            SetCostValues(shopItems[selectedItemIndex].GetPrice());
            RefreshStarsOnSelectedItem(shopItems[selectedItemIndex].GetStar());
        }
        else if (slot.name.Substring(0, 6).Equals("Stored"))
        {
            selectedItemPlace = "stored";
            selectedItemIndex = int.Parse(slot.name.Substring(11, 2)) - 1;
            buy_sell_equip_unequipButton.interactable = true;
            buy_sell_equip_unequipButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            if (mode.Equals("equipment"))
            {
                upgrage_Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                buy_sell_equip_unequipButton.GetComponent<Image>().sprite = equipButtonSprite;
                if (storedItems[selectedItemIndex].GetStar() < 5)
                {
                    upgrage_Button.interactable = true;
                    SetCostValues(storedItems[selectedItemIndex].GetUpgradePrice());
                    costText.text = "Upgrade cost: ";
                }
                else
                {
                    upgrage_Button.interactable = false;
                    SetCostValues(-1);
                    costText.text = "Max upgrade";
                }
            }
            else if (mode.Equals("shop"))
            {
                upgrage_Button.interactable = false;
                upgrage_Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                buy_sell_equip_unequipButton.GetComponent<Image>().sprite = sellButtonSprite;
                costText.text = "Sell for: ";
                SetCostValues(Mathf.RoundToInt(storedItems[selectedItemIndex].GetPrice() * 0.5f));
            }
            if (equippedItems[GetTypeFromValue(storedItems[selectedItemIndex].GetItemType())] != null)
            {
                equippedItemText.text = equippedItems[GetTypeFromValue(storedItems[selectedItemIndex].GetItemType())].GetDescription();
                RefreshStarsOnEquippedItem(equippedItems[GetTypeFromValue(storedItems[selectedItemIndex].GetItemType())].GetStar());
            }
            else
            {
                equippedItemText.text = "";
                RefreshStarsOnEquippedItem(-1);
            }
            selectedItemText.text = storedItems[selectedItemIndex].GetDescription();
            RefreshStarsOnSelectedItem(storedItems[selectedItemIndex].GetStar());
        }
        else if (slot.name.Substring(0, 8).Equals("Equipped"))
        {
            selectedItemPlace = "equipped";
            selectedItemIndex = int.Parse(slot.name.Substring(13, 2)) - 1;
            upgrage_Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

            if (!"Weapon".Equals(equippedItems[selectedItemIndex].GetItemType()))
            {
                buy_sell_equip_unequipButton.GetComponent<Image>().sprite = unequipButtonSprite;
                buy_sell_equip_unequipButton.interactable = true;
                buy_sell_equip_unequipButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            } else
            {
                buy_sell_equip_unequipButton.interactable = false;
                buy_sell_equip_unequipButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            }

            selectedItemText.text = equippedItems[selectedItemIndex].GetDescription();
            RefreshStarsOnSelectedItem(equippedItems[selectedItemIndex].GetStar());
            equippedItemText.text = "";
            RefreshStarsOnEquippedItem(-1);
            if (equippedItems[selectedItemIndex].GetStar() < 5)
            {
                upgrage_Button.interactable = true;
                SetCostValues(equippedItems[selectedItemIndex].GetUpgradePrice());
                costText.text = "Upgrade cost: ";
            }
            else
            {
                upgrage_Button.interactable = false;
                SetCostValues(-1);
                costText.text = "Max upgrade";
            }
        }
    }

    public void ChooseEquipment()
    {
        SetItemSelected(null);
        shopButton.interactable = true;
        equipmentButton.interactable = false;
        equipmentSlotsGO.transform.localPosition = new Vector3(0f, 0f);
        shopSlotsGO.transform.localPosition = new Vector3(500f, 0f);
        mode = "equipment";

        RefreshStarsOnSelectedItem(-1);
        RefreshStarsOnEquippedItem(-1);

        foreach (GameObject go in equippedItemGOs)
        {
            if (go != null)
                go.transform.localPosition -= new Vector3(500f, 0f);
        }
        foreach (GameObject go in shopItemGOs)
        {
            if (go != null)
                go.transform.localPosition += new Vector3(500f, 0f);
        }
    }

    public void ChooseShop()
    {
        SetItemSelected(null);
        equipmentButton.interactable = true;
        shopButton.interactable = false;
        shopSlotsGO.transform.localPosition = new Vector3(0f, 0f);
        equipmentSlotsGO.transform.localPosition = new Vector3(500f, 0f);
        mode = "shop";

        RefreshStarsOnSelectedItem(-1);
        RefreshStarsOnEquippedItem(-1);

        foreach (GameObject go in shopItemGOs)
        {
            if(go != null)
                go.transform.localPosition -= new Vector3(500f, 0f);
        }
        foreach (GameObject go in equippedItemGOs)
        {
            if (go != null)
                go.transform.localPosition += new Vector3(500f, 0f);
        }
    }

    public void SaveItems()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/itemsData" + PlayerPrefs.GetInt("GameSlot") + ".dat");

        SerializationItems items = new SerializationItems();
        items.equippedItemsSer = equippedItems;
        items.storedItemsSer = storedItems;
        items.shopItemsSer = shopItems;

        bf.Serialize(file, items);
        file.Close();
    }

    public void LoadItems()
    {
        if (File.Exists(Application.persistentDataPath + "/itemsData" + PlayerPrefs.GetInt("GameSlot") + ".dat"))
         {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/itemsData" + PlayerPrefs.GetInt("GameSlot") + ".dat", FileMode.Open);
            SerializationItems items = (SerializationItems) bf.Deserialize(file);
            file.Close();

            equippedItems = items.equippedItemsSer;
            storedItems = items.storedItemsSer;
            shopItems = items.shopItemsSer;
         }
        MakeAllItem();
    }

    void InicializeEquippedItemTypes()
    {
        equippedItemTypes[0] = "Head";
        equippedItemTypes[1] = "Amulet";
        equippedItemTypes[2] = "Weapon";
        equippedItemTypes[3] = "Robe";
        equippedItemTypes[4] = "Offhand";
        equippedItemTypes[5] = "Ring";
        equippedItemTypes[6] = "Boots";
        equippedItemTypes[7] = "Ring";
    }

    int GetTypeFromValue(string str)
    {
        int i = 0;
        foreach(string s in equippedItemTypes)
        {
            if (s.Equals(str))
                return i;
            i++; 
        }
        return -1;
    }

    void MakeAllItem()
    {
        int i = 1;
        foreach (Item item in equippedItems)
        {
            if ((item != null) && !item.GetRarity().Equals("non"))
                MakeItem("EquippedSlot_", i, item);
            i++;
        }
        i = 1;
        foreach (Item item in storedItems)
        {
            if ((item != null) && !item.GetRarity().Equals("non"))
                MakeItem("StoredSlot_", i, item);
            i++;
        }
        i = 1;
        foreach (Item item in shopItems)
        {
            if ((item != null) && !item.GetRarity().Equals("non"))
                MakeItem("ShopSlot_", i, item);
            i++;
        }
    }

    void MakeItem(string slotType, int slotNum, Item i)
    {
        GameObject item = Instantiate(itemGO);
        item = item.transform.GetChild(0).gameObject;
        switch (slotType)
        {
            case "EquippedSlot_":
                equippedItemGOs[slotNum - 1] = item;
                break;
            case "StoredSlot_":
                storedItemGOs[slotNum - 1] = item;
                break;
            case "ShopSlot_":
                shopItemGOs[slotNum - 1] = item;
                break;
            default:
                shopItemGOs[slotNum - 1] = item;
                break;
        }
        item.gameObject.name = "Item" + slotNum;
        GameObject slot = GameObject.Find(slotType + slotNum.ToString("D2"));
        item.transform.position = slot.transform.position;
        item.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("Datas").GetComponent<ItemImages>().GetRaritySprite(i.GetRarity());
        switch (i.GetItemType())
        {
            case "Weapon":
                item.transform.GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Datas").GetComponent<ItemImages>().GetItemSprite(((Weapon)i).GetWeaponType());
                break;
            case "Offhand":
                item.transform.GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Datas").GetComponent<ItemImages>().GetItemSprite(((Offhand)i).GetOffhandType());
                break;
            default:
                item.transform.GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Datas").GetComponent<ItemImages>().GetItemSprite(i.GetItemType());
                break;
        }

        item.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = i.GetLevel().ToString();
        item.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = i.GetLevel().ToString();
    }

    public void RefreshShopItems()
    {
        foreach(GameObject go in shopItemGOs)
            if (go != null)
                Destroy(go.transform.gameObject.GetComponentInParent<Canvas>().gameObject);
        shopItemGOs = new GameObject[9];
        shopItems = new Item[9];
        System.Random r = new System.Random();
        for(int i = 1; i <= 9; i++)
        {
            string rarity = getItemRarityForSlot(r, i);
            string type = getRandomItemType(r);     
            int level = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Level");
            RefreshItem("ShopSlot_", i, rarity, type, level);
        }
        //SortShopItemsByRarity();
        SaveItems();
    }

    private string getItemRarityForSlot(System.Random r, int slotNum)
    {
        int rar = r.Next(1, 11);
        string rarity;
        switch (slotNum)
        {
            case 1:
            case 2:
            case 3:
                rarity = "Common";
                break;
            case 4:
            case 5:
            case 6:
                rarity = "Uncommon";
                break;
            case 7:
            case 8:
                rarity = "Heroic";
                break;
            case 9:
                rarity = "Uncommon";
                break;
            default:
                rarity = "Legendary";
                break;
        }
        return rarity;
    }

    private string getRandomItemType(System.Random r)
    {
        string type;

        int typ = r.Next(1, 12);
        switch (typ)
        {
            case 1:
                type = "Head";
                break;
            case 2:
                type = "Amulet";
                break;
            case 3:
                type = "Robe";
                break;
            case 4:
                type = "Boots";
                break;
            case 5:
                type = "Ring";
                break;
            case 6:
                type = "Bow";
                break;
            case 7:
                type = "Crossbow";
                break;
            case 8:
                type = "Staff";
                break;
            case 9:
                type = "Boomerang";
                break;
            case 10:
                type = "Shield";
                break;
            case 11:
                type = "Wand";
                break;
            default:
                type = "Head";
                break;
        }
        return type;
    }

    public void AddGold()
    {
        int g = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold");
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold", g + 100000);
    }

    public void AddLevel()
    {
        int level = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Level");
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Level", level + 1);
        SceneManager.LoadScene("ItemsMenu");
    }

    void RefreshItem(string slotType, int slotNum, string rarity, string itemType, int level)
    {
        GameObject item = Instantiate(itemGO);
        item = item.transform.GetChild(0).gameObject;
        switch (slotType)
        {
            case "EquippedSlot_":
                equippedItems[slotNum - 1] = NewItem(itemType, rarity, level);
                equippedItemGOs[slotNum - 1] = item;
                break;
            case "StoredSlot_":
                storedItems[slotNum - 1] = NewItem(itemType, rarity, level);
                storedItemGOs[slotNum - 1] = item;
                break;
            case "ShopSlot_":
                shopItems[slotNum - 1] = NewItem(itemType, rarity, level);
                shopItemGOs[slotNum - 1] = item;
                break;
            default:
                storedItems[slotNum - 1] = NewItem(itemType, rarity, level);
                storedItemGOs[slotNum - 1] = item;
                break;
        }
        item.gameObject.name = "Item" + slotNum;
        GameObject slot = GameObject.Find(slotType + slotNum.ToString("D2"));
        item.transform.position = slot.transform.position;
        item.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("Datas").GetComponent<ItemImages>().GetRaritySprite(rarity);
        item.transform.GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Datas").GetComponent<ItemImages>().GetItemSprite(itemType);
        item.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = level.ToString();
        item.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = level.ToString();
    }

    Item NewItem(string itemType, string rarity, int level)
    {
        switch (itemType)
        {
            case "Head":
                return new Head(rarity, level);
            case "Amulet":
                return new Amulet(rarity, level);
            case "Robe":
                return new Robe(rarity, level);
            case "Boots":
                return new Boots(rarity, level);
            case "Ring":
                return new Ring(rarity, level);
            case "Bow":
                return new Bow(rarity, level);
            case "Crossbow":
                return new Crossbow(rarity, level);
            case "Staff":
                return new Staff(rarity, level);
            case "Boomerang":
                return new Boomerang(rarity, level);
            case "Shield":
                return new Shield(rarity, level);
            case "Wand":
                return new Wand(rarity, level);
            default:
                return null;
        }
    }

    void SortShopItemsByRarity()
    {
        for(int i = 0; i < shopItems.Length - 1; i++)
            for(int j = i + 1; j < shopItems.Length; j++)
            {
                if(CompareItemsByRarity_FirstIsHigher(shopItems[i], shopItems[j]))
                {
                    Item tmpItem = shopItems[i];
                    shopItems[i] = shopItems[j];
                    shopItems[j] = tmpItem;
                    PlaceItemGO("shop", i, "ShopSlot_" + (j + 1).ToString("D2"));
                    PlaceItemGO("shop", j, "ShopSlot_" + (i + 1).ToString("D2"));
                    GameObject tmpGO = shopItemGOs[i];
                    shopItemGOs[i] = shopItemGOs[j];
                    shopItemGOs[j] = tmpGO;
                }
            }
    }

    bool CompareItemsByRarity_FirstIsHigher(Item first, Item second)
    {
        string fRar = first.GetRarity();
        string sRar = second.GetRarity();
        switch (fRar)
        {
            case "Legendary":
                return true;
            case "Heroic":
                if (sRar.Equals("Legendary"))
                    return false;
                else return true;
            case "Uncommon":
                if (sRar.Equals("Common"))
                    return true;
                else return false;
            case "Common":
                return false;
            default:
                return true;
        }
    }

    public void PlaceItem(string originalSlot, string destinationSlot)
    {
        if (originalSlot.Substring(0, 4).Equals("Shop"))
        {
            if (destinationSlot.Substring(0, 6).Equals("Stored"))
            {
                BuyItem(int.Parse(originalSlot.Substring(9, 2)), int.Parse(destinationSlot.Substring(11, 2)));
                SetItemSelected(null);
            }
            else
            {
                shopItemGOs[int.Parse(originalSlot.Substring(9, 2)) - 1].transform.position = shopItemGOs[int.Parse(originalSlot.Substring(9, 2)) - 1].transform.GetChild(3).GetComponent<ItemDrag>().GetStartingPosition();
            }
        }
        else
        if (originalSlot.Substring(0, 6).Equals("Stored"))
        {
            if (destinationSlot.Substring(0, 4).Equals("Shop"))
            {
                SellItem(int.Parse(originalSlot.Substring(11, 2)));
                SetItemSelected(null);
            }
            else if (destinationSlot.Substring(0, 6).Equals("Stored"))
                PlaceStorageItems(int.Parse(originalSlot.Substring(11, 2)), int.Parse(destinationSlot.Substring(11, 2)));
            else if (destinationSlot.Substring(0, 8).Equals("Equipped"))
            {
                EquipItem(int.Parse(originalSlot.Substring(11, 2)), int.Parse(destinationSlot.Substring(13, 2)));
                SetItemSelected(null);
            }
        }
        else
        if (originalSlot.Substring(0, 8).Equals("Equipped"))
        {
            if (destinationSlot.Substring(0, 6).Equals("Stored"))
            {
                UnequipItem(int.Parse(originalSlot.Substring(13, 2)), int.Parse(destinationSlot.Substring(11, 2)));
                SetItemSelected(null);
            }
            else
            {
                equippedItemGOs[int.Parse(originalSlot.Substring(13, 2)) - 1].transform.position = equippedItemGOs[int.Parse(originalSlot.Substring(13, 2)) - 1].transform.GetChild(3).GetComponent<ItemDrag>().GetStartingPosition();
            }
        }
        SaveItems();
    }

    void PlaceStorageItems(int originalIdx, int destinationIdx)
    {
        originalIdx--;
        destinationIdx--;
        if (originalIdx != destinationIdx)
        {
            if ((storedItems[destinationIdx] != null) && !storedItems[destinationIdx].GetRarity().Equals("non"))
            {
                PlaceItemGO("storage", originalIdx, "StoredSlot_" + (destinationIdx + 1).ToString("D2"));
                PlaceItemGO("storage", destinationIdx, "StoredSlot_" + (originalIdx + 1).ToString("D2")); Item tmpItem;

                tmpItem = storedItems[destinationIdx];
                storedItems[destinationIdx] = storedItems[originalIdx];
                storedItems[originalIdx] = tmpItem;
                GameObject tmpGO;
                tmpGO = storedItemGOs[destinationIdx];
                storedItemGOs[destinationIdx] = storedItemGOs[originalIdx];
                storedItemGOs[originalIdx] = tmpGO;
            }
            else
            {
                PlaceItemGO("storage", originalIdx, "StoredSlot_" + (destinationIdx + 1).ToString("D2"));

                storedItems[destinationIdx] = storedItems[originalIdx];
                storedItems[originalIdx] = null;
                storedItemGOs[destinationIdx] = storedItemGOs[originalIdx];
                storedItemGOs[originalIdx] = null;
            }
            SetItemSelected(null);
        }
        else
            PlaceItemGO("storage", originalIdx, "StoredSlot_" + (originalIdx + 1).ToString("D2"));
    }

    void EquipItem(int storedItemIdx, int equippedItemIdx)
    {
        storedItemIdx--;
        equippedItemIdx--;
        if (storedItems[storedItemIdx].GetItemType() == equippedItemTypes[equippedItemIdx])
        {
            if (equippedItemTypes[equippedItemIdx].Equals("Weapon"))
            {
                if (!((Weapon)storedItems[storedItemIdx]).GetTwoHanded() || ((Weapon)equippedItems[equippedItemIdx]).GetTwoHanded())
                {
                    PlaceItemGO("storage", storedItemIdx, "EquippedSlot_" + (equippedItemIdx + 1).ToString("D2"));
                    PlaceItemGO("equip", equippedItemIdx, "StoredSlot_" + (storedItemIdx + 1).ToString("D2"));

                    Item tmpItem;
                    tmpItem = equippedItems[equippedItemIdx];
                    equippedItems[equippedItemIdx] = storedItems[storedItemIdx];
                    storedItems[storedItemIdx] = tmpItem;
                    GameObject tmpGO;
                    tmpGO = equippedItemGOs[equippedItemIdx];
                    equippedItemGOs[equippedItemIdx] = storedItemGOs[storedItemIdx];
                    storedItemGOs[storedItemIdx] = tmpGO;
                }
                else
                {
                    if ((equippedItems[4] == null) || equippedItems[4].GetRarity().Equals("non"))
                    {
                        PlaceItemGO("storage", storedItemIdx, "EquippedSlot_" + (equippedItemIdx + 1).ToString("D2"));
                        PlaceItemGO("equip", equippedItemIdx, "StoredSlot_" + (storedItemIdx + 1).ToString("D2")); Item tmpItem;

                        tmpItem = equippedItems[equippedItemIdx];
                        equippedItems[equippedItemIdx] = storedItems[storedItemIdx];
                        storedItems[storedItemIdx] = tmpItem;
                        GameObject tmpGO;
                        tmpGO = equippedItemGOs[equippedItemIdx];
                        equippedItemGOs[equippedItemIdx] = storedItemGOs[storedItemIdx];
                        storedItemGOs[storedItemIdx] = tmpGO;
                    }
                    else
                    {
                        if (GetFirstEmptyStoredItemSpace() > 0)
                        {
                            PlaceItemGO("storage", storedItemIdx, "EquippedSlot_" + (equippedItemIdx + 1).ToString("D2"));
                            PlaceItemGO("equip", equippedItemIdx, "StoredSlot_" + (storedItemIdx + 1).ToString("D2"));

                            UnequipItem(5, GetFirstEmptyStoredItemSpace());
                            Item tmpItem;
                            tmpItem = equippedItems[equippedItemIdx];
                            equippedItems[equippedItemIdx] = storedItems[storedItemIdx];
                            storedItems[storedItemIdx] = tmpItem;
                            GameObject tmpGO;
                            tmpGO = equippedItemGOs[equippedItemIdx];
                            equippedItemGOs[equippedItemIdx] = storedItemGOs[storedItemIdx];
                            storedItemGOs[storedItemIdx] = tmpGO;
                        }
                        else
                            storedItemGOs[storedItemIdx].transform.position = storedItemGOs[storedItemIdx].transform.GetChild(3).GetComponent<ItemDrag>().GetStartingPosition();
                    }
                }
            }
            else
            {
                if ((equippedItems[equippedItemIdx] != null) && !equippedItems[equippedItemIdx].GetRarity().Equals("non"))
                {
                    PlaceItemGO("storage", storedItemIdx, "EquippedSlot_" + (equippedItemIdx + 1).ToString("D2"));
                    PlaceItemGO("equip", equippedItemIdx, "StoredSlot_" + (storedItemIdx + 1).ToString("D2"));

                    Item tmpItem;
                    tmpItem = equippedItems[equippedItemIdx];
                    equippedItems[equippedItemIdx] = storedItems[storedItemIdx];
                    storedItems[storedItemIdx] = tmpItem;
                    GameObject tmpGO;
                    tmpGO = equippedItemGOs[equippedItemIdx];
                    equippedItemGOs[equippedItemIdx] = storedItemGOs[storedItemIdx];
                    storedItemGOs[storedItemIdx] = tmpGO;
                }
                else
                {
                    if (storedItems[storedItemIdx].GetItemType().Equals("Offhand") && ((Weapon)equippedItems[2]).GetTwoHanded())
                    {
                        storedItemGOs[storedItemIdx].transform.position = storedItemGOs[storedItemIdx].transform.GetChild(3).GetComponent<ItemDrag>().GetStartingPosition();
                    }
                    else
                    {
                        PlaceItemGO("storage", storedItemIdx, "EquippedSlot_" + (equippedItemIdx + 1).ToString("D2"));

                        equippedItems[equippedItemIdx] = storedItems[storedItemIdx];
                        storedItems[storedItemIdx] = null;
                        equippedItemGOs[equippedItemIdx] = storedItemGOs[storedItemIdx];
                        storedItemGOs[storedItemIdx] = null;
                    }
                }
            }
        }
        else
            storedItemGOs[storedItemIdx].transform.position = storedItemGOs[storedItemIdx].transform.GetChild(3).GetComponent<ItemDrag>().GetStartingPosition();
    }

    void UnequipItem(int equippedItemIdx, int storedItemIdx)
    {
        equippedItemIdx--;
        storedItemIdx--;
        if ((storedItems[storedItemIdx] == null) || storedItems[storedItemIdx].GetRarity().Equals("non"))
        {
            if (!equippedItemTypes[equippedItemIdx].Equals("Weapon"))
            {
                PlaceItemGO("equip", equippedItemIdx, "StoredSlot_" + (storedItemIdx + 1).ToString("D2"));

                storedItems[storedItemIdx] = equippedItems[equippedItemIdx];
                equippedItems[equippedItemIdx] = null;
                storedItemGOs[storedItemIdx] = equippedItemGOs[equippedItemIdx];
                equippedItemGOs[equippedItemIdx] = null;
            }
            else
            {
                equippedItemGOs[equippedItemIdx].transform.position = equippedItemGOs[equippedItemIdx].transform.GetChild(3).GetComponent<ItemDrag>().GetStartingPosition();
            }
        }
        else
        {
            if (storedItems[storedItemIdx].GetItemType() == equippedItems[equippedItemIdx].GetItemType())
            {
                if (equippedItemTypes[equippedItemIdx].Equals("Weapon"))
                {
                    if (!((Weapon)storedItems[storedItemIdx]).GetTwoHanded() || ((Weapon)equippedItems[equippedItemIdx]).GetTwoHanded())
                    {
                        PlaceItemGO("equip", equippedItemIdx, "StoredSlot_" + (storedItemIdx + 1).ToString("D2"));
                        PlaceItemGO("storage", storedItemIdx, "EquippedSlot_" + (equippedItemIdx + 1).ToString("D2"));

                        Item tmpItem;
                        tmpItem = storedItems[storedItemIdx];
                        storedItems[storedItemIdx] = equippedItems[equippedItemIdx];
                        equippedItems[equippedItemIdx] = tmpItem;
                        GameObject tmpGO;
                        tmpGO = storedItemGOs[storedItemIdx];
                        storedItemGOs[storedItemIdx] = equippedItemGOs[equippedItemIdx];
                        equippedItemGOs[equippedItemIdx] = tmpGO;
                    }
                    else
                    {
                        if ((equippedItems[4] == null) || equippedItems[4].GetRarity().Equals("non"))
                        {
                            PlaceItemGO("equip", equippedItemIdx, "StoredSlot_" + (storedItemIdx + 1).ToString("D2"));
                            PlaceItemGO("storage", storedItemIdx, "EquippedSlot_" + (equippedItemIdx + 1).ToString("D2"));

                            Item tmpItem;
                            tmpItem = storedItems[storedItemIdx];
                            storedItems[storedItemIdx] = equippedItems[equippedItemIdx];
                            equippedItems[equippedItemIdx] = tmpItem;
                            GameObject tmpGO;
                            tmpGO = storedItemGOs[storedItemIdx];
                            storedItemGOs[storedItemIdx] = equippedItemGOs[equippedItemIdx];
                            equippedItemGOs[equippedItemIdx] = tmpGO;
                        }
                        else
                        if (GetFirstEmptyStoredItemSpace() > 0)
                        {
                            PlaceItemGO("equip", equippedItemIdx, "StoredSlot_" + (storedItemIdx + 1).ToString("D2"));
                            PlaceItemGO("storage", storedItemIdx, "EquippedSlot_" + (equippedItemIdx + 1).ToString("D2"));

                            UnequipItem(5, GetFirstEmptyStoredItemSpace());
                            Item tmpItem;
                            tmpItem = storedItems[storedItemIdx];
                            storedItems[storedItemIdx] = equippedItems[equippedItemIdx];
                            equippedItems[equippedItemIdx] = tmpItem;
                            GameObject tmpGO;
                            tmpGO = storedItemGOs[storedItemIdx];
                            storedItemGOs[storedItemIdx] = equippedItemGOs[equippedItemIdx];
                            equippedItemGOs[equippedItemIdx] = tmpGO;
                        }
                        else
                            equippedItemGOs[equippedItemIdx].transform.position = equippedItemGOs[equippedItemIdx].transform.GetChild(3).GetComponent<ItemDrag>().GetStartingPosition();
                    }
                }
                else
                {
                    PlaceItemGO("equip", equippedItemIdx, "StoredSlot_" + (storedItemIdx + 1).ToString("D2"));
                    PlaceItemGO("storage", storedItemIdx, "EquippedSlot_" + (equippedItemIdx + 1).ToString("D2"));

                    Item tmpItem;
                    tmpItem = storedItems[storedItemIdx];
                    storedItems[storedItemIdx] = equippedItems[equippedItemIdx];
                    equippedItems[equippedItemIdx] = tmpItem;
                    GameObject tmpGO;
                    tmpGO = storedItemGOs[storedItemIdx];
                    storedItemGOs[storedItemIdx] = equippedItemGOs[equippedItemIdx];
                    equippedItemGOs[equippedItemIdx] = tmpGO;
                }
            }
            else
                equippedItemGOs[equippedItemIdx].transform.position = equippedItemGOs[equippedItemIdx].transform.GetChild(3).GetComponent<ItemDrag>().GetStartingPosition();
        }
    }

    void BuyItem(int shopItemIdx, int storedItemIdx)
    {
        shopItemIdx--;
        storedItemIdx--;
        if(PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold") >= shopItems[shopItemIdx].GetPrice())
        {
            if ((storedItems[storedItemIdx] == null) || storedItems[storedItemIdx].GetRarity().Equals("non"))
            {
                int g = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold");
                PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold", g - shopItems[shopItemIdx].GetPrice());
                storedItems[storedItemIdx] = shopItems[shopItemIdx];
                shopItems[shopItemIdx] = null;
                PlaceItemGO("shop", shopItemIdx, "StoredSlot_" + (storedItemIdx + 1).ToString("D2"));
                storedItemGOs[storedItemIdx] = shopItemGOs[shopItemIdx];
                shopItemGOs[shopItemIdx] = null;
            }
            else
                shopItemGOs[shopItemIdx].transform.position = shopItemGOs[shopItemIdx].transform.GetChild(3).GetComponent<ItemDrag>().GetStartingPosition();
        }
        else
            shopItemGOs[shopItemIdx].transform.position = shopItemGOs[shopItemIdx].transform.GetChild(3).GetComponent<ItemDrag>().GetStartingPosition();
    }

    void SellItem(int storedItemIdx)
    {
        storedItemIdx--;


        int g = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold");
        PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Gold", g + Mathf.RoundToInt(storedItems[storedItemIdx].GetPrice() * 0.8f));
        storedItems[storedItemIdx] = null;
        Destroy(storedItemGOs[storedItemIdx].transform.parent.gameObject);
        storedItemGOs[storedItemIdx] = null;

        RefreshStarsOnSelectedItem(-1);
        RefreshStarsOnEquippedItem(-1);
    }

    public void PressYes()
    {
        pressedYes = true;
        waitingForPress = false;
        confirmGO.transform.localScale = Vector3.zero;
    }

    public void PressNo()
    {
        pressedYes = false;
        waitingForPress = false;
        confirmGO.transform.localScale = Vector3.zero;
    }

    public int GetFirstEmptyStoredItemSpace()
    {
        int idx = -1;
        int i = 0;
        bool found = false;
        while(!found && (i < storedItems.Length))
        {
            if ((storedItems[i] == null) || storedItems[i].GetRarity().Equals("non"))
            {
                found = true;
                idx = i;
            }
            i++;
        }
        return idx + 1;
    }

    public void PlaceItemGO(string place, int slotNum, string s)
    {
        GameObject slot = GameObject.Find(s);
        GameObject item;
        switch (place)
        {
            case "equip":
                item = equippedItemGOs[slotNum];
                break;
            case "storage":
                item = storedItemGOs[slotNum];
                break;
            case "shop":
                item = shopItemGOs[slotNum];
                break;
            default:
                return;
        }
        item.transform.position = slot.transform.position;
        item.transform.GetChild(3).GetComponent<ItemDrag>().SetStartingPosition(item.transform.position);
    }

    public void BackToMenu()
    {
        SaveItems();
        SceneManager.LoadScene("GameplayMenu");
    }
}
