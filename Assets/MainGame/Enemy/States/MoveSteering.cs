using UnityEngine;

public class MoveSteering<T> : State<T>
{
    ISteering steering;
    EnemyModel enemyModel;
    private Timer timer;
    ISteering flocking;
    GenericBehaviour genericBehaviour;
    public MoveSteering(GenericBehaviour generic, ISteering steering,ISteering flocking, EnemyModel enemyModel)
    {
        this.steering = steering;
        this.enemyModel = enemyModel;
        timer = new Timer(0, 7);
        this.flocking = flocking;
        genericBehaviour = generic;
    }
    public MoveSteering(ISteering steering, KeyModel enemyModel)
    {
        this.steering = steering;
        timer = new Timer(0, 7);
    }

    public override void Enter()
    {
        base.Enter();
        timer.currentTime = 0f;
        

    }
    public override void Execute()
    {
        base.Execute();

        if (steering.GetType() == typeof(Persuit))
        {
            if (!enemyModel.isTired)
            {
                genericBehaviour.Dir = steering.GetDir();
                enemyModel.Move(steering.GetDir());
                timer.Run();
            }
            if (timer.IsCompleted())
            {
                enemyModel.isTired = true;
            }
        }
        else
        {
            genericBehaviour.Dir = steering.GetDir();
            enemyModel.Move(flocking.GetDir());
        }
    }
    public override void Exit()
    {
        base.Exit();
       
    }
    public void ChangeSteering(ISteering newSteering)
    {
        steering = newSteering;
    }
}
