using UnityEngine;
using System.Collections;

namespace AI.DecisionTree
{
    public abstract class DTNode : MonoBehaviour
    {
        public abstract void MakeDecision();
    }
}