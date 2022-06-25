using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TickInterval
{
    EveryFrame,
    FixedDeltaTime,
    Custom
}
public class StateController : MonoBehaviour
{
    public AIState CurrentState;


    [HideInInspector] public Enemy Enemy;
    [HideInInspector] public AIMovement AIMovement;
    [HideInInspector] public Transform Target;

    /// <summary>
    /// Used for states that require a timer to perform actions at specific intervals.
    /// </summary>
    [HideInInspector] public float StateTimer = 0;
    [HideInInspector] public float StateTimeElapsed { get; private set; }


    private Coroutine aiCoroutine;
    private WaitForSeconds tickInterval;

    [SerializeField] private bool aiActive = false;

    private void Awake()
    {
        tickInterval = new WaitForSeconds(Time.fixedDeltaTime);
    }


    public void InitialiseAI(AIState initialState)
    {
        TransitionToState(initialState);
        UpdateParentEnemy();
    }

    /// <summary>
    /// Find enemy component, subscribe to enemy events.
    /// </summary>
    public void UpdateParentEnemy()
    {
        DeactivateAI();

        //unsubscribe from old enemy disabled event
        if (Enemy != null)
        {
            Enemy.Defeated -= DeactivateAI;
        }

        Enemy = GetComponent<Enemy>();
        AIMovement = GetComponent<AIMovement>();
        Enemy.Defeated += DeactivateAI;

        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        
        ActivateAI();
    }


    public void StopFollowing()
    {
        //todo follow again after interval
        
    }

    public void ActivateAI()
    {
        aiActive = true;
        aiCoroutine = StartCoroutine(AICoroutine());
    }
    public void DeactivateAI()
    {
        if (aiCoroutine != null) { StopCoroutine(aiCoroutine); }
        aiActive = false;
    }


    void Update()
    {
        if (StateTimer > 0)
        {
            StateTimer -= Time.deltaTime;
        }

        StateTimeElapsed += Time.deltaTime;

    }

    /// <summary>
    /// Ticks the state machine update function with a specified interval time.
    /// </summary>
    /// <returns></returns>
    IEnumerator AICoroutine()
    {
        while (true)
        {
            if (!aiActive) { yield break; }

            CurrentState.UpdateState(this);
            yield return tickInterval;
        }
    }

    public void TransitionToState(AIState nextState)
    {

        if (nextState != null)
        {
            if (CurrentState != null)
            {
                foreach (AIAction action in CurrentState.OnExitActions)
                {
                    action.Act(this);
                }
            }

            CurrentState = nextState;

            foreach (AIAction action in CurrentState.OnEnterActions)
            {
                action.Act(this);
            }

            UpdateTickInterval();

            StateTimeElapsed = 0;
            StateTimer = 0;
        }
    }

    private void UpdateTickInterval()
    {
        if (CurrentState.TickInterval == TickInterval.EveryFrame && aiCoroutine != null)
        {
            tickInterval = null;
        }
        else if (CurrentState.TickInterval == TickInterval.Custom)
        {
            tickInterval = new WaitForSeconds(CurrentState.customTickInterval);
        }
        else if (CurrentState.TickInterval == TickInterval.FixedDeltaTime)
        {
            tickInterval = new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
}
