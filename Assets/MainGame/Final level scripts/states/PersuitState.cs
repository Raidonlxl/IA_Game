using UnityEngine;
using System.Collections.Generic;
//persuitstate es una clase que sera la encargada de moverse con el persuit.
public class PersuitState<T> : PatrolState<T>
{
    ISteering steering;
    BaseModel enemyModel;
    private Timer timer;
    ISteering flocking;
    GenericBehaviour genericBehaviour;

    public PersuitState(GenericBehaviour genericBehaviour, ISteering steering, ISteering flocking, BaseModel enemyModel, Transform target) : base(genericBehaviour, steering, flocking, enemyModel)
    {
        this.steering = steering;
        this.enemyModel = enemyModel;
        this.flocking = flocking;
        this.genericBehaviour = genericBehaviour;
    }

    public override void Enter()
    {
        base.Enter();
        genericBehaviour.IsActive = true;
    }

    public override void Execute()
    {
        base.Execute();


        if (!enemyModel.isTired)
        {
            genericBehaviour.Dir = steering.GetDir();

            enemyModel.Move(flocking.GetDir());

            timer.Run();

        }
        if (timer.IsCompleted())
        {
            enemyModel.SetTired();
        }

    }


    public override void Exit()
    {
        base.Exit();

        enemyModel.lasPositionPlayer.position = enemyModel.target.transform.position;

        genericBehaviour.IsActive = false;
        
    } 
}
