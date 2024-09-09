using System.Collections;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Transform destination;
    public float speed = 1f;

    private bool reachedDestination = false;
    [SerializeField] private GameObject[] enemy;

    [SerializeField] private GameObject[] boss;

    public bool isBoss = false;
    // Update is called once per frame
    void Update()
    {
        if (!reachedDestination)
        {
            // Calculate the direction towards the destination
            Vector2 direction = (Vector2)destination.position - (Vector2)transform.position;

            // Check if the object is close enough to the destination
            if (direction.magnitude <= 0.1f)
            {
                reachedDestination = true;
                StartCoroutine(DestroyAfterDelay());
            }
            else
            {
                // Normalize the direction to have a magnitude of 1
                direction.Normalize();

                // Move the object towards the destination
                transform.Translate(direction * speed * Time.deltaTime);
            }
        }
    }

    IEnumerator DestroyAfterDelay()
    {

        if (isBoss)
        {
            InstantiateNewPrefabBoss();
        }
        else
        {
            // Instantiate a new prefab at the current position
            //InstantiateNewPrefab();
            InstantiateNewPrefab();
        }

        // Wait for 1 second
        yield return new WaitForSeconds(1f);


        // Destroy the current game object
        Destroy(gameObject);
    }

    void InstantiateNewPrefab()
    {
        // Define a random offset range
        float offsetX = Random.Range(-1f, 1f);
        float offsetY = Random.Range(-1f, 1f);

        GameObject enemyRandom;

        if (StaticLobbySend.numMap == 3 && GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().waveCount == 4)
        {
            enemyRandom = enemy[1];
        }
        else if (StaticLobbySend.numMap != 0)
        {
            enemyRandom = enemy[Random.Range(0, enemy.Length)];
        }
        else
        {
            enemyRandom = enemy[0];
        }

        // Calculate the new position with the offset
        Vector3 newPosition = transform.position + new Vector3(offsetX, offsetY, 0f);
        // Instantiate the new prefab at the new position
        Instantiate(enemyRandom, newPosition, Quaternion.identity);
    }
    void InstantiateNewPrefabBoss()
    {
        // Define a random offset range
        float offsetX = Random.Range(-1f, 1f);
        float offsetY = Random.Range(-1f, 1f);


        GameObject enemyRandom = boss[StaticLobbySend.numMap];
        // Calculate the new position with the offset
        Vector3 newPosition = transform.position + new Vector3(offsetX, offsetY, 0f);
        // Instantiate the new prefab at the new position
        Instantiate(enemyRandom, newPosition, Quaternion.identity);
    }

}
