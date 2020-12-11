using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmMenu : MonoBehaviour {

    public Text text;
    public GameObject OptionalGO;
    public GameObject MoneyGO;
    public GameObject FairyShardGO;
    public GameObject InvokerGO;

	void Start () {
        PlayerPrefs.SetInt("ConfirmMenuState", -1);
    }

    public void SetText(string t)
    {
        text.text = t;
    }

    public void SetOptionalValue(string type, int value = -1)
    {
        switch (type)
        {
            case "Money":
                OptionalGO = Instantiate(MoneyGO);
                if (value != -1)
                    OptionalGO.GetComponent<SetMoneyValue>().SetValue(value);
                break;
            case "FairyShard":
                OptionalGO = Instantiate(FairyShardGO);
                if (value != -1)
                    OptionalGO.GetComponent<SetFairyShardValue>().SetValue(value);
                break;
        }
    }

    public void SetInvokerGO(GameObject go)
    {
        InvokerGO = go;
    }

    public void PressedYes()
    {
        PlayerPrefs.SetInt("ConfirmMenuState", 1);
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void PressedNo()
    {
        PlayerPrefs.SetInt("ConfirmMenuState", 0);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
