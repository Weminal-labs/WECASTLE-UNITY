using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{
    private NavMeshAgent Agent;
    [SerializeField]
    private SpriteRenderer SelectionSprite;
    public SmartOnes smartOnes;
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();
        smartOnes = GetComponent<SmartOnes>();
        SelectionSprite = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        SelectionSprite.color = new Color(0.7f, 0.7f, 0.7f);
    }
    public void MoveTo(Vector3 Position)
    {
        //smartOnes.stateMachine.SetInitialState(new WanderState(smartOnes.stateMachine, smartOnes));

        Agent.SetDestination(Position);

        //smartOnes.stateMachine.EndState();
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