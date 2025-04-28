using UnityEngine;

public class PatrolState<T> : State<T>
{
    ISteering steering;
    EnemyModel enemyModel;

    public PatrolState(ISteering steering, EnemyModel enemyModel)
    {
        this.steering = steering;
        this.enemyModel = enemyModel;
    }

    public override void Enter()
    {  
        base.Enter();

    }
    public override void Execute()
    {
        base.Execute();

       
        enemyModel.Move(steering.GetDir());

    }
    public void ChangeSteering(ISteering newSteering)
    {
        steering = newSteering;
    }
}
