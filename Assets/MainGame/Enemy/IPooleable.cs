using UnityEngine;

public interface IPooleable 
{
    void Recycle(GameObject obj);
}
