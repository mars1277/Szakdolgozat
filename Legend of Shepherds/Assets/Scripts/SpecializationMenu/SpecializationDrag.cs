using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SpecializationDrag : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public GameObject specialization;

    public Vector2 specializationStartingPoint;

    Vector2 startingPoint;

    public Vector2 movementVector;
    public Vector2 targetPosition;
    Vector2 changedPosition;

    float specializationMovementValue;

    void Start()
    {
        movementVector = Vector2.zero;
        specializationMovementValue = 200f;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().zoomedIn)
        {
            changedPosition = Input.mousePosition - (Vector3)startingPoint;

            specialization.transform.position = targetPosition + changedPosition;
        }
    }


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().zoomedIn)
        {
            startingPoint = Input.mousePosition;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (GameObject.Find("SpecializationMenu").GetComponent<SpecializationTest>().zoomedIn)
        {
            startingPoint = Vector2.zero;
            targetPosition += changedPosition;

            if(targetPosition.x - specializationStartingPoint.x > specializationMovementValue)
            {
                targetPosition.x = specializationStartingPoint.x + specializationMovementValue;
            }
            if (targetPosition.x - specializationStartingPoint.x < -specializationMovementValue)
            {
                targetPosition.x = specializationStartingPoint.x - specializationMovementValue;
            }
            if (targetPosition.y - specializationStartingPoint.y > specializationMovementValue)
            {
                targetPosition.y = specializationStartingPoint.y + specializationMovementValue;
            }
            if (targetPosition.y - specializationStartingPoint.y < -specializationMovementValue)
            {
                targetPosition.y = specializationStartingPoint.y - specializationMovementValue;
            }
            specialization.transform.position = targetPosition;
        }
    }



}
