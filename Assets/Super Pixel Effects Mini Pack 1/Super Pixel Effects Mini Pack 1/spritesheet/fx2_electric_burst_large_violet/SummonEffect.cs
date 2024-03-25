using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject anim;
    public void StartEffect()
    {
        anim.GetComponent<Animator>().SetTrigger("Play");
    }
}
