using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class SafePoint<T> : State<T>
{
    private Transform target;
    private EnemyModel self;
    private List<Node> nodes;
    ISteering steering;
    Transform selected;
    GenericBehaviour generic;
    ISteering flocking;
    Timer timer;
    public SafePoint(GenericBehaviour generic,ISteering steering, ISteering flocking, EnemyModel self, Transform target,List<Node> nodes)
    {
        this.generic = generic;
        this.flocking = flocking;
        this.target = target;
        this.self = self;
        this.nodes = nodes;
        this.steering = steering;
        timer = new Timer(0, 15);
    }

    public override void Enter()
    {
        base.Enter();
        float x = 0f;

        Debug.Log("Safe");
        for (int i = 0; i < nodes.Count; i++)
        {
            float a = Vector3.Distance(self.transform.position, nodes[i].transform.position);
            if (a < 20)
            {
                var direction = target.position - nodes[i].transform.position;
                if (!Physics.Raycast(nodes[i].transform.position, direction.normalized, 20, 10) && a > x)
                {
                    x = a;

                    selected = nodes[i].transform;
                }
            }

        }
        if (selected == null)
        {
            selected= nodes[nodes.Count-1].transform;
        }

        steering.Refresh(selected);
       

    }




    public override void Execute()
    {
        base.Execute();
        if (Vector3.Distance(self.transform.position, selected.position) < 2)
        {
            timer.Run();
            if (timer.IsCompleted())
            {
                self.healthController.GetHeal();
                self.isHealed = true;
            }
        }
        else
        {
            generic.Dir = steering.GetDir();
            self.Move(flocking.GetDir());
        }

    }
}
