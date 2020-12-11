using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class SkillChooser : MonoBehaviour, IPointerDownHandler
{
    bool active;
    bool locked;

    int skillLevel;
    int skillNumber;

    void Start () {
        string name = gameObject.name;
        skillLevel = int.Parse(name.Substring(6, 1));
        skillNumber = int.Parse(name.Substring(14, 1));
	}

    public bool IsActive()
    {
        return active;
    }

    public bool IsLocked()
    {
        return locked;
    }

    public void SetActive(bool a)
    {
        active = a;
    }

    public void SetLocked(bool l)
    {
        locked = l;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.Find("SkillMenu").GetComponent<SkillMenu>().SetSkillDescription(skillLevel, skillNumber);
        if (GameObject.Find("SkillMenu").GetComponent<SkillMenu>().IsActivatableSkill(skillLevel, skillNumber))
        {
            GameObject.Find("SkillMenu").GetComponent<SkillMenu>().ActivateSkill(skillLevel, skillNumber);
        }
    }

}
