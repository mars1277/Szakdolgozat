using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour {

    public GameObject pauseMenuGO;

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Destroy(GameObject.Find("PauseMenuCanvas(Clone)"));
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameplayMenu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameObject pauseMenu = (GameObject)Instantiate(pauseMenuGO);
    }
}
