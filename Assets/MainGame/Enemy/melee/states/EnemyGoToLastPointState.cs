using UnityEngine;

public class EnemyGoToLastPointState<T> : StatePathfinding<T>
{
    
    MeleeEnemyModel _model;
    ISteering _steer;
    ObstacleAvoidance _avoidance;
    public EnemyGoToLastPointState(MeleeEnemyModel model, ISteering steer, ObstacleAvoidance avoidance, Transform entity, IMove move, Transform target) : base(entity, move, target)
    {
        
        _model = model;
        _steer = steer;
        _avoidance = avoidance;
    }
    public override void Enter()
    {
        base.Enter();
        SetPathAStarPlus();
    }
    public override void Execute()
    {
        base.Execute();
        Vector3 dir1 = _steer.GetDir();
        Vector3 dir2 = _avoidance.GetDir(dir1);
        Run(dir2);
        _model.RedoCalculation();
    }

    protected override void OnMove(Vector3 dir)
    {
        base.OnMove(dir);
        _move.Move(dir);
    }
}
