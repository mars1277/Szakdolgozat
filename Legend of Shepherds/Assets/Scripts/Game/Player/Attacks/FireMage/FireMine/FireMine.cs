using UnityEngine;
using System.Collections;

public class FireMine : MonoBehaviour
{

    public GameObject fireMineSplashGO;

    void Start()
    {
        transform.position = GameObject.Find("Player").transform.position + (new Vector3(0f, 0.135f));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            GameObject splash = (GameObject)Instantiate(fireMineSplashGO);
            splash.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
