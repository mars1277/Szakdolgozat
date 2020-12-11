using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Active_Dragable_DragArea : MonoBehaviour, IPointerDownHandler
{
    public GameObject skill;

    public GameObject margin;

    bool started = false;

    Vector3 startingPoint;
    Vector3 changedPosition;
    Vector3 skillUsedPosition;

    Touch dragTouch;
    bool touched;

    void Start()
    {
        touched = false;
    }

    void Update()
    {
        if(touched && (Input.touchCount > 0))
        {
            dragTouch = Input.touches[Input.touches.Length - 1];

            if(dragTouch.phase == TouchPhase.Began)
            {
                Debug.Log("touchBegan");
                if (skill.GetComponent<Active_Dragable>().SkillReady())
                {
                    Debug.Log("skillBegan");
                    startingPoint = dragTouch.position;
                    margin.GetComponent<Image>().sprite = skill.GetComponent<Active_Dragable>().GetMarginSprite();
                    margin.transform.position = startingPoint;
                    started = true;
                }
            }
            else
            if(dragTouch.phase == TouchPhase.Moved)
            {
                Debug.Log("touchMoved");
                if (started)
                {
                    Debug.Log("skillMoved");
                    margin.transform.position = dragTouch.position;
                }
            }
            else
            if (dragTouch.phase == TouchPhase.Ended)
            {
                Debug.Log("touchEnded");
                if (started)
                {
                    Debug.Log("skillEnded");
                    touched = false;
                    started = false;
                    skillUsedPosition = dragTouch.position;
                    margin.transform.position = new Vector3(-1000f, -1000f);
                    skillUsedPosition = new Vector3(skillUsedPosition.x / Screen.width * 2.88f - 1.44f, skillUsedPosition.y / Screen.height * 5.12f - 2.5f); //2.5f helyett 2.56 kellene lehet csak a firewall miatt jo
                    skill.GetComponent<Active_Dragable>().SkillUsed();
                }
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointerDown");
        touched = true;   
    }

    public Vector3 GetSkillUsedPosition()
    {
        return skillUsedPosition;
    }

}
