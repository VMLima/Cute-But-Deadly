using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private PlayerControls playerControls;
    public PlayerControls PlayerControls
    {
        get
        {
            if(playerControls == null)
            {
                playerControls = new PlayerControls();
                playerControls.Enable();
            }
            return playerControls;

        }
    }
    public Vector2 MovementVector { get => PlayerControls.Player.Movement.ReadValue<Vector2>(); }
    public Vector2 LookVector { get => PlayerControls.Player.Look.ReadValue<Vector2>(); }
    public Vector2 MousePosition { get => PlayerControls.Player.MousePosition.ReadValue<Vector2>(); }

    public string CurrentControlScheme { get => playerInput.currentControlScheme; }
    public bool GamepadEnabled { get => playerInput.currentControlScheme.Equals("Gamepad"); }

    public static event Action<string> InputDeviceChanged;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        GameManager.GameFinished += DisableControls;
    }
    private void OnDisable()
    {
        GameManager.GameFinished -= DisableControls;
    }

    public void OnInputDeviceChanged()
    {
        InputDeviceChanged?.Invoke(CurrentControlScheme);
    }

    public void DisableControls()
    {
        playerControls.Disable();
    }

}
