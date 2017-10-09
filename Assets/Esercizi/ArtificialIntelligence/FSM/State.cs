using UnityEngine;
using System.Collections;

namespace AI.FSM
{
    public abstract class State : MonoBehaviour
    {
        public abstract void StateUpdate();
    }

}
