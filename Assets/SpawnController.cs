using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Transform spawnPoints;
    public List<Transform> destinations;
    public List<GameObject> prefabsToInstantiate;

    void Start()
    {

    }

    public void SpawnEnemy(int i)
    {
        if (spawnPoints == null || destinations.Count == 0 || prefabsToInstantiate.Count == 0)
        {
            Debug.LogError("Spawn points, destinations, or prefabs to instantiate is not set.");
            return;
        }

        for (int j = 0; j <= i; j++)
        {
            StartCoroutine(SpawnPrefabAfterDelay(1f, false));
            StartCoroutine(SpawnPrefabAfterDelay(5f, false));
        }
    }

    public void SpawnBoss()
    {
        if (spawnPoints == null || destinations.Count == 0 || prefabsToInstantiate.Count == 0)
        {
            Debug.LogError("Spawn points, destinations, or prefabs to instantiate is not set.");
            return;
        }


        StartCoroutine(SpawnPrefabAfterDelay(5f, true));

    }

    IEnumerator SpawnPrefabAfterDelay(float delay, bool isBoss)
    {
        yield return new WaitForSeconds(delay);

        Transform randomDestination = destinations[Random.Range(0, destinations.Count)];

        // Calculate the angle between randomDropPoint and randomDestination
        Vector3 direction = randomDestination.position - spawnPoints.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Determine which prefab to instantiate based on angle
        int index = 0;
        if ((angle > 135 && angle <= 180) || (angle >= -180 && angle < -90)) // left
        {
            index = 0; // replace with the index of the prefab for left direction
        }
        else if (angle > 90 && angle <= 135) // up-left
        {
            index = 1; // replace with the index of the prefab for up-left direction
        }
        else
        {
            Debug.LogError("Invalid direction.");
            yield break;
        }

        GameObject instantiatedPrefab;
        if (isBoss)
        {
            instantiatedPrefab = Instantiate(prefabsToInstantiate[index + 2], spawnPoints.position, Quaternion.identity);

        }
        else
        {
            instantiatedPrefab = Instantiate(prefabsToInstantiate[index], spawnPoints.position, Quaternion.identity);
        }

        // Move prefab to destination
        instantiatedPrefab.GetComponent<BoatController>().destination = randomDestination;
    }


    void Update()
    {

    }
}
