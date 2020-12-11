using UnityEngine;
using System.Collections;

public class SkeletonMage_Movement : MonoBehaviour {

    float counter;

    Vector3 velocity = Vector3.zero;
    float forwardSpeed = 0.1f;

    void Start()
    {
        counter = 0f;
    }

    void Update()
    {
        counter += Time.deltaTime;

        velocity.y = forwardSpeed * 1.2f;

        transform.position += -velocity * Time.deltaTime;
    }
}
