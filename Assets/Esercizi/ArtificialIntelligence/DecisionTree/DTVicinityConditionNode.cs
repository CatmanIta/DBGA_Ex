using UnityEngine;
using System.Collections;
using System;

namespace AI.DecisionTree
{
    public class DTVicinityConditionNode : DTConditionNode
    {
        public Transform playerTr;
        public Transform botTr;

        public float threshold = 10f;

        public override bool CheckCondition()
        {
            float distance = Vector3.Distance(playerTr.position, botTr.position);
            bool conditionTrue = distance < threshold;
            return conditionTrue;
        }

    }

}