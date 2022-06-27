using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

[RequireComponent(typeof(InputManager), typeof(AudioSource))]
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

    private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> fireAudioClips = new();
    

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        _audioSource = GetComponent<AudioSource>();
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
                _audioSource.PlayOneShot(fireAudioClips[Random.Range(0,fireAudioClips.Count)], 0.3f);
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
        uiManager.EnableRapidFireIcons(true);

        yield return new WaitForSeconds(powerupTime);

        uiManager.EnableRapidFireIcons(false);
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
