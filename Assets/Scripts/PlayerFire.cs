using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour {

    public float bulletCooldown = 1f;

    public float age;
    public float speed;

    public GameObject CircleBulletPrefab;
    public GameObject SquareBulletPrefab;
    public GameObject TriangleBulletPrefab;

    public Transform spawner;


    private bool isShooting = false;
    private float timer;

    private GameObject currentBulletType;

    private void Start() {
        currentBulletType = CircleBulletPrefab;
        timer = bulletCooldown;
    }

    private void Update() {
        // Instantiate the bullet after a set cooldown and set the stats
        if (isShooting) {
            timer -= Time.deltaTime;
            if (timer <= 0f) {
                GameObject bullet = Instantiate(currentBulletType, spawner.position, transform.rotation);
                bullet.GetComponent<BulletController>().SetStats(age, speed);
                timer = bulletCooldown;
            }
        }
    }

    // I'm not sure if there is a better way to do this, but this is for press and hold "automatic" firing
    public void Fire(InputAction.CallbackContext context) {
        if (context.started) {
            isShooting = true;
        }
        if (context.canceled) {
            isShooting = false;
        }

    }

}
