using UnityEngine;

public class EnemyAttackState<T> : State<T>
{
    MeleeEnemyModel _model;
    PlayerModel _player;
    int damage = 10;

    public EnemyAttackState(MeleeEnemyModel model, PlayerModel playerModel)
    {
        _model = model;
        _player = playerModel;

    }

    public override void Enter()
    {
        
        _model.attack();
        _player.healthController.GetDamage(damage);
    }

    public override void Exit()
    {
        base.Exit();
        _model.turnoffhitbox();
    }
}
