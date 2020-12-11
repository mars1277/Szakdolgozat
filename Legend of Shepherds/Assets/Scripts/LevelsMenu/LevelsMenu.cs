using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour {

    public Button[] buttons;
    int dayPassedNumber;

    void Start()
    {
        PlayerPrefs.SetInt("SelectedLevel", 0);
        dayPassedNumber = PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_DayPassed");
        for (int i = 0; i < dayPassedNumber + 1; i++)
        {
            buttons[i].interactable = true;
        }
        for (int i = dayPassedNumber + 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

    public void StartLevel()
    {
        if ((PlayerPrefs.GetInt("SelectedLevel") >= 0) && (PlayerPrefs.GetInt("SelectedLevel") < 22))
            SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("GameplayMenu");
    }
}
