using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [Header("LineOfSight")]
    public EnemyBase enemyBase;
    public Transform target;

    private void Start()
    {
    }
     private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyBase.Range);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, enemyBase.Angle / 2, 0) * transform.forward * enemyBase.Range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -enemyBase.Angle / 2, 0) * transform.forward * enemyBase.Range);
    }
}
