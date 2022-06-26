using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

[RequireComponent(typeof(InputManager))]
public class PlayerFire : MonoBehaviour {

    [SerializeField] private float firingInterval = 1;

    public GameObject CircleBulletPrefab;
    public GameObject SquareBulletPrefab;
    public GameObject TriangleBulletPrefab;

    public Transform spawner;
    public Transform modelHolder;

    private float cooldownTimer = 0;

    private GameObject currentBulletType;
    private PlayerMovement playerMovement;

    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start() {
        currentBulletType = CircleBulletPrefab;
    }

    private void OnEnable()
    {
        _inputManager.PlayerControls.Player.Fire.performed += FireBullet;
    }
    private void OnDisable()
    {
        _inputManager.PlayerControls.Player.Fire.performed -= FireBullet;
    }

    private void FireBullet(InputAction.CallbackContext obj)
    {        
        if (cooldownTimer > 0)
            return;

        GameObject bullet = Instantiate(currentBulletType, spawner.position, playerMovement.CurrentRotation, transform);        
        cooldownTimer = firingInterval;

    }

    private void Update() 
    {

        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("CircleZone")) {
            currentBulletType = CircleBulletPrefab;

        } else if (other.CompareTag("SquareZone")) {
            currentBulletType = SquareBulletPrefab;

        } else if (other.CompareTag("TriangleZone")) {
            currentBulletType = TriangleBulletPrefab;

        }
    }
}
