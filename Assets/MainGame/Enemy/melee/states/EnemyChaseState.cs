using Unity.VisualScripting;
using UnityEngine;

public class EnemyChaseState<T> : StatePathfinding<T>
{
    ISteering _persuit;
    MeleeEnemyModel _model;
    ObstacleAvoidance _avoidance;
    Transform _player;

    public EnemyChaseState(Transform player,ISteering persuit, MeleeEnemyModel model, ObstacleAvoidance avoidance, Transform entity, IMove move, Transform target) : base(entity, move, target)
    {
        _persuit = persuit;
        _model = model;
        _avoidance = avoidance;
        _player = player;
    }
    public override void Enter()
    {
        base.Enter();
        target = _player;
        SetPathAStarPlus();
        _model.SetPosition(goal.transform.position);
        _model.ChaseTime();
    }

    public override void Execute()
    {
        base.Execute();
        Vector3 dir1 = _persuit.GetDir();
        Vector3 dir2 = _avoidance.GetDir(dir1);
        Run(dir2);
        
    }

    protected override void OnMove(Vector3 dir)
    {
        base.OnMove(dir);
        _move.Move(dir);
        
    }
}
