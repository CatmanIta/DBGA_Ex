using UnityEngine;
using System.Collections;

namespace AI.Genetics
{
    public class Individual : MonoBehaviour
    {

        // genotype (3 values for the color)
        public Vector3 genotype = new Vector3();
        

        public void Start()
        {
            // phenotype: genotype mapped to the color of the sprite
            GetComponent<SpriteRenderer>().color
                = new Color(genotype.x, genotype.y, genotype.z, 1);
        }



        public float fitness = 0;

        public void EvaluateFitness()
        {
            Color environmentColor = Camera.main.backgroundColor;

            float dR = Mathf.Abs(environmentColor.r - genotype.x);
            float dG = Mathf.Abs(environmentColor.g - genotype.y);
            float dB = Mathf.Abs(environmentColor.b - genotype.z);

            this.fitness = (1 - dR) * (1 - dG) * (1 - dB);
        }


    }

}