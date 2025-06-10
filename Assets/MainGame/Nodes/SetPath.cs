using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class SetPath 
{
 
    private static LayerMask nodeMask = LayerMask.GetMask("Nodes");
    private static LayerMask obsMask = LayerMask.GetMask("Walls");
  
    public static List<Node> SetPathAStarPlus(Transform self, Transform target)
    {
        var init = GetNearNode(self.position);
        var goal = GetNearNode(target.position);

        List<Node> path = ASTAR.Run<Node>(init,(x) => IsSatisfied(x,goal), GetConnections, GetCost, (x)=>Heuristic(x,goal));
        path = ASTAR.CleanPath(path, InView);
        return path;
    }

    static Node GetNearNode(Vector3 position)
    {
        Collider[] nodes = Physics.OverlapSphere(position, 5, nodeMask);

        Node nearNode = null;
        float nearDistance = Mathf.Infinity;
        for (int i = 0; i < nodes.Length; i++)
        {
            var currNode = nodes[i].GetComponent<Node>();
            if (currNode == null) continue;

            var dir = currNode.transform.position - position;
            var currDistance = dir.magnitude;
            if (Physics.Raycast(position, dir.normalized, currDistance, obsMask)) continue;
            if (nearNode == null || nearDistance > currDistance)
            {
                nearNode = currNode;
                nearDistance = currDistance;
            }
        }
       
        return nearNode;
    }

    static bool IsSatisfied(Node curr, Node goal)
    {
        return curr == goal;
    }
    static List<Node> GetConnections(Node curr)
    {
        return curr.neightbourds;
    }
    static float GetCost(Node parent, Node child)
    {
        float cost = 0;
        cost += Vector3.Distance(parent.transform.position, child.transform.position);
      
        return cost;
    }
    static float Heuristic(Node current, Node goal)
    {
        float distanceMultiplier = 1.5f;

        float h = 0;
        h += Vector3.Distance(current.transform.position, goal.transform.position) * distanceMultiplier;
        return h;
    }

    static bool InView(Node grandparent, Node child)
    {
        return InView(grandparent.transform.position, child.transform.position);
    }
    static bool InView(Vector3 grandparent, Vector3 child)
    {
        var diff = child - grandparent;
        return !Physics.Raycast(grandparent, diff.normalized, diff.magnitude, obsMask);
    }

}
