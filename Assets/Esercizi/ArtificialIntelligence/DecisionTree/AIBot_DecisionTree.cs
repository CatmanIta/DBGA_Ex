using UnityEngine;
using System.Collections;

namespace AI.DecisionTree
{
    // Bot controllato da un Decision Tree
    public class AIBot_DecisionTree : MonoBehaviour
    {
        public Movement movement;

        public DTNode rootNode;

        public float aiPeriod = 0.5f;

        public void Start()
        {
            InvokeRepeating("DoAI", aiPeriod, aiPeriod);
        }

        public void DoAI()
        {
            // Use the decision tree to get the velocity for the movement
            rootNode.MakeDecision();
        }

    }

}