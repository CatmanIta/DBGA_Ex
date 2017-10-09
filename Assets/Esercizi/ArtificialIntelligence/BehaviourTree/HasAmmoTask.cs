using UnityEngine;
using System.Collections;
using System;

namespace AI.BehaviourTree
{

    public class HasAmmoTask : Task
    {
        public override bool Run()
        {
            return blackboard.bot.currentAmmo > 0;
        }
    }

}