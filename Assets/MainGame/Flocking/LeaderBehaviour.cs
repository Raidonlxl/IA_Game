using System.Collections.Generic;
using UnityEngine;

public class LeaderBehaviour : FlockingBaseBehaviour
{
    public float timePrediction;
    Persuit _pursuit;
    public bool _isPursuit;
    [SerializeField] public Transform target;
    private void Awake()
    {
        _pursuit = new Persuit(transform,target , timePrediction);



    }
    protected override Vector3 GetRealDir(List<IBoid> boids, IBoid self)
    {
        if (_isPursuit)
        {
           return _pursuit.GetDir() * multiplier;
        }
        return Vector3.zero * multiplier;
    }
}
