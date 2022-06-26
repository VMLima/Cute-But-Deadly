using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
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
    public Vector2 MousePosition { get => PlayerControls.Player.MousePosition.ReadValue<Vector2>(); }

}
