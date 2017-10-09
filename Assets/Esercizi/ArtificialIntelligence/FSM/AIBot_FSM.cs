using UnityEngine;
using System.Collections;

namespace AI.FSM
{
    public class AIBot_FSM : MonoBehaviour
    {
        private StateMachine sm; 

        void Start()
        {
            // Let's initialise the FSM
            sm = new StateMachine();
            sm.stateIdle = GetComponentInChildren<State_Idle>();
            sm.stateFlee = GetComponentInChildren<State_Flee>();
            sm.stateSearch = GetComponentInChildren<State_Search>();

            // Set the initial state
            sm.initialState = sm.stateIdle;
            sm.StartMachine();

            // Create transitions too
            sm.CreateTransitions();


            InvokeRepeating("SendTimeoutInput", 1f, 1f);

        }

        public Transform playerTr;
        public float threshold = 2f;

        void Update()
        {
            // Get inputs
            float distance = Vector3.Distance(playerTr.position, this.transform.position);
            if (distance < threshold)
            {
                sm.HandleInput(Input.PlayerClose);
            } else
            {
                sm.HandleInput(Input.PlayerNotClose);
            }

            // At each frame, we ask the FSM to do whattever its current state wants to do
            sm.StateUpdate();

        }

        private void SendTimeoutInput()
        {
            sm.HandleInput(Input.Timeout);
        }

    }
}
