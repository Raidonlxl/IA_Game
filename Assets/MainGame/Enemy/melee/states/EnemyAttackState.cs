using UnityEngine;

public class EnemyAttackState<T> : State<T>
{
    MeleeEnemyModel _model;

    public EnemyAttackState(MeleeEnemyModel model)
    {
        _model = model;
    }

    public override void Enter()
    {
        
        _model.attack();
    }

    public override void Exit()
    {
        base.Exit();
        _model.turnoffhitbox();
    }
}
