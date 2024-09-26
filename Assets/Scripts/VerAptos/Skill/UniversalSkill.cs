using UnityEngine;
using System.Collections;
using System.Linq;

public class UniversalSkill : MonoBehaviour
{
    [Header("Dark Bolt Settings")]
    [SerializeField] private GameObject darkBoltPrefab;
    [SerializeField] private bool DarkBoltActive = false;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private int boltsPerSpawn = 3;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnHeight = 0.1f;
    [SerializeField] private float spawnRadiusAroundEnemy = 0.5f;

    [Header("Targeting")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask enemyLayer;

    private void Start()
    {
        StartCoroutine(SpawnDarkBolts());
    }
    private void Update() {
        if(GameObject.FindGameObjectWithTag("Ally").GetComponent<HeroStats>().getLevelUpList()[3] > 0 && !DarkBoltActive){
            DarkBoltActive = true;
        }
    }
    #region Dark Bolt
    private int LevelDarkBolt(){
        return GameObject.FindGameObjectWithTag("Ally").GetComponent<HeroStats>().getLevelUpList()[3];
    }

    private IEnumerator SpawnDarkBolts()
    {
        while (true)
        {
            spawnInterval = 10f - LevelDarkBolt()*0.5f;
            yield return new WaitForSeconds(spawnInterval);
            if(!PauseGameManager.instance.IsPaused() && DarkBoltActive){

                Transform closestEnemy = FindClosestEnemy();

                if (closestEnemy != null)
                {
                    for (int i = 0; i < boltsPerSpawn*LevelDarkBolt(); i++)
                    {
                        Vector3 spawnPosition = GetSpawnPositionAroundEnemy(closestEnemy.position);
                        SpawnDarkBolt(spawnPosition, closestEnemy.position);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }
        }
    }

    private Vector3 GetSpawnPositionAroundEnemy(Vector3 enemyPosition)
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadiusAroundEnemy;
        Vector3 spawnOffset = new Vector3(randomCircle.x, spawnHeight, randomCircle.y);
        return enemyPosition + spawnOffset;
    }

    private void SpawnDarkBolt(Vector3 spawnPosition, Vector3 targetPosition)
    {
        if (darkBoltPrefab != null)
        {
            GameObject darkBolt = Instantiate(darkBoltPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Dark Bolt prefab is not set!");
        }
    }

    private Transform FindClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        
        return hitColliders
            .OrderBy(c => Vector3.Distance(transform.position, c.transform.position))
            .FirstOrDefault()?.transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    #endregion
}