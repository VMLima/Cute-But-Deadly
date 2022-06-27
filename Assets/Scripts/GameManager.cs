using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static event Action GameStarted;

    private Coroutine waveCoroutine;

    private void Awake()
    {
        enemiesRemaining = enemiesInWave;
        uiManager.SetEnemyCount(enemiesRemaining);

#if UNITY_WEBGL
        Application.targetFrameRate = 60;
#endif
        Cursor.visible = false;
    }

    void Start()
    {
        uiManager.FadeIn();
        uiManager.FadeCompleted += StartGame;
    }
    private void StartGame()
    {
        uiManager.FadeCompleted -= StartGame;

        enemySpawnManager.SetSpawningParameters(spawnInterval, enemiesInWave);

        waveCoroutine = StartCoroutine(WaveCoroutine());

        GameStarted?.Invoke();
    }
    private void FinishGame()
    {
        if (waveCoroutine != null)
            StopCoroutine(waveCoroutine);

        GameFinished?.Invoke();
        Cursor.visible = true;
    }
    public void RestartGame()
    {
        uiManager.FadeOut();
        uiManager.FadeCompleted += ReloadScene;
    }
    private void ReloadScene()
    {
        uiManager.FadeCompleted -= ReloadScene;
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator WaveCoroutine()
    {
        float timer = waveTimer;
        while (currentWave <= maxWaves)
        {
            uiManager.SetWaveTimer(timer);
            if (timer <= 0)
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
        uiManager.ShowDefeatScreen();
        FinishGame();
    }

    public void OnEnemyDeath()
    {
        enemiesRemaining--;
        uiManager.SetEnemyCount(enemiesRemaining);
        if (currentWave == maxWaves && enemiesRemaining <= 0)
        {
            //GAME OVER
            uiManager.ShowVictoryScreen();
            FinishGame();
        }
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

        enemySpawnManager.SetSpawningParameters(spawnInterval, enemiesInWave);
        enemySpawnManager.StartSpawningEnemies();

    }
}
