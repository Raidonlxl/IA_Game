using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField]Transform self;
    [SerializeField] Transform target;
    [SerializeField] float range;
    [SerializeField] float angle;
    [SerializeField] LayerMask obsMask;
   
    public void Initialize(Transform self, Transform target, float range, float angle, LayerMask obsMask) 
    {
        this.self = self;
        this.target = target;
        this.range = range;
        this.angle = angle;
        this.obsMask = obsMask;
    }
    public static bool CheckRange(Transform self, Transform target, float range)
    {
        Vector3 dir = target.position - self.position;
        float distance = dir.magnitude;
        return distance <= range;
    }

    public static bool CheckAngle(Transform self, Transform target, float angle)
    {
        return CheckAngle(self, target, self.forward, angle);
    }
    public static bool CheckAngle(Transform self, Transform target, Vector3 front, float angle)
    {
        Vector3 dir = target.position - self.position;
        float angleToTarget = Vector3.Angle(front, dir);
        return angleToTarget <= angle / 2;
    }
    public static bool CheckView(Transform self, Transform target, LayerMask obsMask)
    {
        Vector3 dir = target.position - self.position;
        return !Physics.Raycast(self.position, dir.normalized, dir.magnitude, obsMask);
    }

    public bool LOS()
    {
        return CheckRange(self, target, range)
            && CheckAngle(self, target, angle)
            && CheckView(self, target, obsMask);
    }

   
}
