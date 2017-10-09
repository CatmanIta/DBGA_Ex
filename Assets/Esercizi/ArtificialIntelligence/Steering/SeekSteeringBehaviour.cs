using UnityEngine;
using System.Collections;

namespace AI.SteeringBehaviour
{
    public class SeekSteeringBehaviour : AbstractSteeringBehaviour
    {
        public Transform seekTarget;

        // Compute the steering contribution of this SteeringBehaviour
        public override Vector2 ComputeSteering(SteeringMovement movement)
        {
            Vector2 targetPosition = seekTarget.transform.position;
            Vector2 myPosition = movement.transform.position;

            Vector2 desiredDirection = (targetPosition - myPosition).normalized;
            Vector2 desiredVelocity = desiredDirection * movement.maxSpeed;

            Vector2 desiredAcceleration = desiredVelocity - movement.state.currentVelocity;
            desiredAcceleration = Vector2.ClampMagnitude(desiredAcceleration, movement.maxAcceleration);

            return desiredAcceleration;
        }

    }

}