using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour {

    private float bulletCooldown;

    private float age;
    private float speed;

    public GameObject CircleBulletPrefab;
    public GameObject SquareBulletPrefab;
    public GameObject TriangleBulletPrefab;

    public Transform spawner;
    public Transform modelHolder;

    private bool isShooting = false;
    private float timer;

    private GameObject currentBulletType;
    private PlayerControls playerControls;

    private void Start() {
        currentBulletType = CircleBulletPrefab;
        timer = bulletCooldown;

        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    private void Update() {
        // Instantiate the bullet after a set cooldown and set the stats
        if (isShooting) {
            timer -= Time.deltaTime;
            if (timer <= 0f) {
                GameObject bullet = Instantiate(currentBulletType, spawner.position, modelHolder.rotation);
                bullet.GetComponent<BulletController>().SetStats(age, speed);
                timer = bulletCooldown;
            }
        }

        if (playerControls.Player.Fire.WasPressedThisFrame()) {
            isShooting = true;
        } 
        if (playerControls.Player.Fire.WasReleasedThisFrame()) {
            isShooting = false;
        }
    }

    // I'm not sure if there is a better way to do this, but this is for press and hold "automatic" firing
    //public void Fire(InputAction.CallbackContext context) {
    //    if (context.started) {
    //        isShooting = true;
    //    }
    //    if (context.canceled) {
    //        isShooting = false;
    //    }

    //}

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("CircleZone")) {
            currentBulletType = CircleBulletPrefab;

        } else if (other.CompareTag("SquareZone")) {
            currentBulletType = SquareBulletPrefab;

        } else if (other.CompareTag("TriangleZone")) {
            currentBulletType = TriangleBulletPrefab;

        }
    }

    public void InitializeStats(float _cooldown, float _age, float _speed) {
        bulletCooldown = _cooldown;
        age = _age;
        speed = _speed;
    }

}
