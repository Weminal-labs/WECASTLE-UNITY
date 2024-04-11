/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    public static List<MoveToMouse> moveAble = new List<MoveToMouse>();
    public float speed;
    private Vector3 target;
    private bool selected;
    // Start is called before the first frame update
    void Start()
    {
        moveAble.Add(this);
        target = transform.position;
        this.GetComponent<SpriteRenderer>().color = new Color(160f / 255f, 160f / 255f, 160f / 255f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)&& selected)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        selected = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);

        foreach (MoveToMouse obj in moveAble)
        {
            if (obj != this)
            {
                obj.selected = false;
                obj.GetComponent<SpriteRenderer>().color = new Color(160f / 255f, 160f / 255f, 160f / 255f);
            }
        }
    } 

}
*/