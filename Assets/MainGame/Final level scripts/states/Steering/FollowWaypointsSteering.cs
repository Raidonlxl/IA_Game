using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class FollowWaypointsSteering : ISteering
{
    Transform self;
    Transform target;
    List<Node> path;
    int index = 0;
    bool isBacking;
    List<Node> nodes;
    List<float> weight;
    Dictionary<Node, float> _patrolNodes = new Dictionary<Node, float>();
    public FollowWaypointsSteering(Transform self, Transform target, List<Node> nodes, List<float> weight)
    {
        this.self = self;
        this.target = target;
        this.nodes = nodes;
        this.weight = weight;

        for (int i = 0; i < nodes.Count; i++)
        {
            _patrolNodes[nodes[i]] = weight[i];
        }
        target = MyRandoms.Roulette<Node>(_patrolNodes).transform;

        CalculatePath(target,false);

    }
   
    public void ChangeRoulleteValues()
    {
        if (nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Vector3 distance = target.transform.position - nodes[i].transform.position;
                if (distance.magnitude > 10)
                {
                    weight[i] = distance.magnitude + 40;
                }
                else
                {
                    weight[i] = distance.magnitude / 4;
                }
                _patrolNodes[nodes[i]] = weight[i];
            }
            target = MyRandoms.Roulette<Node>(_patrolNodes).transform;

            CalculatePath(target, false);
            index = 0;
        }
    }

    public Vector3 GetDir()
    {
        if (path != null)
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
                        if (index > 0) index--;
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
                        if (index < path.Count - 1) index++;
                    }
                }
            }
        }

        return (path[index].transform.position - self.position).normalized;

    }
    public void Refresh(Transform target)
    {
        throw new NotImplementedException();
    }

    public void CalculatePath(Transform target, bool isFear)
    {
        isFear = false;

        if (isFear)
        {
            path = SetPath.SetPathAStarPlus(self, target, isFear);
        }
        else
        {
            path = SetPath.SetPathAStarPlus(self, target);
        }
  
    }
}
