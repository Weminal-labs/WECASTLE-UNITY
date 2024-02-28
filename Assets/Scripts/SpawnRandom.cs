using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnRandom : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private float secondSpawn = 3f;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Spawn());
    }
    // Update is called once per frame

    IEnumerator Spawn()
    {
        while (true)
        {
            int i = Random.Range(0, 5);
            float x;
            float y;
            for (; i > 0; i--)
            {
                x = Random.Range(minX, maxX);
                y = Random.Range(minY, maxY);
                Instantiate(obj, new Vector3(x, y, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(secondSpawn);
        } 
    }
}
