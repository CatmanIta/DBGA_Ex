using UnityEngine;
using System.Collections;

namespace AI.SteeringBehaviour
{
    /// <summary>
    /// Get close to the middle of the formation
    /// </summary>
    public class ChoesionSteeringBehaviour : AbstractSteeringBehaviour
    {
        public float distanceThreshold = 2;

        public override Vector2 ComputeSteering(SteeringMovement movement)
        {
            var allBots = GameObject.FindObjectsOfType<AIBot_Steering>();

            Vector2 myPosition = movement.transform.position;

            // Compute an average position based on the positions of other bots
            Vector2 averagePosition = Vector2.zero;
            int insideCircleCount = 0;
            foreach (var bot in allBots)
            {
                Vector2 targetPosition = bot.transform.position;


                Vector2 toTargetDir = (targetPosition - myPosition).normalized;
                Vector2 currentDir = movement.state.currentVelocity.normalized;

                if (Mathf.Abs( Vector2.Angle(toTargetDir,currentDir)) > 30)
                {
                    continue;
                }

                float botDistance = (targetPosition - myPosition).magnitude;
                if (botDistance <= distanceThreshold && botDistance > 0)
                {
                    averagePosition += targetPosition;
                    insideCircleCount += 1;
                }
            }
            if (insideCircleCount == 0) return Vector2.zero;
            averagePosition /= insideCircleCount;

            Vector2 desiredDirection = (averagePosition - myPosition).normalized;
            //desiredDirection = -desiredDirection; // we want to get away
            float desiredSpeed = 0;
            float distance = (averagePosition - myPosition).magnitude;
            if (distance >= distanceThreshold)
            {
                desiredSpeed = 0;
            }
            else
            {
                desiredSpeed = distance / distanceThreshold;
            }

            Vector2 desiredVelocity = desiredDirection * desiredSpeed;
            desiredVelocity *= movement.maxSpeed;

            Vector2 desiredAcceleration = desiredVelocity - movement.state.currentVelocity;
            desiredAcceleration = Vector2.ClampMagnitude(desiredAcceleration, movement.maxAcceleration);

            //Debug.DrawLine(movement.transform.position, averagePosition, Color.black);

            return desiredAcceleration;
        }

    }

}