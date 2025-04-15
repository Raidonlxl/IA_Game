using UnityEngine;

public class Evade : Persuit
{
    public Evade(Transform self, Rigidbody target) : base(self, target)
    {
    }
    public Evade(Transform self, Rigidbody target, float errorRange = 0, float timePrediction = 0) : base(self, target, errorRange, timePrediction)
    {
    }
    public Evade(Transform self, Rigidbody target, float errorRange = 0) : base(self, target, errorRange)
    {
    }
    public override Vector3 GetDir()
    {
        return -base.GetDir();
    }
}
    