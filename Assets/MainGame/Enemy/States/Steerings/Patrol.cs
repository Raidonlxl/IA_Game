using System.Diagnostics;
using UnityEngine;

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

        if (Vector3.Distance(self.position, waypoints[index].position) < 2)
        {

            if (!isBacking)
            {
                if (index < waypoints.Length - 1)
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
        return (waypoints[index].position - self.position).normalized;


    }
}