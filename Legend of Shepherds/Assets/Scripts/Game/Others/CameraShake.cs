using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public float duration = 0.1f;
    public Vector2 shakeSize = new Vector2(0.001f, 0.001f);

    Vector2 shake = Vector2.zero;

    float currentTime;

    bool doShake = false;

    void Start()
    {

    }

    void Update()
    {
        if (doShake)
        {
            currentTime += Time.deltaTime;




        }
    }

    public void DoShake()
    {
        currentTime = 0f;
        doShake = true;
    }




}
