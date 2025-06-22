using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToWaypoints : ISteering
{
    private Transform self;
    private Transform target;
    private bool isBacking;
    private bool canBack;
    private bool endWay;
    private int index;
    List<Node> path;
    List<Node> nodes;
    List<float> weight;

    public MoveToWaypoints(Transform self, Transform target, bool canBack)
    {
        this.self = self;
        this.target = target;
        index = 0;
        isBacking = false;
        this.canBack = canBack;
        Refresh(target);
    }
    public MoveToWaypoints(Transform self, List<Node> nodes,List<float> weight, bool canBack)
    {
        this.self = self;
        this.nodes = nodes;
        index = 0;
        isBacking = false;
        this.canBack = canBack;
        this.weight = weight;
        Refresh(target);
        ChangeRoulleteValues(nodes, weight);
    }
    public void Refresh(Transform target)
    {
        path = SetPath.SetPathAStarPlus(self, target);
    }
    public void ChangeRoulleteValues(List<Node> nodes, List<float> weight)
    {
        
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 distance = target.transform.position - nodes[i].transform.position;
            if (distance.magnitude < 10)
            {
                weight[i] = distance.magnitude + 40;
            }
            else
            {
                weight[i] = distance.magnitude / 4;
            }
        }

    }
    public Vector3 GetDir()
    {
        if (canBack)
        {
            if (Vector3.Distance(self.position, path[index].transform.position) < 0.5f)
            {

                if (!isBacking)
                {
                    if (index < path.Count - 1)
                    {
                        index++;
                    }
                    else
                    {
                        isBacking = true;
                        index--;
                    }
                }
                else
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        isBacking = false;
                        index++;
                    }
                }
            }
        }

        else if (!canBack && !endWay)
        {
            if (Vector3.Distance(self.position, path[index].transform.position) < 0.5f)
            {
                if (index < path.Count - 1)
                {
                    index++;
                }
                else
                {
                    endWay = true;
                    
                }
            }
        }
        return (path[index].transform.position - self.position).normalized;
    }
}