using Unity.VisualScripting;
using UnityEngine;

public class EnemyChaseState<T> : StatePathfinding<T>
{
    ISteering _persuit;
    MeleeEnemyModel _model;
    GenericBehaviour _generic;
    Transform _player;

    public EnemyChaseState(Transform player,ISteering persuit, MeleeEnemyModel model, ObstacleAvoidance avoidance, Transform entity, IMove move, Transform target) : base(entity, move, target, avoidance)
    {
        _persuit = persuit;
        _model = model;
        
        _player = player;
    }
    public override void Enter()
    {
        base.Enter();
        target = _player;
        SetPathAStarPlus();
        _model.SetPosition(goal.transform.position);
    }

    public override void Execute()
    {
        base.Execute();
        Vector3 dir1 = _persuit.GetDir();
        Run(dir1);
        
    }
    public override void Exit()
    {
        base.Exit();
        _model.ChaseTime();
    }

    protected override void OnMove(Vector3 dir)
    {
        base.OnMove(dir);
        _move.Move(dir);
        
    }
}
