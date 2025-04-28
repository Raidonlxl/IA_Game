using UnityEngine;

public class EnemyDeadState<T> : State<T>
{
    GameObject _self;
    MeleeEnemyModel _model;

    public EnemyDeadState(GameObject self, MeleeEnemyModel model)
    {
        _self = self;
        _model = model;
    }

    public override void Enter()
    {
        base.Enter();
        Object.Destroy(_self, 2f);
    }

    public override void Execute()
    {
        base.Execute();
        _model.Move(new Vector3(0, 1, 0));
    }
}
