using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    public float speed;

    [SerializeField] private Transform playerModel;

    private CharacterController controller;
    private PlayerControls playerControls;
    private Rigidbody _rigidbody;
    private Transform _camera;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();

        _camera = Camera.main.transform;
        playerControls = new PlayerControls();
        playerControls.Player.Enable();

    }

    private void Update()
    {
        //// Controls WASD player movement
        //Vector2 moveDirection = playerControls.Player.Movement.ReadValue<Vector2>();
        //moveDirection *= speed;
        //controller.Move(new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime);

        SetLookDirection();
    }

    private void FixedUpdate()
    {
        ExecuteMovement();
    }

    private void SetLookDirection()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(playerControls.Player.MousePosition.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lookPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            playerModel.transform.LookAt(lookPos);
        }
    }

    private void ExecuteMovement()
    {
        Vector2 input = playerControls.Player.Movement.ReadValue<Vector2>();

        Vector3 cameraForward = _camera.forward.HorizontalVector3().normalized;
        Vector3 cameraRight = _camera.right.HorizontalVector3().normalized;

        Vector3 moveDir = (cameraForward * input.y) + (cameraRight * input.x);
        moveDir = moveDir.normalized;

        _rigidbody.velocity = moveDir * speed;
    }
}
