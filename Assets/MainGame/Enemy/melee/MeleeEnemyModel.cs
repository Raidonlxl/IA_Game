using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeleeEnemyModel : MonoBehaviour, IMove
{
    [SerializeField] EnemyBase _stats;
    [SerializeField] GameObject _hitbox;
    [SerializeField] float _currentLife;
    [SerializeField] List<Node> _nodeskey = new List<Node>();
    [SerializeField] List<float> _nodesvalue = new List<float>();
    [SerializeField] Vector3 _lastSeenPos = new Vector3(0, 0, 0);
    [SerializeField] float _dirMag;
    [SerializeField] float _distancetopoint;
    private Dictionary<Node, float> _patrolNodes = new Dictionary<Node, float>();
    bool _isIdling;
    
    
    

    Coroutine AttackCooldown;
    Coroutine IdleCooldown;
    Coroutine PatrolCoolDown;
    Coroutine ChaseTimer;
    void Awake()
    {
        
        for (int i = 0; i < _nodeskey.Count; i++)
        {
            if( _nodeskey[i] != null )
            {
                if (_nodesvalue[i] <= 0)
                {
                    float newvalue = Random.Range(2, 11);
                    _nodesvalue[i] = newvalue;
                }
                _patrolNodes.Add(_nodeskey[i], _nodesvalue[i]);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        Vector3 finalDir = dir.normalized;
        
        transform.position += finalDir * _stats.Speed * Time.deltaTime;
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
    public void ChaseTime()
    {
        //es un failsafe para que el enemigo sigua en chase por un rato
        //en caso de que el enemigo pierda por un segundo al player en su LOS
        ChaseTimer = StartCoroutine(chasingtime(3));
    }
    IEnumerator chasingtime(float time)
    {
        yield return new WaitForSeconds(time);
    }
    public void idle()
    {
        float time = 8;
        IdleCooldown = StartCoroutine(idlecooldown(time));
    }

    IEnumerator idlecooldown(float time)
    {
        yield return new WaitForSeconds(time);
        IdleCooldown = null;
        _isIdling = false;
    }
    public Transform patrol()
    {
        //patrol elegira un nodo random y ira a este
        Node targetnode = MyRandoms.Roulette<Node>(_patrolNodes);

        PatrolCoolDown = StartCoroutine(patrolcooldown(8f)); 
        return targetnode.transform;
    }
    IEnumerator patrolcooldown(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("patrol ended");
        PatrolCoolDown = null;
        _isIdling = true;

    }
    

    public void LookDir(Vector3 dir)
    {
        
    }

    public void SetPosition(Vector3 pos)
    {
        _lastSeenPos = pos;
        _lastSeenPos.y = transform.position.y;
        Vector3 dir = _lastSeenPos - transform.position;
        _dirMag = dir.magnitude;
    }

    public void RedoCalculation()
    {
        Vector3 dir = _lastSeenPos - transform.position;
        _dirMag = dir.magnitude;
    }

    public bool IsIdling => _isIdling;
    public bool IsAttacking => AttackCooldown != null;
    public EnemyBase Stats => _stats;
    public bool IsAlive => _currentLife <= 0;
    public bool IsOnLastSeenPos => _dirMag < 0.2f;
    public bool IsChasing => ChaseTimer != null;
}
