using UnityEngine;
using System.Collections;
using System;

namespace AI.BehaviourTree
{

    public class WaitTask : Task
    {
        public float period = 1;
        private float t = 0;

        public override bool Run()
        {
            t += Time.deltaTime;
            if (t < period)
            {
                return true;
            }
            t = 0;
            return children[0].Run();
        }

    }

}