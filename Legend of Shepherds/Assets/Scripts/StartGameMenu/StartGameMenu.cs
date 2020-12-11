using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameMenu : MonoBehaviour {

    public GameObject confirmMenu;
    public Text confirmMenuText;
    public Text delete_text;

    public Button yesButton;
    public Button noButton;

    public Image slot1_image;
    public Text slot1_name;
    public Text slot1_level;
    public Button slot1_start;
    public Button slot1_delete;
    public Image slot2_image;
    public Text slot2_name;
    public Text slot2_level;
    public Button slot2_start;
    public Button slot2_delete;
    public Image slot3_image;
    public Text slot3_name;
    public Text slot3_level;
    public Button slot3_start;
    public Button slot3_delete;
    public Image slot4_image;
    public Text slot4_name;
    public Text slot4_level;
    public Button slot4_start;
    public Button slot4_delete;
    public Image slot5_image;
    public Text slot5_name;
    public Text slot5_level;
    public Button slot5_start;
    public Button slot5_delete;
    public Image slot6_image;
    public Text slot6_name;
    public Text slot6_level;
    public Button slot6_start;
    public Button slot6_delete;

    string character;
    NewCharacterMenuImages newCharacterMenuImages;

    void Start()
    {
        newCharacterMenuImages = gameObject.GetComponent<NewCharacterMenuImages>();

        if (PlayerPrefs.GetInt("Slot1_Empty") == 0)
        {
            string character = PlayerPrefs.GetString("Slot1_Character");
            slot1_name.text = PlayerPrefs.GetString("Slot1_Name");
            slot1_level.text = "Lvl " + PlayerPrefs.GetInt("Slot1_Level");
            slot1_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(true, character);
            slot1_start.interactable = true;
            slot1_delete.interactable = true;
        }
        else
        {
            slot1_name.text = "Empty";
            slot1_level.text = "Lvl ??";
            slot1_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
            slot1_start.interactable = true;
            slot1_delete.interactable = false;
        }

        if (PlayerPrefs.GetInt("Slot2_Empty") == 0)
        {
            string character = PlayerPrefs.GetString("Slot2_Character");
            slot2_name.text = PlayerPrefs.GetString("Slot2_Name");
            slot2_level.text = "Lvl " + PlayerPrefs.GetInt("Slot2_Level");
            slot2_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(true, character);
            slot2_start.interactable = true;
            slot2_delete.interactable = true;
        }
        else
        {
            slot2_name.text = "Empty";
            slot2_level.text = "Lvl ??";
            slot2_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
            slot2_start.interactable = true;
            slot2_delete.interactable = false;
        }

        if (PlayerPrefs.GetInt("Slot3_Empty") == 0)
        {
            string character = PlayerPrefs.GetString("Slot3_Character");
            slot3_name.text = PlayerPrefs.GetString("Slot3_Name");
            slot3_level.text = "Lvl " + PlayerPrefs.GetInt("Slot3_Level");
            slot3_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(true, character);
            slot3_start.interactable = true;
            slot3_delete.interactable = true;
        }
        else
        {
            slot3_name.text = "Empty";
            slot3_level.text = "Lvl ??";
            slot3_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
            slot3_start.interactable = true;
            slot3_delete.interactable = false;
        }

        if (PlayerPrefs.GetInt("Slot4_Empty") == 0)
        {
            string character = PlayerPrefs.GetString("Slot4_Character");
            slot4_name.text = PlayerPrefs.GetString("Slot4_Name");
            slot4_level.text = "Lvl " + PlayerPrefs.GetInt("Slot4_Level");
            slot4_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(true, character);
            slot4_start.interactable = true;
            slot4_delete.interactable = true;
        }
        else
        {
            slot4_name.text = "Empty";
            slot4_level.text = "Lvl ??";
            slot4_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
            slot4_start.interactable = true;
            slot4_delete.interactable = false;
        }

        if (PlayerPrefs.GetInt("Slot5_Empty") == 0)
        {
            string character = PlayerPrefs.GetString("Slot5_Character");
            slot5_name.text = PlayerPrefs.GetString("Slot5_Name");
            slot5_level.text = "Lvl " + PlayerPrefs.GetInt("Slot5_Level");
            slot5_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(true, character);
            slot5_start.interactable = true;
            slot5_delete.interactable = true;
        }
        else
        {
            slot5_name.text = "Empty";
            slot5_level.text = "Lvl ??";
            slot5_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
            slot5_start.interactable = true;
            slot5_delete.interactable = false;
        }

        if (PlayerPrefs.GetInt("Slot6_Empty") == 0)
        {
            string character = PlayerPrefs.GetString("Slot6_Character");
            slot6_name.text = PlayerPrefs.GetString("Slot6_Name");
            slot6_level.text = "Lvl " + PlayerPrefs.GetInt("Slot6_Level");
            slot6_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(true, character);
            slot6_start.interactable = true;
            slot6_delete.interactable = true;
        }
        else
        {
            slot6_name.text = "Empty";
            slot6_level.text = "Lvl ??";
            slot6_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
            slot6_start.interactable = true;
            slot6_delete.interactable = false;
        }
    }

    void Update()
    {
        if (delete_text.text.Equals("DELETE"))
        {
            yesButton.interactable = true;
        }
        else
        {
            yesButton.interactable = false;
        }    
    }

    public void Slot1_StartProcess()
    {
        if (PlayerPrefs.GetInt("Slot1_Empty") != 0)
        {
            PlayerPrefs.SetInt("GameSlot", 1);
            SceneManager.LoadScene("NewCharacterMenu");
        }
        else {
            PlayerPrefs.SetInt("GameSlot", 1);
            SceneManager.LoadScene("GameplayMenu");
        }
    }

    public void Slot2_StartProcess()
    {
        if (PlayerPrefs.GetInt("Slot2_Empty") != 0)
        {
            PlayerPrefs.SetInt("GameSlot", 2);
            SceneManager.LoadScene("NewCharacterMenu");
        }
        else
        {
            PlayerPrefs.SetInt("GameSlot", 2);
            SceneManager.LoadScene("GameplayMenu");
        }
    }

    public void Slot3_StartProcess()
    {
        if (PlayerPrefs.GetInt("Slot3_Empty") != 0)
        {
            PlayerPrefs.SetInt("GameSlot", 3);
            SceneManager.LoadScene("NewCharacterMenu");
        }
        else
        {
            PlayerPrefs.SetInt("GameSlot", 3);
            SceneManager.LoadScene("GameplayMenu");
        }
    }

    public void Slot4_StartProcess()
    {
        if (PlayerPrefs.GetInt("Slot4_Empty") != 0)
        {
            PlayerPrefs.SetInt("GameSlot", 4);
            SceneManager.LoadScene("NewCharacterMenu");
        }
        else
        {
            PlayerPrefs.SetInt("GameSlot", 4);
            SceneManager.LoadScene("GameplayMenu");
        }
    }

    public void Slot5_StartProcess()
    {
        if (PlayerPrefs.GetInt("Slot5_Empty") != 0)
        {
            PlayerPrefs.SetInt("GameSlot", 5);
            SceneManager.LoadScene("NewCharacterMenu");
        }
        else
        {
            PlayerPrefs.SetInt("GameSlot", 5);
            SceneManager.LoadScene("GameplayMenu");
        }
    }

    public void Slot6_StartProcess()
    {
        if (PlayerPrefs.GetInt("Slot6_Empty") != 0)
        {
            PlayerPrefs.SetInt("GameSlot", 6);
            SceneManager.LoadScene("NewCharacterMenu");
        }
        else
        {
            PlayerPrefs.SetInt("GameSlot", 6);
            SceneManager.LoadScene("GameplayMenu");
        }
    }

    public void Slot1_Delete()
    {
        PlayerPrefs.SetInt("GameSlot", 1);
        confirmMenu.SetActive(true);
        confirmMenuText.text = "To delete slot 1 type in 'DELETE' then press Yes.";
    }

    public void Slot2_Delete()
    {
        PlayerPrefs.SetInt("GameSlot", 2);
        confirmMenu.SetActive(true);
        confirmMenuText.text = "To delete slot 2 type in 'DELETE' then press Yes.";
    }

    public void Slot3_Delete()
    {
        PlayerPrefs.SetInt("GameSlot", 3);
        confirmMenu.SetActive(true);
        confirmMenuText.text = "To delete slot 3 type in 'DELETE' then press Yes.";
    }

    public void Slot4_Delete()
    {
        PlayerPrefs.SetInt("GameSlot", 4);
        confirmMenu.SetActive(true);
        confirmMenuText.text = "To delete slot 4 type in 'DELETE' then press Yes.";
    }

    public void Slot5_Delete()
    {
        PlayerPrefs.SetInt("GameSlot", 5);
        confirmMenu.SetActive(true);
        confirmMenuText.text = "To delete slot 5 type in 'DELETE' then press Yes.";
    }

    public void Slot6_Delete()
    {
        PlayerPrefs.SetInt("GameSlot", 6);
        confirmMenu.SetActive(true);
        confirmMenuText.text = "To delete slot 6 type in 'DELETE' then press Yes.";
    }

    public void DeleteSlot(int slotNumber)
    {
        switch (slotNumber)
        {
            case 1:
                slot1_name.text = "Empty";
                slot1_level.text = "Lvl ??";
                slot1_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
                slot1_start.interactable = true;
                slot1_delete.interactable = false;
                break;
            case 2:
                slot2_name.text = "Empty";
                slot2_level.text = "Lvl ??";
                slot2_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
                slot2_start.interactable = true;
                slot2_delete.interactable = false;
                break;
            case 3:
                slot3_name.text = "Empty";
                slot3_level.text = "Lvl ??";
                slot3_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
                slot3_start.interactable = true;
                slot3_delete.interactable = false;
                break;
            case 4:
                slot4_name.text = "Empty";
                slot4_level.text = "Lvl ??";
                slot4_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
                slot4_start.interactable = true;
                slot4_delete.interactable = false;
                break;
            case 5:
                slot5_name.text = "Empty";
                slot5_level.text = "Lvl ??";
                slot5_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
                slot5_start.interactable = true;
                slot5_delete.interactable = false;
                break;
            case 6:
                slot6_name.text = "Empty";
                slot6_level.text = "Lvl ??";
                slot6_image.overrideSprite = newCharacterMenuImages.GetCharacterSprite(false, "Empty");
                slot6_start.interactable = true;
                slot6_delete.interactable = false;
                break;
            default:
                break;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
