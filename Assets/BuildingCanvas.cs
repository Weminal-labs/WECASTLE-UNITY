using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCanvas : MonoBehaviour
{
    [SerializeField]
    private Button buttonClose;
    [SerializeField]
    private GameObject container;
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
        this.gameObject.SetActive(false);
    }
}
