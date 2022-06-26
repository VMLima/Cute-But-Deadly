using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

[RequireComponent(typeof(InputManager))]
public class PlayerFire : MonoBehaviour {

    [SerializeField] private float firingInterval = 1;
    [SerializeField] private UIManager uiManager;

    private float firingIntervalModifier = 1;

    public GameObject CircleBulletPrefab;
    public GameObject SquareBulletPrefab;
    public GameObject TriangleBulletPrefab;

    public Transform firePoint;
    public Transform modelHolder;


    private GameObject currentBulletType;
    private PlayerMovement playerMovement;

    private InputManager _inputManager;

    private Coroutine firingCoroutine;
    private Coroutine rapidFireCoroutine;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start() {
        currentBulletType = CircleBulletPrefab;
        uiManager.SetAmmoType(AmmoType.Circle);
    }

    private void OnEnable()
    {
        _inputManager.PlayerControls.Player.Fire.performed += OnFireButtonPressed;
        _inputManager.PlayerControls.Player.Fire.canceled += OnFireButtonReleased;
    }
    private void OnDisable()
    {
        _inputManager.PlayerControls.Player.Fire.performed -= OnFireButtonPressed;
        _inputManager.PlayerControls.Player.Fire.canceled -= OnFireButtonReleased;
    }

    private void OnFireButtonPressed(InputAction.CallbackContext obj)
    {
        if (firingCoroutine != null)
            StopCoroutine(firingCoroutine);

        firingCoroutine = StartCoroutine(FireBulletsCoroutine());
    }
    private void OnFireButtonReleased(InputAction.CallbackContext obj)
    {
        if (firingCoroutine != null)
            StopCoroutine(firingCoroutine);
    }

    IEnumerator FireBulletsCoroutine()
    {
        float timer = 0;
        while (true)
        {
            if(timer <= 0)
            {
                Instantiate(currentBulletType, firePoint.position, playerMovement.CurrentRotation, transform);
                timer = firingInterval * firingIntervalModifier;
            }

            timer -= Time.deltaTime;
            yield return null; 
        }
    }

    public void EnableRapidFire(float powerupTime)
    {
        if (rapidFireCoroutine != null)
            StopCoroutine(rapidFireCoroutine);

        rapidFireCoroutine = StartCoroutine(RapidFireCoroutine(powerupTime));
    }
    IEnumerator RapidFireCoroutine(float powerupTime)
    {
        firingIntervalModifier = 0.5f;

        yield return new WaitForSeconds(powerupTime);

        firingIntervalModifier = 1;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("CircleZone")) {
            currentBulletType = CircleBulletPrefab;
            uiManager.SetAmmoType(AmmoType.Circle);

        } else if (other.CompareTag("SquareZone")) {
            currentBulletType = SquareBulletPrefab;
            uiManager.SetAmmoType(AmmoType.Square);

        } else if (other.CompareTag("TriangleZone")) {
            currentBulletType = TriangleBulletPrefab;
            uiManager.SetAmmoType(AmmoType.Triangle);
        }
    }
}
