using UnityEngine;

public class MoveSteering<T> : State<T>
{
    ISteering steering;
    EnemyModel enemyModel;
    private Timer timer;
    public MoveSteering(ISteering steering, EnemyModel enemyModel)
    {
        this.steering = steering;
        this.enemyModel = enemyModel;
        timer = new Timer(0, 7);
    }

    public override void Enter()
    {
        base.Enter();
        timer.currentTime = 0f;

    }
    public override void Execute()
    {
        if (steering.GetType() == typeof(Persuit))
        {
            if (!enemyModel.isTired)
            {
                base.Execute();
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
            enemyModel.Move(steering.GetDir());
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
