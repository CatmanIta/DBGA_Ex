using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace AI.Pathfinding
{

    // Data structures
    public class Node
    {
        public float pathWeight;        // cost until that node
        public float tentativeWeight;   // estimated cost (path + heuristic)
    }

    public class Edge
    {
        public Node nodeFrom;
        public Node nodeTo;
    }

    public class AlgorithmData
    {
        public List<Node> allNodes;
        public List<Edge> allEdges;
        public Node startNode;
        public Node endNode;
    }


    public class GraphConverter : MonoBehaviour
    {
        // Dictionaries that define the mapping Visual-to-Logic
        public Dictionary<VisualNode, Node> visualNodeToNodeDict = new Dictionary<VisualNode, Node>();
        public Dictionary<Node, VisualNode> nodeToVisualNodeDict = new Dictionary<Node, VisualNode>();

        public Dictionary<VisualEdge, Edge> visualEdgeToEdgeDict = new Dictionary<VisualEdge, Edge>();
        public Dictionary<Edge, VisualEdge> edgeToVisualEdgeDict = new Dictionary<Edge, VisualEdge>();

        public AlgorithmData currentData;

        #region Visualization

        public void SelectNode(Node node, Color color)
        {
            VisualNode visualNode = nodeToVisualNodeDict[node];
            visualNode.GetComponent<SpriteRenderer>().color = color;
        }

        public void SelectNode(Node node)
        {
            SelectNode(node, Color.red);
        }

        public void UnselectNode(Node node)
        {
            VisualNode visualNode = nodeToVisualNodeDict[node];
            visualNode.GetComponent<SpriteRenderer>().color = Color.white;
        }

        public void SelectEdge(Edge edge)
        {
            VisualEdge visualEdge = edgeToVisualEdgeDict[edge];
            visualEdge.color = Color.red;
        }

        public void UnselectEdge(Edge edge)
        {
            VisualEdge visualEdge = edgeToVisualEdgeDict[edge];
            visualEdge.color = Color.white;
        }

        public void Update()
        {
            foreach (var edge in currentData.allEdges)
            {
                VisualEdge visualEdge = edgeToVisualEdgeDict[edge];
                Debug.DrawLine(visualEdge.nodeFrom.transform.position, visualEdge.nodeTo.transform.position, visualEdge.color);
            }
        }

        #endregion

        #region Conversion

        public AlgorithmData ConvertVisualsToData()
        {
            AlgorithmData data = new AlgorithmData();

            var visualNodes = FindObjectsOfType<VisualNode>();
            data.allNodes = new List<Node>();
            foreach(var visualNode in visualNodes)
            {
                Node newNode = new Node();
                data.allNodes.Add(newNode);
                if (visualNode.isStartNode) data.startNode = newNode;
                if (visualNode.isEndNode) data.endNode = newNode;
                visualNodeToNodeDict[visualNode] = newNode;
                nodeToVisualNodeDict[newNode] = visualNode;
            }

            var visualEdges = FindObjectOfType<EdgeMaker>().visualEdges;
            data.allEdges = new List<Edge>();
            foreach (var visualEdge in visualEdges)
            {
                Edge newEdge = new Edge();
                data.allEdges.Add(newEdge);

                newEdge.nodeFrom = visualNodeToNodeDict[visualEdge.nodeFrom];
                newEdge.nodeTo = visualNodeToNodeDict[visualEdge.nodeTo];

                visualEdgeToEdgeDict[visualEdge] = newEdge;
                edgeToVisualEdgeDict[newEdge] = visualEdge;
            }

            return data;
        }

        #endregion

    }

}