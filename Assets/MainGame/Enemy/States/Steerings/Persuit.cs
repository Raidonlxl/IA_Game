using NUnit.Framework;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class Persuit : ISteering
{
    Transform _self;
    Transform _target;
    float _timePrediction;
    float _errorRange = 0.1f;
    float currentSpeedTarget;
    List<Node> _path;
    public Persuit(Transform self, Transform target,float currentSpeedTarget, float errorRange = 0, float timePrediction = 0)
    {
        _self = self;
        _target = target;
        _timePrediction = timePrediction;
        this.currentSpeedTarget = currentSpeedTarget;
    }

    public Persuit(Transform self, Transform target,float currentSpeedTarget, float errorRange = 0)
    {
        _self = self;
        _target = target;
        this.currentSpeedTarget=currentSpeedTarget;
    }
    public Persuit(Transform self, Transform target, float currentSpeedTarget)
    {
        _self = self;
        _target = target;
        this.currentSpeedTarget=currentSpeedTarget;
    }
    public Persuit(Transform self, Transform target)
    {
        _self = self;
        _target = target;
     
    }

    public virtual Vector3 GetDir()
    {
        Vector3 point = _target.position + _target.position.normalized * currentSpeedTarget * _timePrediction;
        Vector3 dirToPoint = (point - _self.position).normalized;
        Vector3 dirToTarget = (_target.position - _self.position).normalized;

        if (Vector3.Dot(dirToPoint, dirToTarget) < 0 + _errorRange)
        {
            return dirToTarget;
        }
        else
        {
            return dirToPoint;
        }
    }

    public void Refresh(Transform target)
    {
        _path = SetPath.SetPathAStarPlus(_self, target);
    }


    public float TimePrediction
    {
        get
        {
            return _timePrediction;
        }
        set
        {
            _timePrediction = value;
        }
    }
}
