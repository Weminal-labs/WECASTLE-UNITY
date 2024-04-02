using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTreeInList : MonoBehaviour
{
    [SerializeField]
    private GameObject manage,ui,tree;
    private void OnMouseDown()
    {
        manage.GetComponent<ManageTreeList>().addWaitForChop(tree);
        Destroy(ui);
    }
}
