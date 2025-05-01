using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class EnemyModel : MonoBehaviour
{
    [Header("LineOfSight")]
    public EnemyBase enemyBase;
    public GameObject target;
    public PlayerModel playerModel;
    [SerializeField]
    private ObstacleAvoidance obs;
    public bool isReady;
    public GameObject pointToShoot;
    public GameObject bullet;
    private void Start()
    {
        playerModel = target.GetComponent<PlayerModel>();
        obs = gameObject.GetComponent<ObstacleAvoidance>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyBase.Range);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, enemyBase.Angle / 2, 0) * transform.forward * enemyBase.Range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -enemyBase.Angle / 2, 0) * transform.forward * enemyBase.Range);
    }
    public void Move(Vector3 direction)
    {
        direction = obs.GetDir(direction);
        transform.position += direction * enemyBase.Speed * Time.deltaTime;
        RotateEnemy(direction);
    }
    
    public void RotateEnemy(Vector3 direction)
    {
        transform.forward = Vector3.Lerp(transform.forward,direction.normalized,0.2f);
    }

    public void Shoot(GameObject bullet)
    {
        bullet.GetComponent<BulletController>();
        bullet.transform.position = pointToShoot.transform.position;
        bullet.transform.rotation = pointToShoot.transform.rotation;
    }
}
