using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public List<Transform> dropPoints;
    public List<Transform> destinations;
    public List<GameObject> prefabsToInstantiate;

    void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (dropPoints.Count == 0 || destinations.Count == 0 || prefabsToInstantiate.Count == 0)
        {
            Debug.LogError("Drop points, destinations, or prefabs to instantiate is not set.");
            return;
        }

        StartCoroutine(SpawnPrefabAfterDelay(1f));
        StartCoroutine(SpawnPrefabAfterDelay(3f));

        StartCoroutine(SpawnPrefabAfterDelay(10f));
        StartCoroutine(SpawnPrefabAfterDelay(12f));
    }

    IEnumerator SpawnPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Randomly select drop point and destination
        Transform randomDropPoint = GetRandomDropPoint();
        Transform randomDestination = destinations[Random.Range(0, destinations.Count)];

        // Calculate the angle between randomDropPoint and randomDestination
        Vector3 direction = randomDestination.position - randomDropPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Determine which prefab to instantiate based on angle
        int index = Mathf.FloorToInt(angle / 360 * prefabsToInstantiate.Count);
        index = Mathf.Clamp(index, 0, prefabsToInstantiate.Count - 1);

        // Instantiate prefab at drop point
        GameObject instantiatedPrefab = Instantiate(prefabsToInstantiate[index], randomDropPoint.position, Quaternion.identity);

        // Move prefab to destination
        instantiatedPrefab.GetComponent<BoatController>().destination = randomDestination;
    }

    Transform GetRandomDropPoint()
    {
        // Find the distances between spawn point and drop points
        var distances = new Dictionary<Transform, float>();
        foreach (Transform dropPoint in dropPoints)
        {
            float distance = Vector3.Distance(transform.position, dropPoint.position);
            distances.Add(dropPoint, distance);
        }

        // Sort distances dictionary by value (distance)
        var sortedDistances = distances.OrderBy(pair => pair.Value);

        // Take the three closest drop points
        List<Transform> closestDropPoints = sortedDistances.Select(pair => pair.Key).Take(3).ToList();

        // Randomly select drop point from the closest ones
        return closestDropPoints[Random.Range(0, closestDropPoints.Count)];
    }

    void Update()
    {

    }
}
