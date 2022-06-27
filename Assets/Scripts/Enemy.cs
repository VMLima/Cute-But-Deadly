using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(StateController), typeof(CapsuleCollider), typeof(AIMovement))]
public class Enemy : MonoBehaviour
{
    public event Action Defeated;
    [SerializeField] private Animator animator;

    private StateController _stateController;
    [SerializeField] private AIState initialState;
    private CapsuleCollider _capsuleCollider;
    private AIMovement _movement;

    private void OnEnable()
    {
        GameManager.GameFinished += StopAnimation;
    }
    private void OnDisable()
    {
        GameManager.GameFinished -= StopAnimation;
    }

    private void StopAnimation()
    {
        animator.enabled = false;
    }

    private void Awake()
    {
        _movement = GetComponent<AIMovement>();
        _stateController = GetComponent<StateController>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Use this for initialization
    void Start()
    {
        _stateController.InitialiseAI(initialState);
    }

    public void Death()
    {
        _capsuleCollider.enabled = false;
        _movement.StopMovement();
        Defeated?.Invoke();
        animator.SetTrigger("Death");
    }
}
