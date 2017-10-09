using UnityEngine;
using System.Collections;

namespace AI.BehaviourTree
{
    // Contains shared data among tasks
    public class Blackboard : MonoBehaviour
    {
        public Movement botMovement;
        public Player player;
        public AIBot_BehaviourTree bot;
    }

}