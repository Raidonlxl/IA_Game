using UnityEngine;

public class Idle<T> : State<T>     
{

    private Transform self;
    public Idle(Transform self)
    {
        this.self = self;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Idle");

    }
    public Vector3 GetDir()
    {
        return Vector3.zero;
    }
}
