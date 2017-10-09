using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI.Pathfinding
{
    public class DepthFirstSearch : Algorithm
    {

        protected override IEnumerator PerformAlgorithmCO(AlgorithmData data)
        {
            bool result = false;

            Stack<Node> stack = new Stack<Node>();
            List<Node> visitedNodes = new List<Node>();
            stack.Push(data.startNode);
            visitedNodes.Add(data.startNode);
            while (stack.Count > 0)
            {
                Node current = stack.Pop();

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
                        stack.Push(neighbour);
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