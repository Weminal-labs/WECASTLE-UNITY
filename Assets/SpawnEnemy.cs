using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs; // Array to hold the enemy prefabs

    [SerializeField]
    private float spawnInterval = 5.0f; // Time interval between spawns

    [SerializeField]
    private BoxCollider[] spawnArea; // BoxCollider to define the spawn area

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn enemies at regular intervals
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Randomly select an enemy prefab
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[randomIndex];

            // Get a random position within the BoxCollider
            int randomSpawn = Random.Range(0, spawnArea.Length);

            Vector3 spawnPosition = GetRandomPositionInBoxCollider(spawnArea[randomSpawn]);

            // Instantiate the enemy at the random position
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    // Method to get a random position within a BoxCollider
    private Vector3 GetRandomPositionInBoxCollider(BoxCollider boxCollider)
    {
        Vector3 center = boxCollider.center;
        Vector3 size = boxCollider.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);

        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        // Convert local position to world position
        return boxCollider.transform.TransformPoint(randomPosition);
    }
}

