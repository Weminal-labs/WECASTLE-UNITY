using System.Collections;
using UnityEngine;

public class TreeChunk : MonoBehaviour
{
    [SerializeField] private GameObject treeChunkPrefab;
    [SerializeField] private int delay;

    void Start()
    {
        StartCoroutine(SpawnPrefabAfterDelay(delay));
    }

    IEnumerator SpawnPrefabAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(treeChunkPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
