using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PluggableAI/ChaseAction")]
public class ChaseAction : AIAction
{
    public override void Act(StateController controller)
    {
        if(controller.Target == null)
            return;

        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        controller.AIMovement.MoveTo(controller.Target.position.HorizontalVector3());
    }
}
