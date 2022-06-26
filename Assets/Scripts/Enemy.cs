using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(StateController), typeof(CapsuleCollider), typeof(AIMovement))]
public class Enemy : MonoBehaviour
{
    public event Action Defeated;

    private StateController _stateController;
    [SerializeField] private AIState initialState;
    private CapsuleCollider _capsuleCollider;

    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void OnDestroy()
    {
        Defeated?.Invoke();
    }

    // Use this for initialization
    void Start()
    {
        _stateController.InitialiseAI(initialState);
    }
}
