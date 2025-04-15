using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerBase playerBase;
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
    private void FixedUpdate()
    { 
        /*
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

      
        Vector3 forward = new Vector3(cameraController.transform.forward.x,0f,cameraController.transform.forward.z).normalized;
        Vector3 right = new Vector3(cameraController.transform.forward.x,0f,0f).normalized;

        Vector3 movementVertical = forward * y;
        Vector3 movementHorizontal = right * x;
        
        if (x != 0 || y != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && playerBase.CurrentTiredTime > 0)
            {
                Run(movementVertical);
                playerBase.CurrentConter = 0;
                playerBase.CurrentTiredTime -= Time.deltaTime;
                
            }
        else
            {
                playerModel.MoveFront(movementVertical, playerBase.Speed);
                playerModel.MoveSide(movementHorizontal, playerBase.Speed);

            }
        }
        */

        if (playerBase.CurrentConter < playerBase.MaxConter)
        {
            playerBase.CurrentConter += Time.deltaTime;

        }

        else
        {
            if(playerBase.CurrentTiredTime< playerBase.MaxTimeTired)
            {
                playerBase.CurrentTiredTime += Time.deltaTime;
            }

        }

    }

    private void Run(Vector3 moveVertical)
    {
        playerModel.MoveFront(moveVertical, playerBase.RunSpeed);

    }

    void InitializeFsm()
    {
        fsm = new FSM<StatesEnum>();
        var movePlayer = new WalkState<StatesEnum>(playerModel,playerBase.Speed,StatesEnum.Idle);
        var idlePlayer = new IdleState<StatesEnum>(StatesEnum.Run);

        idlePlayer.AddTransition(StatesEnum.Run,movePlayer);

        movePlayer.AddTransition(StatesEnum.Idle,idlePlayer);

        fsm.SetInit(idlePlayer);

    }
}
