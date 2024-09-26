using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject joyStick;
    public GameObject joyStickBG;
    public Vector2 joyStickVec;
    private Vector2 joyStickTouchPos;
    private Vector2 joyStickOriginalPos;
    private float joyStickRadius;
    // Start is called before the first frame update
    void Start()
    {
        joyStickOriginalPos = joyStickBG.transform.position;
        joyStickRadius = joyStickBG.GetComponent<RectTransform>().sizeDelta.y/4;
    }

    
    public void PointerDown()
    {
        joyStick.transform.position = Input.mousePosition;
        joyStickBG.transform.position = Input.mousePosition;
        joyStickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joyStickVec = (dragPos - joyStickTouchPos).normalized;

        float joyStickDisk = Vector2.Distance(dragPos, joyStickTouchPos);

        if(joyStickDisk < joyStickRadius)
        {
            joyStick.transform.position = joyStickTouchPos + joyStickVec * joyStickVec;
        }
        else
        {
            joyStick.transform.position = joyStickTouchPos + joyStickVec * joyStickRadius;
        }
    }
    public void PointerUp()
    {
        joyStickVec = Vector2.zero;
        joyStick.transform.position = joyStickOriginalPos;
        joyStickBG.transform.position = joyStickOriginalPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
