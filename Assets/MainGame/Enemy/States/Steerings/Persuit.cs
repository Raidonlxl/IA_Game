using UnityEngine;

public class Persuit : ISteering
{
    Transform _self;
    Rigidbody _target;
    float _timePrediction;
    float _errorRange = 0.1f;
    public Persuit(Transform self, Rigidbody target, float errorRange = 0, float timePrediction = 0)
    {
        _self = self;
        _target = target;
        _timePrediction = timePrediction;
    }

    public Persuit(Transform self, Rigidbody target, float errorRange = 0)
    {
        _self = self;
        _target = target;
    }
    public Persuit(Transform self, Rigidbody target)
    {
        _self = self;
        _target = target;
    }

    public virtual Vector3 GetDir()
    {
        Vector3 point = _target.position + _target.linearVelocity * _timePrediction;
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
