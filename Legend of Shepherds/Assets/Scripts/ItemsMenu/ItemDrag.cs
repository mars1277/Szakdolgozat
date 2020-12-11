using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemDrag : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public GameObject item;

    public Vector2 itemStartingPoint;

    Vector2 startingPoint;
    public Vector2 startingPosition;
    Vector2 changedPosition;

    public GameObject startingSlot;

    void Start()
    {
        startingPosition = gameObject.transform.position;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        changedPosition = Input.mousePosition - (Vector3)startingPoint;
        item.transform.position = startingPoint + changedPosition;
    }


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        startingPoint = Input.mousePosition;
        GameObject.Find("Datas").GetComponent<ItemController>().HitRaycast();
        startingSlot = GameObject.Find("Datas").GetComponent<ItemController>().coveredSlot;
        GameObject.Find("Datas").GetComponent<ItemController>().SetItemSelected(startingSlot);
        startingPosition = gameObject.transform.position;
        gameObject.transform.parent.transform.parent.GetComponent<Canvas>().sortingOrder = 2;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        GameObject coveredSlot = GameObject.Find("Datas").GetComponent<ItemController>().coveredSlot;
        if (coveredSlot != null)
        {
            GameObject.Find("Datas").GetComponent<ItemController>().PlaceItem(startingSlot.name, coveredSlot.name);          
        }
        else
        {
            item.transform.position = startingPosition;
        }
        startingPoint = Vector2.zero;
        changedPosition = Vector2.zero;
        gameObject.transform.parent.transform.parent.GetComponent<Canvas>().sortingOrder = 1;
    }

    public void DeleteItem()
    {
        Destroy(gameObject.transform.parent.transform.parent.gameObject);
    }

    public void SetStartingPosition(Vector2 startpos)
    {
        startingPosition = startpos;
    }

    public Vector2 GetStartingPosition()
    {
        return startingPosition;
    }
}
