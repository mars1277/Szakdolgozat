using UnityEngine;
using System.Collections;

public class SkeletonMageBoss_Movement : MonoBehaviour {

    Vector3 velocity = Vector3.zero;
    float forwardSpeed = 0.1f;
    bool forwardMovementDone;
    float sidewardSpeed = 0.1f;
    bool stoppedToCastBomb;
    float bombCastingCounter;
    int sidewardMovementCounter;
    bool left;

    void Start()
    {
        forwardMovementDone = false;
        stoppedToCastBomb = false;
        bombCastingCounter = 0f;
        sidewardMovementCounter = 0;
        if (Random.Range(0f, 1f) < 0.5f)
            left = true;
        else
            left = false;
    }

    void Update()
    {

        if (forwardMovementDone)
        {
            if (!stoppedToCastBomb)
            {
                if (sidewardMovementCounter < 5)
                {
                    if (left)
                    {
                        velocity.x = -sidewardSpeed * 1.2f;
                        if (transform.position.x < -0.5f)
                        {
                            left = false;
                            sidewardMovementCounter++;
                        }
                    }
                    else
                    {
                        velocity.x = sidewardSpeed * 1.2f;
                        if (transform.position.x > 0.5f)
                        {
                            left = true;
                            sidewardMovementCounter++;
                        }
                    }
                }
                else
                {
                    if (left)
                    {
                        velocity.x = -sidewardSpeed * 1.2f;
                        if (transform.position.x <= 0f)
                        {
                            velocity.x = 0f;
                            stoppedToCastBomb = true;
                            bombCastingCounter = 0f;
                            sidewardMovementCounter = 0;
                        }
                    }
                    else
                    {
                        velocity.x = sidewardSpeed * 1.2f;
                        if (transform.position.x >= 0f)
                        {
                            velocity.x = 0f;
                            stoppedToCastBomb = true;
                            bombCastingCounter = 0f;
                            sidewardMovementCounter = 0;
                        }
                    }
                }
            }
            else
            {
                bombCastingCounter += Time.deltaTime;
                if (bombCastingCounter >= 5f)
                {
                    stoppedToCastBomb = false;
                }
            }
        }
        else
        {
            velocity.y = -forwardSpeed * 1.2f;
            if (transform.position.y < 1.35)
            {
                forwardMovementDone = true;
                velocity.y = 0f;
            }
        }
        transform.position += velocity * Time.deltaTime;
    }

    public bool ForwardMovementDone()
    {
        return forwardMovementDone;
    }

    public bool StoppedToCastBomb()
    {
        return stoppedToCastBomb;
    }
}
