using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(_inputManager.MousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }
    }
}
