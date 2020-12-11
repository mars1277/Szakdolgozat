using UnityEngine;
using System.Collections;

public class FireRainDrop : MonoBehaviour {

    public GameObject fireRainSplashGO;

    float speed = 1.2f;
    public float delay;

	public void SetDelay (float d)
    {
        delay = d;
	}
	
	void Update ()
    {
        Vector3 pos = transform.position;
        pos -= new Vector3(speed * Time.deltaTime / 2f, speed * Time.deltaTime * 1.414f / 2f);
        transform.position = pos;
        delay -= Time.deltaTime;
        if(delay < 0f)
        {
            GameObject splash = (GameObject)Instantiate(fireRainSplashGO);
            splash.transform.position = transform.position;
            Destroy(gameObject);
        }

    }
}
