using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    public float speed = 5f;
    public Transform ground;

    private CharacterController controller;
    private PlayerControls playerControls;

    private void Awake() {
        controller = GetComponent<CharacterController>();

        playerControls = new PlayerControls();
        playerControls.Player.Enable();

    }

    private void Update() {
        Vector2 moveDirection = playerControls.Player.Movement.ReadValue<Vector2>();
        moveDirection *= speed;
        controller.Move(new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(playerControls.Player.MousePosition.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform == ground) {
                //Vector3 lookPos = hit.point;
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }
        //Vector3 mousePos = playerControls.Player.MousePosition.ReadValue<Vector2>();
        //mousePos.z = 
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //transform.forward = mousePos;

    }

}
