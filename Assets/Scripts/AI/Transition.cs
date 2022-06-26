using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    public AIDecision decision;
    public AIState trueState;
    public AIState falseState;
}
