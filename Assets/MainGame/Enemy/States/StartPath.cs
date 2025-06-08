using UnityEngine;

public class StartPath<T> : State<T>
{
    private EnemyModel self;
    //private SetPath setPath;
    private ISteering steering;
    public StartPath(ISteering steering, EnemyModel self)
    {
        this.self = self;

        this.steering = steering;

    }

    public override void Enter()
    {
        base.Enter();
        //setPath.SetPathAStarPlus();
               
    }

    public override void Execute()
    {
        base.Execute();
        self.Move(steering.GetDir());
    }

    
}
