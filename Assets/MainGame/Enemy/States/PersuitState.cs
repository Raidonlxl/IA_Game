using UnityEngine;
using System.Collections.Generic;
//persuitstate es una clase que sera la encargada de moverse con el persuit.
public class PersuitState<T> : State<T>
{
    ISteering steering;
    EnemyModel enemyModel;
    MeleeEnemyModel meleeModel;
    private Timer timer;
    ISteering flocking;
    GenericBehaviour genericBehaviour;
    

    public PersuitState(GenericBehaviour generic, ISteering steering, ISteering flocking, EnemyModel enemyModel) 
    {
        this.steering = steering;
        this.enemyModel = enemyModel;
        timer = new Timer(0, 7);
        this.flocking = flocking;
        genericBehaviour = generic;
    }

    public PersuitState(GenericBehaviour generic, ISteering steering, ISteering flocking, MeleeEnemyModel enemyModel)
    {
        this.steering = steering;
        meleeModel = enemyModel;
        timer = new Timer(0, 7);
        this.flocking = flocking;
        genericBehaviour = generic;
    }
    public override void Enter()
    {
        base.Enter();
        
    }
    public override void Execute()
    {
        base.Execute();
        if (enemyModel != null)
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
        if (meleeModel != null)
        {
            genericBehaviour.Dir = steering.GetDir();
            meleeModel.Move(steering.GetDir());

        }
    }
}
