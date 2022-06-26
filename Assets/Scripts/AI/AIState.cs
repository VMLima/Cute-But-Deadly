using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class AIState : ScriptableObject
{
    [Tooltip("The tick interval for the ai state machine. If there is any physics dependent actions in this state, use fixed delta time.")]
    public TickInterval TickInterval;
    [Tooltip("This value is only used if 'Custom' Tick Interval is selected.")]
    public float customTickInterval = 0.1f;
    public AIAction[] OnEnterActions;
    public AIAction[] actions;
    public Transition[] transitions;
    public AIAction[] OnExitActions;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if (decisionSucceeded && transitions[i].trueState != null)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else if (!decisionSucceeded && transitions[i].falseState != null)
            {
                controller.TransitionToState(transitions[i].falseState);

            }
        }
    }
}