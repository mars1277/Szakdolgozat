    using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SkillDescriptionSample : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public GameObject skillDescriptionSampleGO;

    GameObject sample;

    public void OnPointerDown(PointerEventData eventData)
    {
        sample = Instantiate(skillDescriptionSampleGO);
        string character = PlayerPrefs.GetString("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Character");
        GameObject.Find("SkillDescriptionSampleText").GetComponent<Text>().text = GameObject.Find("Datas").GetComponent<AllSkillDescriptionSamples>().GetSkillDescriptionSample(int.Parse(gameObject.name.Substring(5, 1)), character);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Destroy(sample);
    }
}
