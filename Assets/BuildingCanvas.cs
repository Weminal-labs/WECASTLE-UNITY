using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCanvas : MonoBehaviour
{
    [SerializeField]
    private Button buttonClose;
    [SerializeField]
    private GameObject container, timeText;
    // Start is called before the first frame update
    void Start()
    {
        if (buttonClose != null)
        {
            buttonClose.onClick.AddListener(Close);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Close()
    {
        container.SetActive(false);
        timeText.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
