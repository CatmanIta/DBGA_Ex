using UnityEngine;
using System.Collections;
using System;

namespace AI.FSM
{
    public class State_Flee : State
    {
        public Movement botMovement;
        public Transform playerTr;
        public Transform botTr;

        public override void StateUpdate()
        {
            // Flee from the player   
            Vector3 playerToBot = (botTr.position - playerTr.position).normalized;
            botMovement.velocity = playerToBot;
        }

    }

}
