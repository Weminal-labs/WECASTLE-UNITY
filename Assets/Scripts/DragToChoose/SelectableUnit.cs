using MBT;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{
    private NavMeshAgent Agent;
    [SerializeField]
    private SpriteRenderer SelectionSprite;
    private Blackboard blackboard;
    private BoolVariable isUnderControl ;
    private TransformVariable pointToMove;

    public void SetIsUnderControl(bool value)
    {
        isUnderControl.Value = value;
    }

    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Blackboard>();
    }
    private void Start()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        isUnderControl = blackboard.GetVariable<BoolVariable>("isUnderControl");
        pointToMove = blackboard.GetVariable<TransformVariable>("pointToMove");

    }


    /*    public void MoveTo(Vector3 Position)
        {

            isUnderControl.Value = true;
            if (transform.position.x >= Position.x)
            {
                FlipLeft();
            }
            else if(transform.position.x < Position.x)
            {
                FlipRight();
            }
            //pointToMove.Value = Position;
            Agent.SetDestination(Position);
            pointToMove.Value = 
        }*/

    public void MoveTo(GameObject Position)
    {

        if (transform.position.x >= Position.transform.position.x)
        {
            FlipLeft();
        }
        else if (transform.position.x < Position.transform.position.x)
        {
            FlipRight();
        }
        //pointToMove.Value = Position;
        pointToMove.Value = Position.transform;
    }

    public void FlipLeft()
    {
        gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.y, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }

    public void FlipRight()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.y, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }

    public void OnSelected()
    {
        SelectionSprite.gameObject.SetActive(true);

    }

    public void OnDeselected()
    {
        SelectionSprite.gameObject.SetActive(false);
    }
}


public class SelectionManager
{
    private static SelectionManager _instance;
    public static SelectionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SelectionManager();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    public HashSet<SelectableUnit> SelectedUnits = new HashSet<SelectableUnit>();
    public List<SelectableUnit> AvailableUnits = new List<SelectableUnit>();

    private SelectionManager() { }

    public void Select(SelectableUnit Unit)
    {
        Unit.SetIsUnderControl(true);

        SelectedUnits.Add(Unit);
        Unit.OnSelected();
    }

    public void Deselect(SelectableUnit Unit)
    {
        Unit.SetIsUnderControl(false);

        Unit.OnDeselected();
        SelectedUnits.Remove(Unit);
    }

    public void DeselectAll()
    {
        foreach (SelectableUnit unit in SelectedUnits)
        {
            unit.OnDeselected();
        }
        SelectedUnits.Clear();
    }

    public bool IsSelected(SelectableUnit Unit)
    {
        return SelectedUnits.Contains(Unit);
    }

}