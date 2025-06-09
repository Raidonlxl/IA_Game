using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyPatrolState<T> : StatePathfinding<T>
{
    MeleeEnemyModel _model;
    ISteering _patrol;
    ObstacleAvoidance _avoidance;

    public EnemyPatrolState(MeleeEnemyModel model, ISteering patrol, ObstacleAvoidance avoidance, Transform entity, IMove move, Transform target) : base(entity,move,target)
    {
        _model = model;
        _patrol = patrol;
        _avoidance = avoidance;
    }
    public override void Enter()
    {
        base.Enter();
        target = _model.patrol();
      
        SetPathAStarPlus();
    }

    public override void Execute()
    {
        base.Execute();
        Vector3 dir1 = _patrol.GetDir();
        Vector3 dir2 = _avoidance.GetDir(dir1);
        Run(dir2);
    }
    protected override void OnMove(Vector3 dir)
    {
        _move.Move(dir);

    }
}
