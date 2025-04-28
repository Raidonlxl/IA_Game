using UnityEngine;

public class EnemyChaseState<T> : State<T>
{
    ISteering _persuit;
    MeleeEnemyModel _model;
    ObstacleAvoidance _avoidance;

    public EnemyChaseState(ISteering persuit, MeleeEnemyModel model, ObstacleAvoidance avoidance)
    {
        _persuit = persuit;
        _model = model;
        _avoidance = avoidance;
    }

    public override void Execute()
    {
        base.Execute();
        Vector3 dir1 = _persuit.GetDir();
        Vector3 dir2 = _avoidance.GetDir(dir1);
        _model.Move(dir2);
    }
}
