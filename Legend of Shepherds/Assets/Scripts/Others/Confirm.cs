using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Confirm : MonoBehaviour {

    public InputField delete_inputfield;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void DeleteSlot_ConfirmYes()
    {
        gameObject.GetComponent<Save_Load>().ClearSlot(PlayerPrefs.GetInt("GameSlot"));
        gameObject.SetActive(false);
    }

    void DeleteSlot_ConfirmNo()
    {
        delete_inputfield.text = "";
        gameObject.SetActive(false);
    }
}
