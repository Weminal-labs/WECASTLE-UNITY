using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTreeList : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> list, waitForChop,listPawn,pawnInOrder, inChop;

    // Start is called before the first frame update
    void Start()
    {
        list = new List<GameObject>();
        foreach (GameObject tree in GameObject.FindGameObjectsWithTag("Tree"))
        {
            list.Add(tree);
        }
        listPawn = new List<GameObject>();
        findAllPawn();
        waitForChop = new List<GameObject>();
        pawnInOrder = new List<GameObject>();
        inChop = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(waitForChop.Count > 0)
        {
            if (listPawn.Count > 0)
            {
                for (int i = listPawn.Count-1; i>=0; i--)
                {
                    listPawn[i].GetComponent<Blackboard>().GetVariable<TransformVariable>("target").Value = waitForChop[i].transform;
                    inChop.Add(waitForChop[i]);
                    pawnInOrder.Add(listPawn[i]);
                }
            }
        }
    }
    public void addWaitForChop(GameObject obj)
    {
        list.Remove(obj);
        waitForChop.Add(obj);
    }
    public void findAllPawn()
    {
        listPawn.Clear();
        foreach (GameObject pawn in GameObject.FindGameObjectsWithTag("Mob"))
        {
            if(pawn.GetComponent<MobStatus>().getType() == 2)
            {
                listPawn.Add(pawn);
            }
        }
    }
}
