
using UnityEngine;
using System.Collections.Generic;

public class SafePointState<T> : State<T>
{
    private Transform target;
    private BaseModel self;
    ISteering steering;
    GenericBehaviour genericBehaviour;
    ISteering flocking;
    public SafePointState(GenericBehaviour genericBehaviour, ISteering steering, ISteering flocking, BaseModel self, Transform target)
    {
        this.genericBehaviour = genericBehaviour;
        this.flocking = flocking;
        this.target = target;
        this.self = self;
        this.steering = steering;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Safe");

        steering.CalculatePath(target.transform, true);

        genericBehaviour.IsActive = true;
    }

    public override void Execute()
    {
        base.Execute();
        genericBehaviour.Dir = steering.GetDir();
        self.Move(flocking.GetDir());
    }

    public override void Exit()
    {
        base.Exit();
        genericBehaviour.IsActive = false;

    }
}
