using UnityEngine;

public interface ISteering
{
    void Refresh(Transform target);

    void CalculatePath(Transform target, bool isFear);
    Vector3 GetDir();
}
