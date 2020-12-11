using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfirmDeleteSlot : MonoBehaviour {

    public InputField delete_inputfield;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void DeleteSlot_ConfirmYes()
    {
        gameObject.GetComponent<Save_Load>().ClearSlot(PlayerPrefs.GetInt("GameSlot"));
        GameObject.Find("StartGameMenu").GetComponent<StartGameMenu>().DeleteSlot(PlayerPrefs.GetInt("GameSlot"));
        delete_inputfield.text = "";
        gameObject.SetActive(false);
    }

    public void DeleteSlot_ConfirmNo()
    {
        delete_inputfield.text = "";
        gameObject.SetActive(false);
    }
}
