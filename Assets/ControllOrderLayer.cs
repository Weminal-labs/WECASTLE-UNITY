using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllOrderLayer : MonoBehaviour
{
    SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        render.sortingOrder = (int)(transform.position.z * -1);
    }
}
