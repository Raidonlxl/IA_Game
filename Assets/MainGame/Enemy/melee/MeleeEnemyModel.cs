using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyModel : MonoBehaviour
{
    [SerializeField] EnemyBase _stats;
    [SerializeField] GameObject _hitbox;
    [SerializeField] float _currentLife;
    [SerializeField] bool _isIdling;

    Coroutine AttackCooldown;
    Coroutine IdleCooldown;
    Coroutine PatrolCoolDown;
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

    public void idle()
    {
        float time = Random.Range(5, 16);
        IdleCooldown = StartCoroutine(idlecooldown(time));
    }

    IEnumerator idlecooldown(float time)
    {
        yield return new WaitForSeconds(time);
        IdleCooldown = null;
        _isIdling = false;
    }
    public void patrol()
    {
        float time = Random.Range(10, 21);
        PatrolCoolDown = StartCoroutine(patrolcooldown(time));
    }
    IEnumerator patrolcooldown(float time)
    {
        yield return new WaitForSeconds(time);
        PatrolCoolDown = null;
        _isIdling = true;
    }

    public bool IsIdling => _isIdling;
    public bool IsAttacking => AttackCooldown != null;
    public EnemyBase Stats => _stats;
    public bool IsAlive => _currentLife <= 0;
}
