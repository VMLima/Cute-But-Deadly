using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FindTargetAction : AIAction
{
    public override void Act(StateController controller)
    {
        controller.Target = GameObject.FindObjectOfType<PlayerMovement>().transform;
    }
}

