using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    int curPage;
    Vector3 tarPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform pageRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshould;

    private void Awake()
    {
        curPage = 1;
        tarPos = pageRect.localPosition;
        dragThreshould = 0.5f;
    }

    public void Next()
    {
        if(curPage < maxPage)
        {
            curPage++;
            tarPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (curPage > 1)
        {
            curPage--;
            tarPos -= pageStep;
            MovePage();
        }
    }
    void MovePage()
    {
        pageRect.LeanMoveLocal(tarPos, tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if(eventData.position.x > eventData.pressPosition.x)
            {
                Previous();
            }
            else
            {
                Next();
            }
        }
        else
        {
            MovePage();
        }
    }
}
