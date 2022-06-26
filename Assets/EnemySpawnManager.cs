using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnLocations = new List<Transform>();
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();

    private Coroutine spawningCoroutine;
    private WaitForSeconds spawnInterval;
    private int enemiesToSpawn;

    private void Start()
    {
        
    }

    public void StartSpawningEnemies(float spawnInterval, int enemiesToSpawn, GameManager gameManager)
    {
        if(spawningCoroutine != null)
            StopCoroutine(spawningCoroutine);

        this.enemiesToSpawn = enemiesToSpawn;

        this.spawnInterval = new WaitForSeconds(spawnInterval);
        spawningCoroutine = StartCoroutine(SpawningCoroutine(gameManager));
    }

    IEnumerator SpawningCoroutine(GameManager gameManager)
    {
        int spawnedEnemies = 0;
        while (spawnedEnemies < enemiesToSpawn)
        {
            Vector3 spawnPos = spawnLocations[Random.Range(0, spawnLocations.Count)].position;
            GameObject enemy  = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], spawnPos, Quaternion.identity, transform);

            enemy.GetComponent<Enemy>().Defeated += gameManager.OnEnemyDeath;

            spawnedEnemies++;
            yield return spawnInterval;
        }
    }
}
