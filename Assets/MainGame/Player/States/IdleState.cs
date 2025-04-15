using UnityEngine;

public class IdleState<T> : State<T>
{
    T inputToMove;
    public IdleState( T inputToMove)
    {
        this.inputToMove = inputToMove;
    }
    public override void Execute()
    {
        base.Execute();

       

        if(InputManager.GetSide() != Vector3.zero || InputManager.GetDirection() != Vector3.zero)
        {
            StateMachine.Transition(inputToMove);
        }
    }
}
