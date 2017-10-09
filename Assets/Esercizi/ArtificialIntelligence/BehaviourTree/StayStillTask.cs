using UnityEngine;
using System.Collections;
using System;

namespace AI.BehaviourTree
{

    public class StayStillTask : Task
    {
        public override bool Run()
        {
            blackboard.botMovement.velocity = Vector3.zero;
            return true;
        }
    }

}