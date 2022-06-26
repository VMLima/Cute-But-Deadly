using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is going to contain anything that a powerup can touch so it can distribute to other scripts
public class PlayerStats : MonoBehaviour {

    [Header("Player Settings")]
    public float playerSpeed = 10f;
    public int baseHealth = 3;
    public float i_frameDuration = 1f;

    [Header("Bullet Settings")]
    public float bulletCooldown = 0.1f;
    public float age = 3f;
    public float bulletSpeed = 15f;

    private PlayerMovement playerMovement;
    private PlayerFire playerFire;

    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        playerFire = GetComponent<PlayerFire>();

        playerFire.InitializeStats(bulletCooldown, age, bulletSpeed);
        playerMovement.SetPlayerSpeed(playerSpeed);
    }

    private void ChangeHealthState() {
        baseHealth -= 1;
        if (baseHealth <= 0) {
            Debug.Log("GameOver");

        }
    }

    private void OnCollisionEnter(Collision collision) {
        GameObject obj = collision.gameObject;
        if ((obj.CompareTag("Circle") || obj.CompareTag("Square") || obj.CompareTag("Triangle"))
              && obj.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            ChangeHealthState();
        }
    }

}

