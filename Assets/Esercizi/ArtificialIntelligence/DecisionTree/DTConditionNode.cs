using UnityEngine;
using System.Collections;
using System;

namespace AI.DecisionTree
{
    public abstract class DTConditionNode : DTNode
    {
        public DTNode trueNode;
        public DTNode falseNode;

        public override void MakeDecision()
        {
            if (CheckCondition())
            {
                trueNode.MakeDecision();
            }
            else
            {
                falseNode.MakeDecision();
            }
        }

        // Test done by concrete classes
        public abstract bool CheckCondition();

    }
}