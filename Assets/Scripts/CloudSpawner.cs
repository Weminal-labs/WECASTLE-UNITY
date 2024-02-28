using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefab;
    public float spawnInterval = 30.0f;

    void Start()
    {
        // Invoke the SpawnCloud method repeatedly with the specified interval
        InvokeRepeating("SpawnCloud", 0f, spawnInterval);
    }

    void SpawnCloud()
    {
            // Calculate a random delay to spawn the cloud within the spawnInterval
            float randomDelay = Random.Range(15.0f, spawnInterval);

            // Invoke the SpawnCloudObject method after the random delay
            Invoke("SpawnCloudObject", randomDelay);
      
    }

    void SpawnCloudObject()
    {
        int randomCloud = Random.Range(0, cloudPrefab.Length);
        // Instantiate a new cloud object at a fixed X position (-30, 0, 0)
        GameObject newCloud = Instantiate(cloudPrefab[randomCloud], new Vector3(-30f, Random.Range(-12.0f, 12.0f), 0), Quaternion.identity);

    }
}
