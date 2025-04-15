using System;
using UnityEngine;

public class WalkState<T> : State<T>
{
    private PlayerModel owner;
    private float speed;

    T imputToIdle;
    public WalkState(PlayerModel owner, float speed, T imputToIdle)
    {
        this.owner = owner;
        this.speed = speed;
        this.imputToIdle = imputToIdle;
    }
    public override void Execute()
    {

        if (InputManager.GetSide() != Vector3.zero || InputManager.GetDirection() != Vector3.zero)
        {
            if (InputManager.Run())
            {
                owner.MoveFront(owner.transform.position, speed);
                Debug.Log("RUNING");

            }
            else
            {
                owner.MoveFront(InputManager.GetDirection(), speed);
                owner.MoveSide(InputManager.GetSide(), speed);
             

            }
        }

        else
        {
            StateMachine.Transition(imputToIdle);
        }

        
    }
}
