using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] EnemyModel enemyModel;

    [SerializeField] private FSM<StatesEnum> fsm;
    ITreeNode root;
    [SerializeField] Transform[] boxsAmmo;
    int cantMaxToShoot = 5;
    [SerializeField] LineOfSight los;

    [SerializeField] private Transform[] waypoints;

    private void Start()
    {
        enemyModel = GetComponent<EnemyModel>();
        los = GetComponent<LineOfSight>();
        los.Initialize(enemyModel.transform, enemyModel.target.transform, enemyModel.enemyBase.Range, enemyModel.enemyBase.Angle, enemyModel.enemyBase.ObstacleMask);

        InitializeFsm();
        InitializeTree();
  
    }

    private void Update()
    {
        
        fsm.OnExecute();
        root.Execute();
    }

    void InitializeFsm()
    {
        fsm = new FSM<StatesEnum>();
        
        var steeringPersuit = new Persuit(enemyModel.transform, enemyModel.target.transform, enemyModel.playerModel.Speed);

        var steeringPatrol = new MoveToWaypoints(enemyModel.transform,enemyModel.target.transform,true);

        var steeringPathing = new MoveToWaypoints(enemyModel.transform,enemyModel.target.transform,false);

        var steeringReaload = new MoveToWaypoints(enemyModel.transform, boxsAmmo[0], false);



        var tired = new TiredState<StatesEnum>(enemyModel);
        
        var goToReload = new ReloadState<StatesEnum>(steeringReaload, boxsAmmo, enemyModel);

        var idle = new Idle<StatesEnum>(enemyModel.transform);

        var patrol = new MoveSteering<StatesEnum>(steeringPatrol, enemyModel);

        var persuit = new MoveSteering<StatesEnum>(steeringPersuit, enemyModel);

        var movePathing = new MoveSteering<StatesEnum>(steeringPathing, enemyModel);

        var shoot = new ShootState<StatesEnum>(enemyModel,enemyModel.bullet,cantMaxToShoot);

        idle.AddTransition(StatesEnum.Patrol, patrol);
        idle.AddTransition(StatesEnum.Persuit, persuit);
        idle.AddTransition(StatesEnum.Tired, tired);
        idle.AddTransition(StatesEnum.GetAmmo, goToReload);
        idle.AddTransition(StatesEnum.Shoot, shoot);

        patrol.AddTransition(StatesEnum.Idle, idle);
        patrol.AddTransition(StatesEnum.Persuit, persuit);
        patrol.AddTransition(StatesEnum.GetAmmo, goToReload);
        patrol.AddTransition(StatesEnum.Tired, tired);
        patrol.AddTransition(StatesEnum.Shoot, shoot);

        persuit.AddTransition(StatesEnum.Idle, idle);
        persuit.AddTransition(StatesEnum.Patrol, patrol);
        persuit.AddTransition(StatesEnum.GetAmmo, goToReload);
        persuit.AddTransition(StatesEnum.Shoot, shoot);
        persuit.AddTransition(StatesEnum.setPathing, movePathing);
        persuit.AddTransition(StatesEnum.Tired, tired);

        goToReload.AddTransition(StatesEnum.Patrol, patrol);
        goToReload.AddTransition(StatesEnum.Persuit, persuit);
        goToReload.AddTransition(StatesEnum.Shoot, shoot);
        goToReload.AddTransition(StatesEnum.Tired, tired);
        goToReload.AddTransition(StatesEnum.setPathing,movePathing);    

        shoot.AddTransition(StatesEnum.Persuit, persuit);
        shoot.AddTransition(StatesEnum.GetAmmo, goToReload);
        shoot.AddTransition(StatesEnum.Tired, tired);

        movePathing.AddTransition(StatesEnum.Persuit, persuit);
        movePathing.AddTransition(StatesEnum.Tired, tired);
        movePathing.AddTransition(StatesEnum.Shoot, shoot);

        tired.AddTransition(StatesEnum.setPathing,movePathing);
        tired.AddTransition(StatesEnum.Persuit, persuit);
        tired.AddTransition(StatesEnum.Idle, idle);
        tired.AddTransition(StatesEnum.Patrol, patrol);
        tired.AddTransition(StatesEnum.Shoot, shoot);
        tired.AddTransition(StatesEnum.GetAmmo, goToReload);

        fsm.SetInit(idle);
    }

    void InitializeTree()
    {
        var idle = new ActionNode(() => fsm.Transition(StatesEnum.Idle));
        var patrol = new ActionNode(() => fsm.Transition(StatesEnum.Patrol));
        var persuit = new ActionNode(() => fsm.Transition(StatesEnum.Persuit));
        var reload = new ActionNode(() => fsm.Transition(StatesEnum.GetAmmo));
        var shoot = new ActionNode(()=> fsm.Transition(StatesEnum.Shoot));
        var setPathing = new ActionNode(()=>fsm.Transition(StatesEnum.setPathing));
        var tired = new ActionNode(() => fsm.Transition(StatesEnum.Tired));

        var qCanShoot = new QuestionNode(CanShot, shoot, persuit);

        var qCheckAmmo = new QuestionNode(HaveAmmo, patrol, reload);

        var qTargetPlayer = new QuestionNode(IsTargetView, qCanShoot, qCheckAmmo);

        var qTired = new QuestionNode(IsTired, tired, qTargetPlayer);

        root = qTired;

    }
    private bool IsTired()
    {
        return enemyModel.isTired;
    }
    private bool CanShot()
    {
        if (los.LOS() && GetDistanceTarget() < 5 && enemyModel.isReady)
        {
            return true;
        }
        else
        {

            return false;
        }
    }
    private bool HaveAmmo()
    {
        return enemyModel.isReady;
    }
    private bool IsTargetView()
    {
        if (los.LOS() && !enemyModel.isReady)
        {
            return false;
        }
        else if(los.LOS() && enemyModel.isReady)
        {
            return true;
        }
        else if(!los.LOS() && !enemyModel.isReady)
        {

            return false;
        }

        else
        {
         
            return false;
        }
        
    }

    private float GetDistanceTarget()
    {
        return Vector3.Distance(enemyModel.transform.position,enemyModel.target.transform.position);
    }
}