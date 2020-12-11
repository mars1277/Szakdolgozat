using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireRain : MonoBehaviour
{
    public GameObject fireRainDropGO;

    float speed = 1.2f;

    float minDelayTime = 3f;
    float maxDelayTime = 8f;

    struct FireDrop
    {
        public Vector3 destination;
        public Vector3 startingPoint;
        public float delay;
    }

    FireDrop[] fireRain = new FireDrop[23];

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                fireRain[i * 3 + j].destination = new Vector3(-0.9f + j * 0.9f, 2f - i * 0.8f);
                fireRain[i * 3 + j].delay = Random.Range(minDelayTime, maxDelayTime);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                fireRain[i * 2 + j + 15].destination = new Vector3(-0.45f + j * 0.9f, 1.6f - i * 0.8f);
                fireRain[i * 2 + j + 15].delay = Random.Range(minDelayTime, maxDelayTime);
            }
        }

        for (int i = 0; i < 23; i++)
        {
            fireRain[i].startingPoint = fireRain[i].destination + new Vector3(fireRain[i].delay * speed / 2f, fireRain[i].delay * speed * 1.414f / 2f);

            GameObject drop = (GameObject)Instantiate(fireRainDropGO);
            drop.transform.position = fireRain[i].startingPoint;
            drop.GetComponent<FireRainDrop>().SetDelay(fireRain[i].delay);
        }
        Destroy(gameObject);
    }
}
