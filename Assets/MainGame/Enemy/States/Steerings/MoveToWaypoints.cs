using System;
using System.Collections.Generic;
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
    public MoveToWaypoints(Transform self, Transform target, bool canBack)
    {
        this.self = self;
        this.target = target;
        index = 0;
        isBacking = false;
        this.canBack = canBack;
        Refresh(target);
    }
    public void Refresh(Transform target)
    {
        path = SetPath.SetPathAStarPlus(self, target);
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