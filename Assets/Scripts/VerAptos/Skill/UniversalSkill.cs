using UnityEngine;
using System.Collections;
using System.Linq;

public class UniversalSkill : MonoBehaviour
{
    [Header("Dark Bolt Settings")]
    [SerializeField] private GameObject darkBoltPrefab;
    [SerializeField] private bool DarkBoltActive = false;
    [SerializeField] private float spawnIntervalDarkBolt = 10f;
    [SerializeField] private int boltsPerSpawn = 3;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnHeight = 0.1f;
    [SerializeField] private float spawnRadiusAroundEnemy = 0.5f;

    [Header("Targeting")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Tornado Settings")]
    [SerializeField] private GameObject tornadoPrefab;
    [SerializeField] private float spawnIntervalTornado = 10f;
    [SerializeField] private Transform spawnPoint;
    private bool TornadoActive = false;
    private void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Ally").transform;
        StartCoroutine(SpawnDarkBolts());
        StartCoroutine(CountDownTornado());
    }
    private void Update() {
        if(GameObject.FindGameObjectWithTag("Ally").GetComponent<HeroStats>().getLevelUpList()[3] > 0 && !DarkBoltActive){
            DarkBoltActive = true;
        }
        if(GameObject.FindGameObjectWithTag("Ally").GetComponent<HeroStats>().getLevelUpList()[4] > 0 && !TornadoActive){
            TornadoActive = true;
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
            if(LevelDarkBolt() > 10){
                spawnIntervalDarkBolt = 1f; 
            }else{
                spawnIntervalDarkBolt = 10f - LevelDarkBolt()*0.5f;
            }
            yield return new WaitForSeconds(spawnIntervalDarkBolt);
            if(DarkBoltActive){

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

    #region Tornado
    private int LevelTornado(){
        return GameObject.FindGameObjectWithTag("Ally").GetComponent<HeroStats>().getLevelUpList()[4]-1;
    }
    private IEnumerator CountDownTornado(){
        while(true){
            if(LevelTornado() > 10){
                spawnIntervalTornado = 1f;
            }else{
                spawnIntervalTornado = 10f - LevelTornado()*0.2f;
            }
            yield return new WaitForSeconds(spawnIntervalTornado);
            if(!PauseGameManager.instance.IsPaused()&&TornadoActive){
                SpawnTornado();
            }
        }
    }
    public void SpawnTornado()
    {
        // Find the player's direction
        TopDownController playerController = GameObject.FindGameObjectWithTag("Ally").GetComponent<TopDownController>();
        Vector3 spawnDirection = playerController.GetDirection();

        // Instantiate the tornado
        GameObject tornadoObject = Instantiate(tornadoPrefab, spawnPoint.position, Quaternion.identity);

        // Get the TornadoController from the instantiated object
        TornadoController tornadoController = tornadoObject.GetComponent<TornadoController>();

        // Set the direction on the instantiated tornado
        if (tornadoController != null)
        {
            tornadoController.SetDirection(spawnDirection);
        }
        else
        {
            Debug.LogError("TornadoController component not found on the instantiated tornado!");
        }
    }
    #endregion
}