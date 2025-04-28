using UnityEngine;

public class MeleeEnemyView : MonoBehaviour
{
    [SerializeField] Animator _Animator;

    [SerializeField] MeleeEnemyModel _Model;

    private void Start()
    {
        
    }

    private void Dead()
    {
        _Animator.SetBool("dead", true);
    } 

    private void Attack()
    {
        _Animator.SetTrigger("attack");
    }
}
