using System;
using UnityEngine;

public class WalkState<T> : State<T>
{
    private PlayerModel owner;
    T inputShoot;

    T inputToIdle;
    public WalkState(PlayerModel owner, T inputToIdle,T inputShoot)
    {
        this.owner = owner;
    
        this.inputToIdle = inputToIdle;
        this.inputShoot = inputShoot;
    }
    public override void Execute()
    {

        if (InputManager.GetMovementInput() != Vector3.zero)
        {

            owner.MoveFront(InputManager.GetMovementInput());

        }

        if (InputManager.Shoot())
        {
            StateMachine.Transition(inputShoot);
        }

        else
        {
            StateMachine.Transition(inputToIdle);
        }

        
    }
}
