using UnityEngine;
using System.Collections;
using System;

namespace AI.BehaviourTree
{

    public class ShootTask : Task
    {
        public override bool Run()
        {
            blackboard.bot.Shoot();
            return true;
        }
    }

}