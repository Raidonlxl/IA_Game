using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerBase playerBase;
    private PlayerModel playerModel;
    private Rigidbody rb;

    private FSM<StatesEnum> fsm;
    //private Counter counter;

    [SerializeField] CameraController cameraController;

    private void Start()
    {
       
        InputManager.cameraController = cameraController;
        rb= gameObject.GetComponent<Rigidbody>();
        playerModel= gameObject.GetComponent<PlayerModel>();

        InitializeFsm();
        //counter = new Counter();
    }

    private void Update()
    {
        fsm.OnExecute();
    }
    void InitializeFsm()
    {
        fsm = new FSM<StatesEnum>();
        var movePlayer = new WalkState<StatesEnum>(playerModel,StatesEnum.Idle,StatesEnum.Shoot);
        var idlePlayer = new IdleState<StatesEnum>(StatesEnum.Run, StatesEnum.Shoot);
        var shootPlayer = new ShootStatePlayer<StatesEnum>(playerModel,playerModel.bullet, StatesEnum.Run,StatesEnum.Idle);

        idlePlayer.AddTransition(StatesEnum.Run,movePlayer);
        idlePlayer.AddTransition(StatesEnum.Shoot,shootPlayer);

        movePlayer.AddTransition(StatesEnum.Idle,idlePlayer);
        movePlayer.AddTransition(StatesEnum.Shoot, shootPlayer);

        shootPlayer.AddTransition(StatesEnum.Run,movePlayer);
        shootPlayer.AddTransition(StatesEnum.Idle, idlePlayer);

        fsm.SetInit(idlePlayer);

    }
}
