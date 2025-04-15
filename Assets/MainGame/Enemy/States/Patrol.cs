using System.Diagnostics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Patrol : ISteering
{
    private Transform self;
    private Transform[] waypoints;
    private bool isBacking;

    private int index;
    public Patrol(Transform self, Transform[] waypoints)
    {
        this.self = self;
        this.waypoints = waypoints;
        index = 0;
        isBacking = false;
    }

    public Vector3 GetDir()
    {

        if (Vector3.Distance(self.position, waypoints[index].position) < 5)
        {

            if (!isBacking)
            {
                index++;
                if (index > waypoints.Length - 1)
                {
                    isBacking = !isBacking;
                }
            }

            else
            {
                index--;

                if (index <= 0)
                {

                    isBacking = !isBacking; 

                }
            }
        }
        
        return (waypoints[index].position - self.position).normalized;
    }
}