using System;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathSteering : ISteering
{
    Transform self;
    Transform target;
    List<Node> path;
    int index = 0;
    public FollowPathSteering(Transform self, Transform target)
    {
        this.self = self;
        this.target = target;
        CalculatePath(target, false);

    }
    public FollowPathSteering(Transform self)
    {
        this.self = self;
    }

    public void CalculatePath(Transform target, bool isFear)
    {
       
        if (isFear)
        {
            path = SetPath.SetPathAStarPlus(self, target, isFear);
        }
        else
        {

            path = SetPath.SetPathAStarPlus(self, target);
        }
    }

    public List<Node> StartPath(Transform target, bool isFear)
    {
        if (isFear)
        {
            path = SetPath.SetPathAStarPlus(self, target,isFear);
        }
        else
        {
            path = SetPath.SetPathAStarPlus(self, target);
        }

        return path;
    }
    public Vector3 GetDir()
    {
        if (Vector3.Distance(self.position, path[index].transform.position) < 0.5f)
        {
            if (index < path.Count - 1)
            {
                index++;
                //Debug.Log("no llegue, index " + index);
            }
            else
            {
               
                //Debug.Log("llegue, index " + index);
                //ChangeRoulleteValues();
            }
        }
    
        return (path[index].transform.position - self.position).normalized;
    }

    public void Refresh(Transform target)
    {
        throw new System.NotImplementedException();
    }
}
