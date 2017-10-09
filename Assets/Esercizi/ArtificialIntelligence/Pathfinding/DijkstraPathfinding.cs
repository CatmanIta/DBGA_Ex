using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI.Pathfinding
{
    public class DijkstraPathfinding : Algorithm
    {

        protected override IEnumerator PerformAlgorithmCO(AlgorithmData data)
        {
            List<Node> unvisitedNodes = new List<Node>();      // nodes we need to check

            // Initialise all nodes to infinity and put them in the unvisited list
            foreach(var node in data.allNodes)
            {
                unvisitedNodes.Add(node);
                node.pathWeight = Mathf.Infinity;
            }

            // Initialise the start node
            Node current = data.startNode;
            data.startNode.pathWeight = 0;
            converter.SelectNode(current);

            while (true)
            {
                List<Node> neighbours = GetNeighbours(data, current);
                foreach(var neighbour in neighbours)
                {
                    if (unvisitedNodes.Contains(neighbour))
                    {
                        float tentativeWeight = current.pathWeight + 1;     // 1 is the weight of the segment connecting the two nodes

                        // we compare the tentative weight with the one we already set for that neighbour
                        if (tentativeWeight < neighbour.pathWeight)
                        {
                            neighbour.pathWeight = tentativeWeight;
                        }
                    }
                }

                // we remove the visited node
                unvisitedNodes.Remove(current);

                // let's check whether we finished the path
                if (current == data.endNode)
                {
                    Debug.Log("WE FOUND THE SHORTEST PATH " + current.pathWeight);
                    break;
                }


                // let's choose the node with the minimum weight
                float minWeight = Mathf.Infinity;
                foreach(var node in unvisitedNodes)
                {
                    if (node.pathWeight < minWeight)
                    {
                        minWeight = node.pathWeight;
                        current = node;
                    }
                }

                converter.SelectNode(current, new Color( current.pathWeight/15f, 1-current.pathWeight/15f, 0) );
                yield return new WaitForSeconds(0.05f);
            }

            // Find the shortest path
            List<Node> shortestPath = new List<Node>();
            shortestPath.Add(data.endNode);
            current = data.endNode;
            converter.SelectNode(current, new Color(0f, 0f, 1f));
            while (true)
            {
                // Find the lowest weight neighbour (which is the one that will take me to the start node at minimum cost)
                float minWeight = Mathf.Infinity;
                Node minNeighbour = null;
                foreach (var neighbour in GetNeighbours(data, current))
                {
                    if (neighbour.pathWeight < minWeight)
                    {
                        minWeight = neighbour.pathWeight;
                        minNeighbour = neighbour;
                    }
                }

                shortestPath.Add(minNeighbour);

                current = minNeighbour;
                converter.SelectNode(current, new Color(0f,0f,1f));
                yield return new WaitForSeconds(0.05f);

                if (current == data.startNode)
                {
                    break;
                }
            }




        }

    }
}