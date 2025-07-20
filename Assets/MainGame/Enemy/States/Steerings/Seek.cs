using UnityEngine;

public class Seek : ISteering
{
    Transform _self;
    Transform _target;
    public Seek(Transform self, Transform target)
    {
        _self = self;
        _target = target;
    }

    public void CalculatePath(Transform target, bool isFear)
    {
        throw new System.NotImplementedException();
    }

    public virtual Vector3 GetDir()
    {
        return (_target.position - _self.position).normalized;
    }

    public void Refresh(Transform target)
    {
        
    }

}
