using UnityEngine;

public class EnemyIdleState<T> : State<T>
{
    MeleeEnemyModel _model;
   public EnemyIdleState(MeleeEnemyModel model)
    {
        _model = model;
    }

    public override void Enter()
    {
        Debug.Log("IDLE");
        base.Enter();
        _model.idle();
    }

}
