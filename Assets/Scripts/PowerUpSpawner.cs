using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerupPrefabs = new List<GameObject>();

    private List<GameObject> powerups = new List<GameObject>();   
    private BoxCollider _collider;

    private Vector2 xMinMax;
    private Vector2 zMinMax;

    private Coroutine powerupSpawningCoroutine;
   
    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        xMinMax = new(-_collider.size.x/2, _collider.size.x/2);
        zMinMax = new(-_collider.size.z/2, _collider.size.z/2);

        for (int i = 0; i < powerupPrefabs.Count; i++)
        {
            GameObject powerup = Instantiate(powerupPrefabs[i], Vector3.zero, Quaternion.identity, transform);
            powerup.GetComponent<PowerUp>().SetSpawner(this);
            powerups.Add(powerup);
            powerup.SetActive(false);
        }
    }
    private void OnEnable()
    {
        GameManager.GameFinished += StopSpawning;
    }
    private void OnDisable()
    {
        GameManager.GameFinished -= StopSpawning;
    }
    private void Start()
    {
        powerupSpawningCoroutine = StartCoroutine(PowerupSpawningCoroutine());
    }
    
    private void StopSpawning()
    {
        if (powerupSpawningCoroutine != null)
            StopCoroutine(powerupSpawningCoroutine);
    }
    IEnumerator PowerupSpawningCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(20);
            ShowPowerups();
        }
    }

    public void ShowPowerups()
    {
        foreach (var powerup in powerups)
        {
            Vector3 spawnPos = new Vector3(Random.Range(xMinMax.x, xMinMax.y), 0, Random.Range(zMinMax.x, zMinMax.y));
            powerup.transform.position = spawnPos;
            powerup.SetActive(true);
        }
    }

    public void HidePowerups()
    {
        foreach (var powerup in powerups)
        {
            powerup.SetActive(false);
        }
    }
}
