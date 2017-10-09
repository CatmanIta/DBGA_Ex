using UnityEngine;
using System.Collections;
using System;

namespace AI.DecisionTree
{
    public class DTFleeActionNode : DTActionNode
    {
        public Movement botMovement;
        public Transform playerTr;
        public Transform botTr;

        public override void MakeDecision()
        {
            Vector3 playerToBot = (botTr.position - playerTr.position).normalized;
            botMovement.velocity = playerToBot;
        }

    }

}