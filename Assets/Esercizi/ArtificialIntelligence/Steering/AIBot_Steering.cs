using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI.SteeringBehaviour
{
    public class AIBot_Steering : MonoBehaviour
    {
        public class WeightedSteeringBehaviour
        {
            public AbstractSteeringBehaviour steeringBehaviour;
            public float weight;
        }

        List<WeightedSteeringBehaviour> weightedSteeringBehaviourList;

        WeightedSteeringBehaviour seekWSb;

        public SteeringMovement movement;

        void Start()
        {
            movement = GetComponent<SteeringMovement>();

            // Setup steering (decision making)
            weightedSteeringBehaviourList = new List<WeightedSteeringBehaviour>();

            {
                var sb = new SeekSteeringBehaviour();
                sb.seekTarget = FindObjectOfType<Player>().transform;
                WeightedSteeringBehaviour wsb = new WeightedSteeringBehaviour();
                wsb.steeringBehaviour = sb;
                wsb.weight = 2;
                weightedSteeringBehaviourList.Add(wsb);
                seekWSb = wsb;
            }

            {
                var sb = new AvoidSteeringBehaviour();
                sb.seekTarget = FindObjectOfType<Player>().transform;
                WeightedSteeringBehaviour wsb = new WeightedSteeringBehaviour();
                wsb.steeringBehaviour = sb;
                wsb.weight = 1;
                weightedSteeringBehaviourList.Add(wsb);
            }

            {
                var sb = new SeparationSteeringBehaviour();
                WeightedSteeringBehaviour wsb = new WeightedSteeringBehaviour();
                wsb.steeringBehaviour = sb;
                wsb.weight = 20;
                weightedSteeringBehaviourList.Add(wsb);
            }

            {
                var sb = new ChoesionSteeringBehaviour();
                WeightedSteeringBehaviour wsb = new WeightedSteeringBehaviour();
                wsb.steeringBehaviour = sb;
                wsb.weight = 1;
                weightedSteeringBehaviourList.Add(wsb);
            }

            {
                var sb = new AlignmentSteeringBehaviour();
                WeightedSteeringBehaviour wsb = new WeightedSteeringBehaviour();
                wsb.steeringBehaviour = sb;
                wsb.weight = 50;
                weightedSteeringBehaviourList.Add(wsb);
            }

        }

        void Update()
        {
            if ((this.transform.position - ((SeekSteeringBehaviour)(seekWSb.steeringBehaviour)).seekTarget.transform.position).sqrMagnitude < 10f*10f)
            {
                seekWSb.weight = 2;
            } else
            {
                seekWSb.weight = 0;
            }

            Vector2 totalDesiredAcceleration = Vector2.zero;
            float totalWeight = 0;

            foreach (var wsb in weightedSteeringBehaviourList)
            {
                totalDesiredAcceleration += wsb.steeringBehaviour.ComputeSteering(movement) * wsb.weight;
                totalWeight += wsb.weight;
            }

            totalDesiredAcceleration /= totalWeight;

            movement.steeringTarget.acceleration = totalDesiredAcceleration;

           // Debug.DrawLine(transform.position, transform.position + (Vector3)movement.state.currentVelocity.normalized, Color.white);
        }

    }

}