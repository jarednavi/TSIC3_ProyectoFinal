using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public delegate void JoystickAction(Vector2 joystickAxes);
    public static event JoystickAction JoystickMoved;
    public Vector2 joystickAxes;
    private bool usingJoystick;
    private Image bgImg;
    private Image stickImg;
    private RectTransform bgTransform;
    private RectTransform stickTransform;
    public virtual void OnPointerDown(PointerEventData ped)
    {
        usingJoystick = true;
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        usingJoystick = false;
        joystickAxes = stickImg.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        if (JoystickMoved != null) JoystickMoved(Vector2.zero);
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 rectPos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                bgImg.rectTransform,
                ped.position,
                ped.enterEventCamera,
                out rectPos))
        {
            Vector2 clampedPos = GetClampedPosition(rectPos);
            joystickAxes = new Vector2(
                clampedPos.x / (bgTransform.rect.width / 2f),
                clampedPos.y / (bgTransform.rect.height / 2f));
            if (joystickAxes.magnitude > 1f)
            {
                stickImg.GetComponent<RectTransform>().anchoredPosition = clampedPos.normalized * stickTransform.rect.width;
            }
            else
            {
                stickImg.GetComponent<RectTransform>().anchoredPosition = clampedPos;
            }
        }
    }
    private Vector2 GetClampedPosition(Vector2 pos)
    {
        Vector2 bgMin = bgTransform.rect.min;
        Vector2 bgMax = bgTransform.rect.max;
        return new Vector2(
            Mathf.Clamp(pos.x, bgMin.x, bgMax.x),
            Mathf.Clamp(pos.y, bgMin.y, bgMax.y));
    }
    void Start()
    {
        joystickAxes = new Vector2();
        bgImg = GetComponent<Image>();
        stickImg = transform.GetChild(0).GetComponent<Image>();
        bgTransform = gameObject.GetComponent<RectTransform>();
        stickTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }
    void Update()
    {
        if (usingJoystick && JoystickMoved != null)
        {
            JoystickMoved(joystickAxes);
        }
    }
}