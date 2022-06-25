using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    public float speed;

    private CharacterController controller;
    private PlayerControls playerControls;

    private void Awake() {
        controller = GetComponent<CharacterController>();

        playerControls = new PlayerControls();
        playerControls.Player.Enable();

    }

    private void Update() {

        // Controls WASD player movement
        Vector2 moveDirection = playerControls.Player.Movement.ReadValue<Vector2>();
        moveDirection *= speed;
        controller.Move(new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime);

        // Cast a point to world position for player mouse look
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(playerControls.Player.MousePosition.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.CompareTag("Ground")) {
                Vector3 lookPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                transform.LookAt(lookPos);
            }
        }

    }

}
