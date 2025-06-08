using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.UI.GridLayoutGroup;

public class IdleState<T> : State<T>
{
    T inputToMove;
    T inputShoot;
    PlayerModel playerModel;
    public IdleState(T inputToMove, T inputShoot)
    {
        this.inputToMove = inputToMove;
        this.inputShoot = inputShoot;


    }
    public override void Execute()
    {
        base.Execute();


        

        if (InputManager.GetMovementInput() != Vector3.zero)
        {
            StateMachine.Transition(inputToMove);
        }

        else if (InputManager.Shoot())
        {
            StateMachine.Transition(inputShoot);
        }
    }
}
