using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(InputManager))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 10;
    [SerializeField] private float gamepadRotationSmoothing = 1000f;

    [SerializeField] private Transform playerModel;

    [SerializeField] private Transform reticle;

    private Rigidbody _rigidbody;
    private Transform _camera;
    private InputManager _inputManager;
    public Vector3 ForwardDirection { get => playerModel.transform.forward; }
    public Quaternion CurrentRotation { get => playerModel.transform.rotation; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        SetLookDirection();
    }

    private void FixedUpdate()
    {
        ExecuteMovement();
    }

    private void SetLookDirection()
    {
        //RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(_inputManager.MousePosition);
        //if (Physics.Raycast(ray, out hit))
        //{
        //    Vector3 lookPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //    playerModel.transform.LookAt(lookPos);
        //}

        if (_inputManager.GamepadEnabled)
        {
            Vector2 input = _inputManager.LookVector;
            Vector3 cameraForward = _camera.forward.HorizontalVector3().normalized;
            Vector3 cameraRight = _camera.right.HorizontalVector3().normalized;

            Vector3 lookDir = cameraForward * input.y + cameraRight * input.x;
           
            if(lookDir.sqrMagnitude > 0)
            {
                Quaternion newRot = Quaternion.LookRotation(lookDir, Vector3.up);
                playerModel.rotation = Quaternion.RotateTowards(playerModel.rotation, newRot, gamepadRotationSmoothing * Time.deltaTime);
            }
        }
        else
        {
            Vector3 lookPos = new Vector3(reticle.position.x, transform.position.y, reticle.position.z);
            playerModel.transform.LookAt(lookPos);
        }        
    }

    private void ExecuteMovement()
    {
        Vector2 input = _inputManager.MovementVector;

        Vector3 cameraForward = _camera.forward.HorizontalVector3().normalized;
        Vector3 cameraRight = _camera.right.HorizontalVector3().normalized;

        Vector3 moveDir = (cameraForward * input.y * 1.5f) + (cameraRight * input.x);
        //moveDir = moveDir.normalized;
        _rigidbody.velocity = moveDir * speed;
    }

    public void SetPlayerSpeed(float _speed) 
    {
        speed = _speed;
    }
}
