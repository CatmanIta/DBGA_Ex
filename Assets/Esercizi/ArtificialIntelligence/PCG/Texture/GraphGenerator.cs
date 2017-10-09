using UnityEngine;
using System.Collections;

namespace AI.PCG
{

    public class GraphGenerator : MonoBehaviour
    {
        private Vector3 startPos;

        void Start()
        {
            startPos = transform.position;
        }

        public float frequency = 1f;
        public float phase = 0f;

        void Update()
        {
            this.transform.position =
                startPos + 
                new Vector3(Time.time,
                Mathf.PerlinNoise(frequency * Time.time + phase, 0),
                0
                );
        }

    }

}