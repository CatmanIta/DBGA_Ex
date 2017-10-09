using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI.Pathfinding
{
    public abstract class Algorithm : MonoBehaviour
    {
        protected GraphConverter converter;

        void Start()
        {
            converter = FindObjectOfType<GraphConverter>();
            converter.currentData = converter.ConvertVisualsToData();
            StartCoroutine(PerformAlgorithmCO(converter.currentData));
        }

        protected abstract IEnumerator PerformAlgorithmCO(AlgorithmData data);


        protected List<Node> GetNeighbours(AlgorithmData data, Node current)
        {
            List<Node> neighbours = new List<Node>();
            foreach (var edge in data.allEdges)
            {
                if (edge.nodeFrom == current)
                {
                    neighbours.Add(edge.nodeTo);
                }
                else if (edge.nodeTo == current)
                {
                    neighbours.Add(edge.nodeFrom);
                }
            }
            return neighbours;
        }

    }
}