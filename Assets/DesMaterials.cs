using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesMaterials : MonoBehaviour
{
    [SerializeField]
    private int gold,meat,wood;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControl = GameObject.FindGameObjectWithTag("GameController");
        gameControl.GetComponent<GameController>().setMaterials(gold,meat,wood);
        StartCoroutine(destroyThis());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destroyThis()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
