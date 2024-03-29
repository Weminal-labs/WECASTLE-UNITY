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
    private Animator animator;
    private Blackboard blackboard;
    private BoolVariable isUnderControl ;


    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();
        SelectionSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        blackboard = GetComponent<Blackboard>();
    }
    private void Start()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        SelectionSprite.color = new Color(0.7f, 0.7f, 0.7f);
        isUnderControl = blackboard.GetVariable<BoolVariable>("isUnderControl");

    }

    public void Update()
    {
        if(Agent.remainingDistance == 0)
        {
            isUnderControl.Value = false;
        }

    }

    public void MoveTo(Vector3 Position)
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
        Agent.SetDestination(Position);
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
        //SelectionSprite.gameObject.SetActive(true);
        SelectionSprite.color = new Color(1f, 1f, 1f);

    }

    public void OnDeselected()
    {
        //SelectionSprite.gameObject.SetActive(false);
        SelectionSprite.color = new Color(0.7f, 0.7f, 0.7f);
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
        SelectedUnits.Add(Unit);
        Unit.OnSelected();
    }

    public void Deselect(SelectableUnit Unit)
    {
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