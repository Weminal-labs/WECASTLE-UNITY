using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefab;
    public GameObject cloud;
    public float spawnInterval = 30.0f;
    public float speed = 10.0f;

    void Start()
    {
        // Invoke the SpawnCloud method repeatedly with the specified interval
        InvokeRepeating("SpawnCloud", 0f, spawnInterval);
    }

    void SpawnCloud()
    {
            // Calculate a random delay to spawn the cloud within the spawnInterval
            float randomDelay = Random.Range(1.0f, spawnInterval);

            // Invoke the SpawnCloudObject method after the random delay
            Invoke("SpawnCloudObject", randomDelay);
      
    }

    void SpawnCloudObject()
    {
        int randomCloud = Random.Range(0, cloudPrefab.Length);
        GameObject cloud = cloudPrefab[randomCloud];
        cloud.GetComponent<CloudMovement>().speed = this.speed;
        // Instantiate a new cloud object at a fixed X position (-30, 0, 0)
        Instantiate(cloud, new Vector3(-60f*this.cloud.transform.localScale.x, Random.Range(-30.0f,50.0f), 0), Quaternion.identity);
    }
}
