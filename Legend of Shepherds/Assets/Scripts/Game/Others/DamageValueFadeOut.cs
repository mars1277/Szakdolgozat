using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageValueFadeOut : MonoBehaviour {

    void Start()
    {
        if (gameObject.name.Equals("DamageValueText"))
        {
            StartCoroutine(DamageValueFadeOutTick());
        }
        if (gameObject.name.Equals("DamageValueCanvas(Clone)"))
        {
            StartCoroutine(DestroyCanvas());
        }
    }

    public IEnumerator DamageValueFadeOutTick()
    {
        int time = 40;
        int timeCounter = 0;
        while (timeCounter < time)
        {
            yield return new WaitForSeconds(0.01f);
            timeCounter++;

            gameObject.transform.position += new Vector3(0f, 0.2f, 0f);

            if (timeCounter > 20)
            {
                gameObject.GetComponent<Text>().color -= new Color(0f, 0f, 0f, 0.05f);
            }
        }
        Destroy(gameObject);
    }

    public IEnumerator DestroyCanvas()
    {
        int time = 2;
        int timeCounter = 0;
        while (timeCounter < time)
        {
            yield return new WaitForSeconds(1f);
            timeCounter++;
        }
        Destroy(gameObject);
    }
}
