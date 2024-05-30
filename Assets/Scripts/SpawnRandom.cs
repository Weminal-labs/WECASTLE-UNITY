using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandom : MonoBehaviour
{
    [SerializeField] private GameObject[] obj;
    [SerializeField] private float secondSpawn = 3f;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    // Update is called once per frame

    public void Summon()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float stop = 0f;
        while (stop < 3f)
        {
            float x;
            float y;
            int i = Random.Range(0, 2);
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);
            Instantiate(obj[i], new Vector3(x, y, 0), Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);
            stop += secondSpawn;
        } 
    }
}
