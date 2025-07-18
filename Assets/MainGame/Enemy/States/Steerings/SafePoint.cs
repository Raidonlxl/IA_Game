
using UnityEngine;
using System.Collections.Generic;

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
        timer = new Timer(0, 5);
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Safe");
        SetPath.SetPathAStarPlus(self.transform, target, true);

    }

    public override void Execute()
    {
        base.Execute();


        generic.Dir = steering.GetDir();
        self.Move(flocking.GetDir());
     

    }
}
