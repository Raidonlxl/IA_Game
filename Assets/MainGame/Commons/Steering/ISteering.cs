using UnityEngine;

public interface ISteering
{
    void Refresh(Transform target);
    void Refresh();
    Vector3 GetDir();
}
