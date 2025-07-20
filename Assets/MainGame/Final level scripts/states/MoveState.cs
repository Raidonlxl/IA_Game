using UnityEngine;

public class MoveState<T> : State<T>
{
    ISteering steering;
    BaseModel enemyModel;
    MeleeEnemyModel meleeModel;
    private Timer timer;
    ISteering flocking;
    GenericBehaviour genericBehaviour;
    public MoveState(GenericBehaviour generic, ISteering steering, ISteering flocking, BaseModel enemyModel)
    {
        this.steering = steering;
        this.enemyModel = enemyModel;
        this.flocking = flocking;
        genericBehaviour = generic;
        timer = new Timer(0, 5);

    }
    public override void Enter()
    {
        base.Enter();
        timer.currentTime = 0f;

        genericBehaviour.IsActive = true;

    }
    public override void Execute()
    {
        base.Execute();
            
        genericBehaviour.Dir = steering.GetDir();

        enemyModel.Move(flocking.GetDir());

    }
    public override void Exit()
    {
        base.Exit();
        genericBehaviour.IsActive = false;
    }
}
