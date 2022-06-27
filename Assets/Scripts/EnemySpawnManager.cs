using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnLocations = new List<Transform>();
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    
    private Coroutine spawningCoroutine;
    private WaitForSeconds _spawnInterval;
    private int _enemiesToSpawn;

    [SerializeField] private GameManager gameManager;


    private void Start()
    {
        
    }
    private void OnEnable()
    {
        GameManager.GameFinished += StopSpawningEnemies;
        GameManager.GameStarted += StartSpawningEnemies;
    }
    private void OnDisable()
    {
        GameManager.GameFinished -= StopSpawningEnemies;
        GameManager.GameStarted -= StartSpawningEnemies;
    }

    public void SetSpawningParameters(float spawnInterval, int enemiesToSpawn)
    {
        _spawnInterval = new WaitForSeconds(spawnInterval);
        _enemiesToSpawn = enemiesToSpawn;
    }

    public void StartSpawningEnemies()
    {
        if(spawningCoroutine != null)
            StopCoroutine(spawningCoroutine);
                
        spawningCoroutine = StartCoroutine(SpawningCoroutine());
    }
    public void StopSpawningEnemies()
    {
        if (spawningCoroutine != null)
            StopCoroutine(spawningCoroutine);
    }

    IEnumerator SpawningCoroutine()
    {
        int spawnedEnemies = 0;
        while (spawnedEnemies < _enemiesToSpawn)
        {
            Vector3 spawnPos = spawnLocations[Random.Range(0, spawnLocations.Count)].position;
            GameObject enemy  = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], spawnPos, Quaternion.identity, transform);

            enemy.GetComponent<Enemy>().Defeated += gameManager.OnEnemyDeath;

            spawnedEnemies++;
            yield return _spawnInterval;
        }
    }
}
