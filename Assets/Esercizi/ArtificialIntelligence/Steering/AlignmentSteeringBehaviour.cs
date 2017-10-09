using UnityEngine;
using System.Collections;

namespace AI.SteeringBehaviour
{
    /// <summary>
    /// Try to match the velocity of others
    /// </summary>
    public class AlignmentSteeringBehaviour : AbstractSteeringBehaviour
    {
        public float distanceThreshold = 5;

        public override Vector2 ComputeSteering(SteeringMovement movement)
        {
            var allBots = GameObject.FindObjectsOfType<AIBot_Steering>();

            Vector2 myPosition = movement.transform.position;

            // Compute an average velocity from all bots
            Vector2 avarageVelocity = Vector2.zero;
            int insideCircleCount = 0;
            foreach (var bot in allBots)
            {
                Vector2 targetPosition = bot.transform.position;


                Vector2 toTargetDir = (targetPosition - myPosition).normalized;
                Vector2 currentDir = movement.state.currentVelocity.normalized;

                if (Mathf.Abs(Vector2.Angle(toTargetDir, currentDir)) > 30)
                {
                    continue;
                }


                float botDistance = (targetPosition - myPosition).magnitude;
                if (botDistance <= distanceThreshold && botDistance > 0)
                {
                    avarageVelocity += bot.movement.state.currentVelocity;
                    insideCircleCount += 1;
                }
            }
            if (insideCircleCount == 0) return Vector2.zero;
            avarageVelocity /= insideCircleCount;

            Vector2 desiredVelocity = avarageVelocity;

            Vector2 desiredAcceleration = desiredVelocity - movement.state.currentVelocity;
            desiredAcceleration = Vector2.ClampMagnitude(desiredAcceleration, movement.maxAcceleration);

           // Debug.DrawLine(movement.transform.position, movement.transform.position + (Vector3)desiredVelocity, Color.cyan);

            return desiredAcceleration;
        }

    }

}