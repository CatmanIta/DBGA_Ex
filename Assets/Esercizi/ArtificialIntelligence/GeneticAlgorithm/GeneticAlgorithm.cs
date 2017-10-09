using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI.Genetics
{

    public class GeneticAlgorithm : MonoBehaviour
    {

        void Start()
        {
            StartCoroutine(PerformEvolutionCO());
        }

        public int populationSize = 12;
        public GameObject individualPrefab;
        public int iterations = 100;
        public float waitTime = 0.01f;
        public float mutationRange = 0.1f;
        public float mutationProbability = 0.1f;

        IEnumerator PerformEvolutionCO()
        {
            // Create a random population
            List<Individual> population = new List<Individual>();
            for (int i = 0; i < populationSize; i++)
            {
                var individualGo = Instantiate(individualPrefab);
                var individual = individualGo.GetComponent<Individual>();
                individual.genotype.x = Random.value;
                individual.genotype.y = Random.value;
                individual.genotype.z = Random.value;
                population.Add(individual);
            }


            // Repeat until termination is reached
            int currentInteration = 0;
            while (currentInteration < iterations)
            {

                // Let's see the result
                foreach (var individual in population)
                {
                    individual.transform.position =
                        new Vector3(Random.Range(-4f, +4f), Random.Range(-3f, 3f), 0);
                }
                yield return new WaitForSeconds(waitTime);


                // Evaluate the fitness of the whole population
                foreach (var individual in population)
                {
                    individual.EvaluateFitness();
                }

                // Select the top 50%
                // we sort based on fitness
                population.Sort((x, y) => { return ((x.fitness - y.fitness) < 0 ? 1 : -1); });
                var selectedPopulation = population.GetRange(0, (int)(population.Count * 0.5f));

                // Kill the non selected individuals
                for (int i = (int)(population.Count * 0.5f); i < population.Count; i++)
                {
                    //Debug.Log("KILL " + i);
                    Destroy(population[i].gameObject);
                }


                // Cross the selected population to generate children
                var children = new List<Individual>();
                for (int i = 0; i < populationSize/4; i++)
                {
                    var parent1 = selectedPopulation[i * 2];
                    var parent2 = selectedPopulation[i * 2 + 1];

                    // We create a new individual
                    var individualGo = Instantiate(individualPrefab);
                    var individual = individualGo.GetComponent<Individual>();
                    float alpha = Random.value;
                    individual.genotype.x = parent1.genotype.x * alpha + parent2.genotype.x * (1- alpha);
                    individual.genotype.y = parent1.genotype.y * alpha + parent2.genotype.y * (1 - alpha);
                    individual.genotype.z = parent1.genotype.z * alpha + parent2.genotype.z * (1 - alpha);
                    var child1 = individual;
                    children.Add(child1);

                    individualGo = Instantiate(individualPrefab);
                    individual = individualGo.GetComponent<Individual>();
                    individual.genotype.x = parent1.genotype.x * (1 - alpha) + parent2.genotype.x * alpha;
                    individual.genotype.y = parent1.genotype.y * (1 - alpha) + parent2.genotype.y * alpha;
                    individual.genotype.z = parent1.genotype.z * (1 - alpha) + parent2.genotype.z * alpha;
                    var child2 = individual;
                    children.Add(child2);
                }

                // Mutate some of the children
                foreach(var child in children)
                {
                    if (Random.value < mutationProbability)    // 10%
                    {
                        child.genotype.x = Mathf.Clamp01(child.genotype.x + Random.Range(-mutationRange, mutationRange));
                        child.genotype.y = Mathf.Clamp01(child.genotype.y + Random.Range(-mutationRange, mutationRange));
                        child.genotype.z = Mathf.Clamp01(child.genotype.z + Random.Range(-mutationRange, mutationRange));
                    }
                }

                // Create the next generation
                population.Clear();
                population.AddRange(selectedPopulation);
                population.AddRange(children);
                // Debug.Log(selectedPopulation.Count);
                // Debug.Log(children.Count);

                currentInteration++;
            }

        }
    }

}