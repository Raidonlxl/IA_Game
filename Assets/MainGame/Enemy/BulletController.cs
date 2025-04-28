using UnityEngine;

public class BulletController : MonoBehaviour
{

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 5;
    }
}
