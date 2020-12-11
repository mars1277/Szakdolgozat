using UnityEngine;
using System.Collections;


public class Player_Movement : MonoBehaviour
{
    public VirtualJoystick moveJoystick;

    float movementSpeedStraight = 0.02f;
    float movementSpeedDiagonally = 0.014142f;

    public bool Left = false;
    public bool Right = false;
    public bool Down = false;
    public bool Up = false;

    bool W = false;
    bool A = false;
    bool S = false;
    bool D = false;

    private Animator animator;

    public float movementMultiplier;
    public float baseMovementMultiplier;
    public float fairyPowerMovementMultiplier;

    public bool failed = false;

    public void Initialize()
    {
        animator = gameObject.GetComponent<Animator>();
        baseMovementMultiplier = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_MovementSpeed() / 100f + 1f;
        fairyPowerMovementMultiplier = 1f;
    }

    void Update()
    {
        if (failed)
        {
            return;
        }
        movementMultiplier = baseMovementMultiplier * fairyPowerMovementMultiplier;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            A = true;
            Left = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            D = true;
            Right = true;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            W = true;
            Up = true;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            S = true;
            Down = true;
        }


        animator.SetBool("Left", Left);
        animator.SetBool("Right", Right);
        animator.SetBool("Up", Up);
        animator.SetBool("Down", Down);
    }

    void FixedUpdate()
    {
        if (failed)
        {
            return;
        }
        Movement_Keyboard();
        Movement_VirtualJoystick();
    }

    void Movement_VirtualJoystick()
    {
        Vector3 pos = transform.position;


        if (moveJoystick.InputDirection != Vector3.zero)
        {
            Vector3 temp = moveJoystick.InputDirection;
            temp.x *= Mathf.Sqrt(1 / (moveJoystick.InputDirection.x * moveJoystick.InputDirection.x + moveJoystick.InputDirection.y * moveJoystick.InputDirection.y)) * ( 2 * Mathf.Abs(moveJoystick.InputDirection.x) - moveJoystick.InputDirection.x * moveJoystick.InputDirection.x);
            temp.y *= Mathf.Sqrt(1 / (moveJoystick.InputDirection.x * moveJoystick.InputDirection.x + moveJoystick.InputDirection.y * moveJoystick.InputDirection.y)) * ( 2 * Mathf.Abs(moveJoystick.InputDirection.y) - moveJoystick.InputDirection.y * moveJoystick.InputDirection.y);

            if (temp.x <= (-0.92388))
            {
                if (Mathf.Abs(temp.x) > Mathf.Abs(temp.y))
                {
                    Right = false;
                    Up = false;
                    Down = false;
                    Left = true;
                }
            }
            if ((temp.x > (-0.92388)) && (temp.x <= (-0.38268)) && (temp.y > 0))
            {
                if (Mathf.Abs(temp.x) > Mathf.Abs(temp.y))
                {
                    Right = false;
                    Down = false;
                    Up = true;
                    Left = true;
                }
            }
            if ((temp.x > (-0.92388)) && (temp.x <= (-0.38268)) && (temp.y < 0))
            {
                if (Mathf.Abs(temp.x) > Mathf.Abs(temp.y))
                {
                    Right = false;
                    Up = false;
                    Down = true;
                    Left = true;
                }
            }
            if ((temp.x > (-0.38268)) && (temp.x <= 0.38268) && (temp.y > 0))
            {
                if (Mathf.Abs(temp.x) < Mathf.Abs(temp.y))
                {
                    Right = false;
                    Down = false;
                    Left = false;
                    Up = true;
                }
            }
            if ((temp.x > (-0.38268)) && (temp.x <= 0.38268) && (temp.y < 0))
            {
                if (Mathf.Abs(temp.x) < Mathf.Abs(temp.y))
                {
                    Right = false;
                    Up = false;
                    Left = false;
                    Down = true;
                }
            }
            if ((temp.x > 0.38268) && (temp.x <= 0.92388) && (temp.y > 0))
            {
                if (Mathf.Abs(temp.x) < Mathf.Abs(temp.y))
                {
                    Down = false;
                    Left = false;
                    Right = true;
                    Up = true;
                }
            }
            if ((temp.x > 0.38268) && (temp.x <= 0.92388) && (temp.y < 0))
            {
                if (Mathf.Abs(temp.x) < Mathf.Abs(temp.y))
                {
                    Up = false;
                    Left = false;
                    Right = true;
                    Down = true;
                }
            }
            if (temp.x >= 0.92388)
            {
                if (Mathf.Abs(temp.x) > Mathf.Abs(temp.y))
                {
                    Left = false;
                    Up = false;
                    Down = false;
                    Right = true;
                }
            }

            pos += temp * 0.02f * movementMultiplier;
        }
        else
        {
            Right = false;
            Left = false;
            Down = false;
            Up = false;
        }

        transform.position = pos;
    }

    public void IncreaseMovementSpeed(float value)
    {
        fairyPowerMovementMultiplier = value;
    }

    public void DecreaseMovementSpeed()
    {
        fairyPowerMovementMultiplier = 1f;
    }

    void Movement_Keyboard()
    {
        Vector3 pos = transform.position;

        float tempDiag = movementSpeedDiagonally * movementMultiplier;
        float tempStra = movementSpeedStraight * movementMultiplier;

        if (W)
        {
            if (A)
            {
                pos.x -= tempDiag;
                pos.y += tempDiag;
                A = false;
                W = false;
            }
            else
            if (D)
            {
                pos.x += tempDiag;
                pos.y += tempDiag;
                D = false;
                W = false;
            }
            else
            {
                pos.y += tempStra;
                W = false;
            }
        }
        else
        if (S)
        {
            if (A)
            {
                pos.x -= tempDiag;
                pos.y -= tempDiag;
                A = false;
                S = false;
            }
            else
            if (D)
            {
                pos.x += tempDiag;
                pos.y -= tempDiag;
                D = false;
                S = false;
            }
            else
            {
                pos.y -= tempStra;
                S = false;
            }
        }
        else
        if (A)
        {
            pos.x -= tempStra;
            A = false;
        }
        else
        if (D)
        {
            pos.x += tempStra;
            D = false;
        }
        gameObject.transform.position = pos;
    }
}