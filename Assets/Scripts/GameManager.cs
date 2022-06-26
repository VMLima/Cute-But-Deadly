using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemySpawnManager enemySpawnManager;
    private int currentWave = 0;
    [SerializeField] private List<WaveScriptableObject> wavePresets = new List<WaveScriptableObject>();

    private int enemiesRemaining;

    private void Awake()
    {
    }

    void Start()
    {
        enemiesRemaining = wavePresets[currentWave].EnemiesInWave;
        enemySpawnManager.StartSpawningEnemies(wavePresets[currentWave].SpawnInterval, wavePresets[currentWave].EnemiesInWave, this);
    }

    public void OnEnemyDeath()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
            LoadNextWave();
    }
    private void LoadNextWave()
    {
        if (!enemySpawnManager.gameObject.activeInHierarchy)
            return;

        currentWave++;
        if(currentWave >= wavePresets.Count)
        {
            //GAME OVER
            return;
        }

        enemiesRemaining = wavePresets[currentWave].EnemiesInWave;
        enemySpawnManager.StartSpawningEnemies(wavePresets[currentWave].SpawnInterval, wavePresets[currentWave].EnemiesInWave, this);
    }
}
