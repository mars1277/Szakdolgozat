using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gold : MonoBehaviour {

    public int goldAmount;

    float lifeTime;
    float size;

    void Start()
    {
        lifeTime = 0f;
        size = 0.05f * 2f;
    }

    void Update()
    {
        lifeTime += Time.deltaTime;
        if ((lifeTime > 6.5f) && (lifeTime < 7f))
        {
            gameObject.transform.localScale = new Vector3(size * (7f - lifeTime), size * (7f - lifeTime));
        }
        else if ((lifeTime > 7f) && (lifeTime < 7.5f))
        {
            gameObject.transform.localScale = new Vector3(size * (lifeTime - 7f), size * (lifeTime - 7f));
        }
        else if ((lifeTime > 7.5f) && (lifeTime < 8f))
        {
            gameObject.transform.localScale = new Vector3(size * (8 - lifeTime), size * (8f - lifeTime));
        }
        else if ((lifeTime > 8f) && (lifeTime < 8.5f))
        {
            gameObject.transform.localScale = new Vector3(size * (lifeTime - 8f), size * (lifeTime - 8f));
        }
        else if ((lifeTime > 8.5f) && (lifeTime < 9f))
        {
            gameObject.transform.localScale = new Vector3(size * (9f - lifeTime), size * (9f - lifeTime));
        }
        else if ((lifeTime > 9f) && (lifeTime < 9.5f))
        {
            gameObject.transform.localScale = new Vector3(size * (lifeTime - 9f), size * (lifeTime - 9f));
        }
        else if ((lifeTime > 9.5f) && (lifeTime < 10f))
        {
            gameObject.transform.localScale = new Vector3(size * (10f - lifeTime), size * (10f - lifeTime));
        }
        else if (lifeTime > 10f)
        {
            DestroyObject();
        }
    }

    public int GetGold()
    {
        return goldAmount;
    }

    public void SetGold(int g)
    {
        goldAmount = g;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
