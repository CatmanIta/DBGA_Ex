using UnityEngine;
using System.Collections;
using System;

namespace AI.BehaviourTree
{

    public class PlayerIsCloseTask : Task
    {
        public float threshold = 1;

        public override bool Run()
        {
            float distance = (blackboard.bot.transform.position
                    - blackboard.player.transform.position).magnitude;
            return distance < threshold;
        }
    }

}