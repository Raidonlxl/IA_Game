using System;
using UnityEngine;

public class WalkState<T> : State<T>
{
    private PlayerModel owner;
 

    T imputToIdle;
    public WalkState(PlayerModel owner, T imputToIdle)
    {
        this.owner = owner;
    
        this.imputToIdle = imputToIdle;
    }
    public override void Execute()
    {

        if (InputManager.GetSide() != Vector3.zero || InputManager.GetDirection() != Vector3.zero)
        {
            if (InputManager.Run())
            {
                owner.MoveFront(owner.transform.position);
                Debug.Log("RUNING");

            }
            else
            {
                owner.MoveFront(InputManager.GetDirection());
                owner.MoveSide(InputManager.GetSide());
             

            }
        }

        else
        {
            StateMachine.Transition(imputToIdle);
        }

        
    }
}
