using UnityEngine;
using System.Collections;
using System;

namespace AI.BehaviourTree
{

    public class SelectorTask : Task
    {
        public override bool Run()
        {
            foreach(var child in children)
            {
                if (child.Run()) return true;
            }
            return false;
        }
    }

}