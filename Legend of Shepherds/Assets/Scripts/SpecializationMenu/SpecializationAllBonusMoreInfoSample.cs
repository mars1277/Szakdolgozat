using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SpecializationAllBonusMoreInfoSample : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public GameObject allSpecBonusesDescriptionSampleGO;

    GameObject sample;

    public void OnPointerDown(PointerEventData eventData)
    {
        sample = Instantiate(allSpecBonusesDescriptionSampleGO);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Destroy(sample);
    }
}

