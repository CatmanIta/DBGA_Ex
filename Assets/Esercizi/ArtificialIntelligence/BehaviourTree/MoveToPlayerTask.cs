using UnityEngine;
using System.Collections;
using System;

namespace AI.BehaviourTree
{

    public class MoveToPlayerTask : Task
    {
        public override bool Run()
        {
            Vector3 playerToBot = 
                (blackboard.bot.transform.position
                    - blackboard.player.transform.position).normalized;
            blackboard.botMovement.velocity = -playerToBot;
            return true;
        }
    }

}