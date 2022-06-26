using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float waveTimer = 20;
    [SerializeField] private EnemySpawnManager enemySpawnManager;
    private int currentWave = 0;

    private int enemiesRemaining;

    [SerializeField] private int maxWaves = 10;
    public int enemiesInWave = 10;
    public float spawnInterval = 1;

    public static event Action GameFinished;

    private Coroutine waveCoroutine;

    private void Awake()
    {
        enemiesRemaining = enemiesInWave;
        uiManager.SetEnemyCount(enemiesRemaining);
        enemySpawnManager.StartSpawningEnemies(spawnInterval, enemiesInWave, this);

        Application.targetFrameRate = 60;
    }

    void Start()
    {
        waveCoroutine = StartCoroutine(WaveCoroutine());
    }

    IEnumerator WaveCoroutine()
    {
        float timer = waveTimer;
        while (currentWave <= maxWaves)
        {            
            uiManager.SetWaveTimer(timer);
            if(timer <= 0)
            {
                LoadNextWave();
                timer = waveTimer;
            }
            timer -= Time.deltaTime;
            yield return null;
        }
        uiManager.DisableWaveTimer();
    }

    public void PlayerDead()
    {
        //TODO show player died screen
        FinishGame();
    }

    public void OnEnemyDeath()
    {
        enemiesRemaining--;
        uiManager.SetEnemyCount(enemiesRemaining);
        if (currentWave == maxWaves && enemiesRemaining <= 0)
        {
            //GAME OVER
            //TODO Show game completed screen
            FinishGame();
        }
    }

    private void FinishGame()
    {
        if (waveCoroutine != null)
            StopCoroutine(waveCoroutine);

        GameFinished?.Invoke();
    }

    private void LoadNextWave()
    {
        if (!enemySpawnManager.gameObject.activeInHierarchy)
            return;

        currentWave++;

        enemiesInWave += 5;
        enemiesRemaining += enemiesInWave;
        spawnInterval = Mathf.Clamp(spawnInterval * 0.85f, 0.3f, 1);

        uiManager.SetEnemyCount(enemiesRemaining);
        enemySpawnManager.StartSpawningEnemies(spawnInterval, enemiesInWave, this);

    }
}
