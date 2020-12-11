using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuButton : MonoBehaviour {

    public GameObject selectedButtonFrame;

    public void LevelSelect()
    {
        int levelNum = int.Parse(gameObject.name.Substring(12, 2));
        PlayerPrefs.SetInt("SelectedLevel", levelNum);
        selectedButtonFrame.gameObject.transform.position = transform.position;
    }
}
