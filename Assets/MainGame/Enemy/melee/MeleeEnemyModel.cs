using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeleeEnemyModel : MonoBehaviour, IBoid
{
    [SerializeField] EnemyBase _stats;
    [SerializeField] GameObject _hitbox;
    [SerializeField] float _currentLife;
    public Transform Target;
    public List<Node> Nodeskey = new List<Node>();
    public List<float> Nodesvalue = new List<float>();
    public Transform LastSeenPos;
    [SerializeField] float _dirMag;
    [SerializeField] float _distancetopoint;
    [SerializeField] private ObstacleAvoidance _obs;
    public HealthController _healthController;
    public PlayerModel PlayerModel;
    public GenericBehaviour Instance;
    
    
    

    Coroutine AttackCooldown;
    Coroutine IdleCooldown;
    Coroutine ChaseTimer;
    void Awake()
    {
        _obs = GetComponent<ObstacleAvoidance>();
        _healthController = GetComponent<HealthController>();
       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _healthController.SetMaxLife(_stats.MaxLife);
        _currentLife = _stats.MaxLife;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _stats.Range);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, _stats.Angle / 2, 0) * transform.forward * _stats.Range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -_stats.Angle / 2, 0) * transform.forward * _stats.Range);
    }
    public void Move(Vector3 dir)
    {
        dir.y = 0;


         Vector3 finalDir = _obs.GetDir(dir);
        transform.position += finalDir *_stats.Speed * Time.deltaTime;
        RotateEnemy(finalDir);
    }
    public void RotateEnemy(Vector3 direction)
    {
        transform.forward = Vector3.Lerp(transform.forward, direction.normalized, 0.2f);
    }
    public void turnoffhitbox()
    {
        _hitbox.SetActive(false);
    }
    public void attack()
    {
        _hitbox.SetActive(true);
        AttackCooldown = StartCoroutine(attackcooldown(5));

    }

    IEnumerator attackcooldown(float time)
    {
        yield return new WaitForSeconds(time);
        _hitbox.SetActive(false);
        AttackCooldown = null;
    }
      

    public bool IsIdling => IdleCooldown != null;
    public bool IsAttacking => AttackCooldown != null;
    public EnemyBase Stats => _stats;
    public bool IsAlive => _currentLife <= 0;
    public bool IsOnLastSeenPos => _dirMag < 0.2f;
    public bool IsChasing => ChaseTimer != null;
    public List<Node> NodesKey => Nodeskey;
    public List<float> NodesValue => Nodesvalue;

    public Vector3 Position => transform.position;

    public Vector3 Forward => transform.forward;
}
