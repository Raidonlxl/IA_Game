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
    public MoveSteering(GenericBehaviour generic, ISteering steering,ISteering flocking, EnemyModel enemyModel)
    {
        this.steering = steering;
        this.enemyModel = enemyModel;
        timer = new Timer(0, 7);
        this.flocking = flocking;
        genericBehaviour = generic;
       
    }
    public MoveSteering(GenericBehaviour generic, ISteering steering, ISteering flocking, MeleeEnemyModel enemyModel)
    {
        this.steering = steering;
        meleeModel = enemyModel;
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

        genericBehaviour.IsActive = true;
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
                genericBehaviour.Dir = steering.GetDir();
                meleeModel.Move(steering.GetDir());
                
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
            if(meleeModel != null)
            meleeModel.LastSeenPos.position = meleeModel.Target.transform.position;
            if (enemyModel != null)
            enemyModel.lasPositionPlayer.position = enemyModel.target.transform.position;
        }
        genericBehaviour.IsActive = false;
    }
    public void ChangeSteering(ISteering newSteering)
    {
        steering = newSteering;
    }
    
    
}
