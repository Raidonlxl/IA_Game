using UnityEngine;

public class EnemyPatrolState<T> : State<T>
{
    MeleeEnemyModel _model;
    ISteering _patrol;
    ObstacleAvoidance _avoidance;
    
    public EnemyPatrolState(MeleeEnemyModel model, ISteering patrol, ObstacleAvoidance avoidance)
    {
        _model = model;
        _patrol = patrol;
        _avoidance = avoidance;
    }
    public override void Enter()
    {
        base.Enter();
        _model.patrol();
    }

    public override void Execute()
    {
        base.Execute();
        Vector3 dir1 = _patrol.GetDir();
        Vector3 dir2 = _avoidance.GetDir(dir1);
        _model.Move(dir2);
    }
}
