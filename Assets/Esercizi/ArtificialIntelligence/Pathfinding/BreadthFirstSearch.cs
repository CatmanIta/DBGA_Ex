using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI.Pathfinding
{
    public class BreadthFirstSearch : Algorithm
    {

        protected override IEnumerator PerformAlgorithmCO(AlgorithmData data)
        {
            bool result = false;

            Queue<Node> queue = new Queue<Node>();
            List<Node> visitedNodes = new List<Node>();
            queue.Enqueue(data.startNode);
            visitedNodes.Add(data.startNode);
            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                converter.SelectNode(current);
                yield return new WaitForSeconds(0.05f);

                if (current == data.endNode)
                {
                    Debug.Log("WE FOUND THE END NODE!");
                    result = true;
                    break;
                }

                List<Node> neighbours = GetNeighbours(data, current);
                foreach(var neighbour in neighbours)
                {
                    if (!visitedNodes.Contains(neighbour))
                    {
                        queue.Enqueue(neighbour);
                        visitedNodes.Add(neighbour);
                    }
                }
            }

            if (result == false)
            {
                Debug.Log("We could not find a path to the end node.");
            }
        }

    }
}