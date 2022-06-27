using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameObject bullseye;

    private Coroutine _mouseReticleCoroutine;
    private WaitForSeconds _fixedDeltaTime;
    private bool _showReticle;

    private void Awake()
    {
        _fixedDeltaTime = new WaitForSeconds(Time.fixedDeltaTime);
    }
    private void OnEnable()
    {
        InputManager.InputDeviceChanged += OnInputDeviceChanged;
    }
    private void OnDisable()
    {
        InputManager.InputDeviceChanged -= OnInputDeviceChanged;
    }

    private void Start()
    {
        OnInputDeviceChanged(_inputManager.CurrentControlScheme);
    }
    void FixedUpdate()
    {
        if (!_showReticle)        
            return;       


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(_inputManager.MousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }
    }

    private void OnInputDeviceChanged(string currentDeviceName)
    {
        if (currentDeviceName.Equals("Gamepad"))
        {
            _showReticle = false;
            bullseye.SetActive(false);
        }
        else if (currentDeviceName.Equals("KBM"))
        {
            _showReticle=true;
            bullseye.SetActive(true);
            //_mouseReticleCoroutine = StartCoroutine(MouseReticleCoroutine());
        }

    }

    //IEnumerator MouseReticleCoroutine()
    //{
    //    while (true)
    //    {
    //        RaycastHit hit;
    //        Ray ray = Camera.main.ScreenPointToRay(_inputManager.MousePosition);
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            transform.position = hit.point;
    //        }
    //        yield return _fixedDeltaTime;
    //    }
    //}
}

