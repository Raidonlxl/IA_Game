using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveSteering<T> : State<T>
{
    ISteering steering;
    EnemyModel enemyModel;
    MeleeEnemyModel meleeModel;
    private Timer timer;
    ISteering flocking;
    GenericBehaviour genericBehaviour;
    ObstacleAvoidance avoidance;
    public MoveSteering(GenericBehaviour generic, ISteering steering,ISteering flocking, EnemyModel enemyModel)
    {
        this.steering = steering;
        this.enemyModel = enemyModel;
        timer = new Timer(0, 7);
        this.flocking = flocking;
        genericBehaviour = generic;
       
    }
    public MoveSteering(GenericBehaviour generic, ISteering steering, ISteering flocking, MeleeEnemyModel enemyModel, ObstacleAvoidance avoidance)
    {
        this.steering = steering;
        meleeModel = enemyModel;
        timer = new Timer(0, 7);
        this.flocking = flocking;
        genericBehaviour = generic;
        this.avoidance = avoidance;
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
            if(enemyModel != null)
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
            if(meleeModel != null)
            {
                Vector3 dir1 = steering.GetDir();
                Vector3 dir2 = avoidance.GetDir(dir1, false);
                genericBehaviour.Dir = dir2;
                meleeModel.Move(dir2);
            }
        }
        else
        {
            genericBehaviour.Dir = steering.GetDir();
           
            if (enemyModel != null) enemyModel.Move(flocking.GetDir());
            if (meleeModel != null) meleeModel.Move(flocking.GetDir());
        }
    }
    public override void Exit()
    {
        base.Exit();

        if (steering.GetType() == typeof(Persuit))
        {
            meleeModel.LastSeenPos.position = meleeModel.Target.transform.position;
        }
       
    }
    public void ChangeSteering(ISteering newSteering)
    {
        steering = newSteering;
    }
    
    
}
