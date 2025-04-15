using UnityEngine;

public class Idle : ISteering    
{

    private Transform self;
    public Idle(Transform self)
    {
        this.self = self;
    }
    public Vector3 GetDir()
    {
        return Vector3.zero;
    }
}
