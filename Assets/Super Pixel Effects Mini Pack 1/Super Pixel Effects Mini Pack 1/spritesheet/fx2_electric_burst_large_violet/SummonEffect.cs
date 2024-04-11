using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject thunder,gravity;
    public void StartEffect()
    {
        thunder.GetComponent<SpawnRandom>().Summon();
        
        StartCoroutine(Gravity());
    }
    IEnumerator Gravity()
    {
        yield return new WaitForSeconds(0.5f);
        this.gravity.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        this.gravity.GetComponent<Animator>().SetTrigger("End");
        yield return new WaitForSeconds(1f);
        this.gravity.gameObject.SetActive(false);
    }
}
