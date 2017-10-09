using UnityEngine;
using System.Collections;

namespace AI.SteeringBehaviour
{
    /// <summary>
    /// Keep far from others
    /// </summary>
    public class SeparationSteeringBehaviour : AbstractSteeringBehaviour
    {
        public float distanceThreshold = 2;

        public override Vector2 ComputeSteering(SteeringMovement movement)
        {
            var allBots = GameObject.FindObjectsOfType<AIBot_Steering>();

            // Compute a total desired velocity based on the positions of other bots
            Vector2 totalDesiredVelocity = Vector2.zero;
            foreach (var bot in allBots)
            {
                Vector2 targetPosition = bot.transform.position;
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

                Vector2 desiredVelocity = desiredDirection * desiredSpeed;
                totalDesiredVelocity += desiredVelocity;
            }
            if (allBots.Length <= 1) return Vector2.zero;
            totalDesiredVelocity /= (allBots.Length - 1);   // do not count myself

            totalDesiredVelocity *= movement.maxSpeed;

            Vector2 desiredAcceleration = totalDesiredVelocity - movement.state.currentVelocity;
            desiredAcceleration = Vector2.ClampMagnitude(desiredAcceleration, movement.maxAcceleration);

            return desiredAcceleration;
        }

    }

}