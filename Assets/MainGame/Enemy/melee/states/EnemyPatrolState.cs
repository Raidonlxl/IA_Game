using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyPatrolState<T> : StatePathfinding<T>
{
    MeleeEnemyModel _model;
    ISteering _patrol;
    ObstacleAvoidance _avoidance;

    public EnemyPatrolState(MeleeEnemyModel model, ISteering patrol, ObstacleAvoidance avoidance, Transform entity, IMove move, Transform target) : base(entity,move,target, avoidance)
    {
        _model = model;
        _patrol = patrol;
        
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
        
        Run(dir1);
    }
    protected override void OnMove(Vector3 dir)
    {
        _move.Move(dir);

    }
    protected override void OnFinishPath()
    {
        base.OnFinishPath();
        ChangeRoulleteValues(_model.NodesValue, _model.NodesKey);
    }
    void ChangeRoulleteValues( List<float> values, List<Node> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 distance = target.transform.position - nodes[i].transform.position;
            Debug.Log("distance mag: " + distance.magnitude);
            if(distance.magnitude < 10)
            {
                values[i] = distance.magnitude + 40;
            } 
            else
            {
                values[i] = distance.magnitude /4;
            }
            Debug.Log("new value: " + values[i]);
        }
        
    }
}
