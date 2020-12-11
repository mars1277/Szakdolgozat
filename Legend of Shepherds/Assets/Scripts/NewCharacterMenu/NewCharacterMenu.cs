using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;

public class NewCharacterMenu : MonoBehaviour {

    public InputField NameInputField;

    public Image skillSample_1;
    public Image skillSample_2;
    public Image skillSample_3;
    public Image skillSample_4;

    public Image previousCharacter;
    public Image actualCharacter;
    public Image nextCharacter;
    public Image soonImage;

    public Text characterDescriptionText;

    int numberOfCharacters = 1;
    int indexOfSelectedCharacter = 1;

    void Start()
    {
        ChoosedCharacterChanged();
    }

    public void CharacterChooserLeft()
    {
        if (indexOfSelectedCharacter > 1)
        {
            indexOfSelectedCharacter--;
            ChoosedCharacterChanged();
            if(indexOfSelectedCharacter == numberOfCharacters - 1)
                soonImage.color = new Color(soonImage.color.r, soonImage.color.g, soonImage.color.b, 0f);
        }
    }

    public void CharacterChooserRight()
    {
        if (indexOfSelectedCharacter < numberOfCharacters)
        {
            indexOfSelectedCharacter++;
            ChoosedCharacterChanged();
            if(indexOfSelectedCharacter == numberOfCharacters)
                soonImage.color = new Color(soonImage.color.r, soonImage.color.g, soonImage.color.b, 1f);
        }
    }

    public void ChoosedCharacterChanged()
    {
        switch (indexOfSelectedCharacter)
        {
            case 1:
                PlayerPrefs.SetString("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Character", "FireMage");
                skillSample_1.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetSkillSampleSprite(1, "FireMage");
                skillSample_2.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetSkillSampleSprite(2, "FireMage");
                skillSample_3.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetSkillSampleSprite(3, "FireMage");
                skillSample_4.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetSkillSampleSprite(4, "FireMage");
                previousCharacter.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetCharacterSprite(false, "Empty");
                actualCharacter.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetCharacterSprite(true, "FireMage");
                nextCharacter.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetCharacterSprite(false, "FrostMage");
                characterDescriptionText.text = GameObject.Find("Datas").GetComponent<CharacterDescriptions>().GetCharacterDescription("FireMage");
                break;
            case 2:
                PlayerPrefs.SetString("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Character", "FrostMage");
                skillSample_1.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetSkillSampleSprite(1, "FrostMage");
                skillSample_2.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetSkillSampleSprite(2, "FrostMage");
                skillSample_3.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetSkillSampleSprite(3, "FrostMage");
                skillSample_4.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetSkillSampleSprite(4, "FrostMage");
                previousCharacter.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetCharacterSprite(false, "FireMage");
                actualCharacter.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetCharacterSprite(true, "FrostMage");
                nextCharacter.sprite = GameObject.Find("Datas").GetComponent<NewCharacterMenuImages>().GetCharacterSprite(false, "Empty");
                characterDescriptionText.text = GameObject.Find("Datas").GetComponent<CharacterDescriptions>().GetCharacterDescription("FrostMage");
                break;
            default:
                break;
        }
    }

    public int GetSelectedCharacterIndex()
    {
        return indexOfSelectedCharacter;
    }

    public void StartNewGame()
    {
        if (!NameInputField.text.Equals(""))
        {
            string character;
            switch (indexOfSelectedCharacter)
            {
                case 1:
                    character = "FireMage";
                    break;
                case 2:
                    character = "FrostMage";
                    break;
                default:
                    character = "FireMage";
                    break;
            }
            DateTime yesterday = DateTime.Today.AddDays(-1f);
            string yesterdayStr = yesterday.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));
            gameObject.GetComponentInChildren<Save_Load>().SaveSlot(PlayerPrefs.GetInt("GameSlot"), NameInputField.text, character, 1, 0, 0, "0000000000000000000", "0000000000000000000", "0000000000000000000", 0, 1999, 1, yesterdayStr, 0, 0, 0, true);
            SceneManager.LoadScene("GameplayMenu");
        }
    }

    public void BackToMenu()
    {
        PlayerPrefs.SetString("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Character", "");
        SceneManager.LoadScene("StartGameMenu");
    }
}
