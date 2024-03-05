/*using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.AI;

public class MoveToMouse : MonoBehaviour
{
    public static List<MoveToMouse> moveAble = new List<MoveToMouse>();
    private Vector3 target;
    private bool selected;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        moveAble.Add(this);
        target = transform.position;
        agent = GetComponent<NavMeshAgent>();
        this.GetComponent<SpriteRenderer>().color = new Color(160f / 255f, 160f / 255f, 160f / 255f);
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(selected);
        if (Input.GetMouseButtonDown(1) && selected)
        {

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            agent.SetDestination(new Vector3(target.x,target.y,transform.position.z));

        }
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

}*/