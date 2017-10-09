using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI.Pathfinding
{
    public class VisualEdge
    {
        public VisualNode nodeFrom;
        public VisualNode nodeTo;
        public Color color;

        public override bool Equals(object obj)
        {
            VisualEdge otherEdge = (VisualEdge)obj;
            return this.nodeFrom == otherEdge.nodeFrom && this.nodeTo == otherEdge.nodeTo;
        }

    }

    public class EdgeMaker : MonoBehaviour
    {
        public List<VisualEdge> visualEdges = new List<VisualEdge>();

        public void Awake()
        {
            var allVisualNodes = FindObjectsOfType<VisualNode>();

            foreach (var nodeFrom in allVisualNodes)
            {
                foreach (var nodeTo in allVisualNodes)
                {
                    float distance = (nodeTo.transform.position - nodeFrom.transform.position).magnitude;

                    VisualEdge oppositeEdge = new VisualEdge();
                    oppositeEdge.nodeFrom = nodeTo;
                    oppositeEdge.nodeTo = nodeFrom;
                    oppositeEdge.color = Color.white;

                    if (nodeFrom != nodeTo   // Only if different nodes
                        && distance < 4f    // Only if close enough
                        && !visualEdges.Contains(oppositeEdge)      // Only if the opposite edge is not already in
                        )
                    {

                        // Create the edge
                        VisualEdge edge = new VisualEdge();
                        edge.nodeFrom = nodeFrom;
                        edge.nodeTo = nodeTo;
                        edge.color = Color.white;
                        visualEdges.Add(edge);

                    }
                }
            }
        }

    }

}