using UnityEngine;

public class Evade : Persuit
{
  
    public Evade(Transform self, Transform target, float errorRange = 0, float timePrediction = 0) : base(self, target.transform, errorRange, timePrediction)
    {
    }
    public Evade(Transform self, Transform target, float errorRange = 0) : base(self, target.transform, errorRange)
    {
    }
    public Evade(Transform self, Transform target) : base(self, target.transform)
    {
    }
    public override Vector3 GetDir()
    {
        return -base.GetDir();
    }
}
    