using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject[] cloudPrefab; // Reference to the asteroid prefab
    public float initialDelay = 1f; // Initial delay before the first wave
    public int waveSize = 5; // Number of asteroids to spawn in each wave
    public float spawnDelay = 2f; // Delay before spawning the next asteroid in a wave
    public float waveDelay = 5f; // Delay before starting the next wave
    public Vector2 spawnArea = new Vector2(5f, 5f); // X position range for spawning

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true) // Infinite loop to keep spawning waves indefinitely
        {
            for (int i = 0; i < waveSize; i++)
            {
                SpawnAsteroid();
                yield return new WaitForSeconds(spawnDelay);
            }

            yield return new WaitForSeconds(waveDelay);
        }
    }

    private void SpawnAsteroid()
    {
        // Generate a random X position within the spawn area
        float spawnX = Random.Range(-spawnArea.x, spawnArea.x);

        // Calculate the Z position outside the camera view
        float spawnZ = Camera.main.transform.position.z + Camera.main.orthographicSize * 4f;

        // Create the asteroid at the calculated position
        Vector3 spawnPosition = new Vector3(spawnX, 0f, spawnZ);
         int i = Random.Range(0, 5);
        Instantiate(cloudPrefab[i], spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the spawn area wireframe in the Unity editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea.x * 2f, 0f, spawnArea.y * 2f));
    }
}
