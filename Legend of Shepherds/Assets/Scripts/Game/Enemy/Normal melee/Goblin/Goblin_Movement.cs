using UnityEngine;
using System.Collections;

public class Goblin_Movement : MonoBehaviour
{
    bool firstAnimationImagePassed;
    float counter;

    Vector3 velocity = Vector3.zero;
    float forwardSpeed = 0.2f;

    void Start()
    {
        firstAnimationImagePassed = false;
        counter = 0f;
    }

    void Update()
    {
        counter += Time.deltaTime;

        if (!firstAnimationImagePassed)
        {
            if (counter >= 0.25f)
            {
                counter = 0f;
                firstAnimationImagePassed = true;
            }
            else
            {
                velocity.y = forwardSpeed * 1.2f;

                transform.position += -velocity * Time.deltaTime;
            }
        }
        else
        {
            if (counter < 0.5f)
            {
                velocity.y = forwardSpeed * 1.2f;
            }
            else
            {
                velocity.y = forwardSpeed * 0.8f;
            }

            if (counter >= 1f)
            {
                counter = 0f;
            }

            transform.position += -velocity * Time.deltaTime;
        }
    }
}
