using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class BasicAttack : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    bool pointerDown = false;

    void Update()
    {
        if (pointerDown)
        {
            GameObject.Find("Player").GetComponent<Player_Attack>().Shoot();
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }


    public virtual void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }

}
