using UnityEngine;

public class ShootStatePlayer<T> : State<T>
{
    PlayerModel owner;
    T input;
    T inputIdle;
    private PoolGeneric<GameObject> pool;
    GameObject bulletController;
    public ShootStatePlayer(PlayerModel owner,GameObject bullet, T input, T inputIdle)
    {
        pool = new PoolGeneric<GameObject>();
        this.owner = owner;
        this.input = input;
        pool.originalPrefab = bullet;
        pool.InitializePool(7);
        this.inputIdle = inputIdle;
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
     
        var instance = pool.GetFromPool();
        owner.Shoot(instance,pool);

        if (InputManager.GetMovementInput() != Vector3.zero)
        {
            StateMachine.Transition(input);
        }

        else
        {
            StateMachine.Transition(inputIdle);
        }

    }
}
