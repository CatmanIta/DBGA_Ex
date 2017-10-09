using UnityEngine;
using System.Collections;
using System;

namespace AI.FSM
{
    public class State_Search : State
    {
        public Movement botMovement;

        public override void StateUpdate()
        {
            // Move randomly around
            botMovement.velocity.x = UnityEngine.Random.Range(-10f, 10f) ;
            botMovement.velocity.y = UnityEngine.Random.Range(-10f, 10f);
        }

    }

}
