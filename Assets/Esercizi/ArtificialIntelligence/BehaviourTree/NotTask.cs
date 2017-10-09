using UnityEngine;
using System.Collections;
using System;

namespace AI.BehaviourTree
{

    public class NotTask : Task
    {
        public override bool Run()
        {
            return !children[0].Run();
        }
    }

}