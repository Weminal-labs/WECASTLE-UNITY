using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SummonOpen : MonoBehaviour
{
    [SerializeField]
    public GameObject summonCanvas;
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;
    public bool isDoubleClick = false;

    private void OnMouseUp()
    {
        leftClickNum += 1;
        if (leftClickNum == 1 && isTimeCheckAllowed)
        {
            firstLeftClickTime = Time.time;
            StartCoroutine(DetectDoubleClick());
            isDoubleClick = false;
        }
    }
    IEnumerator DetectDoubleClick()
    {
        isTimeCheckAllowed = false;
        while (Time.time < firstLeftClickTime + timeBetweenLeftClick)
        {
            if (leftClickNum == 2)
            {
                summonCanvas.SetActive(true);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        leftClickNum = 0;
        isTimeCheckAllowed = true;
    }
}
