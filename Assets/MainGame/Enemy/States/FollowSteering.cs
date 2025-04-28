using UnityEngine;

public class FollowSteering<T> : State<T>
{
    ISteering steering;
    EnemyModel enemyModel;
    public FollowSteering(ISteering steering,EnemyModel owner )
    {
        this.steering = steering;
        enemyModel = owner; 
    }

    public override void Execute()
    {
        base.Execute();
        var dir = steering.GetDir();

        enemyModel.Move(dir);
    }
    public void ChangeSteering(ISteering newSteering)
    {
        steering = newSteering;
    }
}
