using UnityEngine;

public interface ISteering
{
    void Refresh(Transform target);
 
    Vector3 GetDir();
}
