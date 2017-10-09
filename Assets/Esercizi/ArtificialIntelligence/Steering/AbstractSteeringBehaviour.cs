using UnityEngine;
using System.Collections;

namespace AI.SteeringBehaviour
{

    public abstract class AbstractSteeringBehaviour
    {
        public abstract Vector2 ComputeSteering(SteeringMovement movement);
    }

}