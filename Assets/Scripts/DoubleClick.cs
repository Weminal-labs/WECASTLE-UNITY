using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClick : MonoBehaviour
{
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;
    public bool isDoubleClick = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            leftClickNum += 1;
        }
        if(leftClickNum == 1 && isTimeCheckAllowed)
        {
            firstLeftClickTime = Time.time;
            StartCoroutine(DetectDoubleClick());
            isDoubleClick = false;
        }
    }

    IEnumerator DetectDoubleClick()
    {
        isTimeCheckAllowed = false;
        while(Time.time < firstLeftClickTime + timeBetweenLeftClick)
        {
            if(leftClickNum == 2)
            {
                isDoubleClick = true;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        leftClickNum = 0;
        isTimeCheckAllowed = true;
        isDoubleClick = false;
    }
}
