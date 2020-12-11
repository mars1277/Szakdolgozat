using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject musicGO;

    void Start()
    {
       if (PlayerPrefs.GetInt("Initialized") == 0)
        {
            gameObject.GetComponent<Save_Load>().ClearSlot(1);
            gameObject.GetComponent<Save_Load>().ClearSlot(2);
            gameObject.GetComponent<Save_Load>().ClearSlot(3);
            gameObject.GetComponent<Save_Load>().ClearSlot(4);
            gameObject.GetComponent<Save_Load>().ClearSlot(5);
            gameObject.GetComponent<Save_Load>().ClearSlot(6);
            PlayerPrefs.SetInt("Initialized", 1);
        }

       if(GameObject.Find("Game Music(Clone)") == null)
        {
            DontDestroyOnLoad(Instantiate(musicGO));
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("StartGameMenu");
    }

    public void Options()
    {
        GameObject.Find("CharacterMaker").GetComponent<Save_Load>().MakeCharacter();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
