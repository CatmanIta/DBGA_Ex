using UnityEngine;
using System.Collections;

namespace AI.SteeringBehaviour
{
    public class AvoidSteeringBehaviour : AbstractSteeringBehaviour
    {
        public Transform seekTarget;
        public float distanceThreshold = 5;

        // Compute the steering contribution of this SteeringBehaviour
        public override Vector2 ComputeSteering(SteeringMovement movement)
        {
            Vector2 targetPosition = seekTarget.transform.position;
            Vector2 myPosition = movement.transform.position;

            Vector2 desiredDirection = (targetPosition - myPosition).normalized;
            desiredDirection = -desiredDirection; // we want to get away

            float desiredSpeed = 0;
            float distance = (targetPosition - myPosition).magnitude;
            if (distance >= distanceThreshold)
            {
                desiredSpeed = 0;
            }
            else
            {
                desiredSpeed = 1 - distance / distanceThreshold;
            }
            desiredSpeed *= movement.maxSpeed;

            Vector2 desiredVelocity = desiredDirection * desiredSpeed;

            Vector2 desiredAcceleration = desiredVelocity - movement.state.currentVelocity;
            desiredAcceleration = Vector2.ClampMagnitude(desiredAcceleration, movement.maxAcceleration);

            return desiredAcceleration;
        }

    }

}