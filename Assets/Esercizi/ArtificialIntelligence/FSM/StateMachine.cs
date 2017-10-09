using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI.FSM
{
    public class Transition
    {
        public State fromState;
        public Input input;
        public State toState;

        public Transition(State s1, Input i, State s2)
        {
            fromState = s1;
            input = i;
            toState = s2;
        }
    }

    public class StateMachine
    {
        public State stateIdle;
        public State stateFlee;
        public State stateSearch;

        public State initialState;


        List<Transition> transitions;
        private State currentState;


        // Start up the machine
        public void StartMachine()
        {
            this.currentState = initialState;
        }

        public void CreateTransitions()
        {
            // Create all transitions between states
            transitions = new List<Transition>();
            transitions.Add(new Transition(stateIdle, Input.PlayerClose, stateFlee));
            transitions.Add(new Transition(stateFlee, Input.PlayerNotClose, stateIdle));
            transitions.Add(new Transition(stateIdle, Input.Timeout, stateSearch));
            transitions.Add(new Transition(stateSearch, Input.PlayerClose, stateFlee));
        }


        // Method that updates the current state of the AI
        public void StateUpdate()
        {
            currentState.StateUpdate();
        }

        // Handle the arrival of an input
        public void HandleInput(Input i)
        {
            foreach(var transition in transitions)
            {
                if (transition.input == i && transition.fromState == this.currentState)
                {
                    this.currentState = transition.toState;
                }
            }
        }

    }

}
