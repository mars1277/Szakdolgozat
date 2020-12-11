using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

private Image backgroundImage;
    private Image joystickImage;

    bool stopped;

    public Vector3 InputDirection { set; get; }

    private void Start()
    {
        stopped = false;
        backgroundImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (!stopped)
        {
            Vector2 pos = Vector2.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
            {
                pos.x = pos.x / backgroundImage.rectTransform.sizeDelta.x;
                pos.y = pos.y / backgroundImage.rectTransform.sizeDelta.y;

                float x = (backgroundImage.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
                float y = (backgroundImage.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

                InputDirection = new Vector3(x, y);
                InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

                joystickImage.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (backgroundImage.rectTransform.sizeDelta.x / 3),
                                                                           InputDirection.y * (backgroundImage.rectTransform.sizeDelta.y / 3));
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public void Stop()
    {
        InputDirection = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
        stopped = true;
    }
}
