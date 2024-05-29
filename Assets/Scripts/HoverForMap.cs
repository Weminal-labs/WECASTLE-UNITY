using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverForMap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Info;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Info.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Info.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
