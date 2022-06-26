using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/FindTargetAction")]
public class FindTargetAction : AIAction
{
    public override void Act(StateController controller)
    {
        controller.Target = GameObject.FindObjectOfType<PlayerMovement>().transform;
    }
}

