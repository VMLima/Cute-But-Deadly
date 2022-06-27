using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is going to contain anything that a powerup can touch so it can distribute to other scripts
public class Player : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<Shield> shields;

    [Header("Player Settings")]
    [SerializeField] private int maxHealth = 5;
    private int health = 3;

    private PlayerMovement playerMovement;
    private PlayerFire playerFire;

    private bool canReceiveDamage = true;

    private Coroutine shieldCoroutine;

    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        playerFire = GetComponent<PlayerFire>();
        health = maxHealth;
    }

    private void ChangeHealthState()
    {
        if (!canReceiveDamage || health <= 0)
            return;

        canReceiveDamage = false;
        health -= 1;
        uiManager.SetPlayerHealth((float)health/maxHealth);
        if (health <= 0)
        {
            //GAME OVER
            gameManager.PlayerDead();
        }
        StartCoroutine(DamageCooldownTimerCoroutine());
    }

    public void UsePowerUp(PowerUpType powerUpType, float duration)
    {
        switch (powerUpType)
        {
            case PowerUpType.Health:
                if (health < maxHealth)
                    health++;
                uiManager.SetPlayerHealth((float)health / maxHealth);
                break;
            case PowerUpType.RapidFire:
                playerFire.EnableRapidFire(duration);
                break;
            case PowerUpType.Shield:
                if (shieldCoroutine != null)
                    StopCoroutine(shieldCoroutine);
                shieldCoroutine = StartCoroutine(ActivateShield(duration));
                break;
            default:
                break;
        }
    }

    IEnumerator ActivateShield(float duration)
    {
        foreach (var shield in shields)
        {
            shield.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(duration);

        foreach (var shield in shields)
        {
            shield.gameObject.SetActive(false);
        }
    }

    IEnumerator DamageCooldownTimerCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        canReceiveDamage = true;
    }

    private void OnCollisionEnter(Collision collision) {
        GameObject obj = collision.gameObject;
        if ((obj.CompareTag("Circle") || obj.CompareTag("Square") || obj.CompareTag("Triangle"))
              && obj.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            ChangeHealthState();
            Destroy(obj, 0.1f);
        }
    }

}

