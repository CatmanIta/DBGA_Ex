using UnityEngine;
using System.Collections;
using System;

namespace AI.FSM
{
    public class State_Idle : State
    {
        public Movement botMovement;

        public override void StateUpdate()
        {
            // Stay where you are
            botMovement.velocity = Vector3.zero;
        }

    }

}
