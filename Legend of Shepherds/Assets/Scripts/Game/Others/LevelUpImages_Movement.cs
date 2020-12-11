using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUpImages_Movement : MonoBehaviour {

    public GameObject levelImage;
    public GameObject upImage;

    Vector3 levelPos;
    Vector3 upPos;

    float speed;
    float currentTime;

    float screenHeight;
    bool fadeOut;

	void Start () {
        speed = 8.0f * Screen.height / 475f;
        levelPos = levelImage.transform.position;
        upPos = upImage.transform.position;
        screenHeight = Screen.height;
        screenHeight *= (5 / 6f);
        gameObject.transform.localScale = new Vector3(Screen.height / 475f, Screen.height / 475f);
    }

    void Update() {
        currentTime += Time.deltaTime;

        if (fadeOut)
        {
            levelImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1.5F - currentTime);
            upImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1.5F - currentTime);
            if (levelImage.GetComponent<Image>().color.a <= 0)
                Destroy(GameObject.Find("LevelUpCanvas(Clone)"));
        }
    }

    void FixedUpdate()
    {
        if (levelPos.y < screenHeight)
        {
            levelPos.y += speed;
            levelImage.transform.position = levelPos;
        }

        if (currentTime > 0.9f)
            if (upPos.y < screenHeight - 60f * Screen.height / 500f)
            {
                upPos.y += speed;
                upImage.transform.position = upPos;
            }
            else if (!fadeOut)
            {
                fadeOut = true;
                currentTime = 0f;
            }
    }
}
