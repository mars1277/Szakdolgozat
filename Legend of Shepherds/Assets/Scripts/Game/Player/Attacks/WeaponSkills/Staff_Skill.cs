using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff_Skill : MonoBehaviour {

    public GameObject staffSkillSplashGO;
    float speed = 1.5f;

    void Start()
    {
        GameObject shootPosition = GameObject.Find("ShootPosition");
        transform.position = shootPosition.transform.position;
    }

    void Update()
    {
        Vector2 pos = transform.position;

        pos = new Vector2(pos.x, pos.y + speed * Time.deltaTime);

        transform.position = pos;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            GameObject splash = (GameObject)Instantiate(staffSkillSplashGO);
            splash.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
