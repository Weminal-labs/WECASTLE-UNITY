using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUITree : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;
    private void OnMouseDown()
    {
        ui.SetActive(false);
    }
}
