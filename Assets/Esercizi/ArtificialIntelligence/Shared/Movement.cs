using UnityEngine;
using System.Collections;

namespace AI
{

    public class Movement : MonoBehaviour
    {
        public Vector3 velocity;

        public void Update()
        {
            transform.position += velocity * Time.deltaTime;
        }

    }

}