using UnityEngine;
using System.Collections;

namespace AI
{

    public class Player : MonoBehaviour
    {
        public Movement movement;

        public void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                movement.velocity.y = 5;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                movement.velocity.y = -5;
            }
            else
            {
                movement.velocity.y = 0;
            }


            if (Input.GetKey(KeyCode.D))
            {
                movement.velocity.x = 5;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                movement.velocity.x = -5;
            }
            else
            {
                movement.velocity.x = 0;
            }
        }

    }

}