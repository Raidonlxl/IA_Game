using System.Collections.Generic;
using UnityEngine;

public class GulController : MonoBehaviour
{

    [SerializeField] GulModel enemyModel;

    [SerializeField] private FSM<StatesEnum> fsm;

    Vector3 positionPlayer;

    ITreeNode root;

    int cantMaxToShoot = 5;

    [SerializeField] LineOfSight los;

    [SerializeField] private Transform[] boxsAmmo;
    Dictionary<ITreeNode, float> randomNodes = new Dictionary<ITreeNode, float>();
    
    private void Start()
    {
        enemyModel = GetComponent<GulModel>();

        los = GetComponent<LineOfSight>();

        los.Initialize(enemyModel.transform, enemyModel.target.transform, enemyModel.unitStats.Range, enemyModel.unitStats.Angle, enemyModel.unitStats.WallsMask);
        positionPlayer = enemyModel.lasPositionPlayer.position;
        InitializeFsm();
        InitializeTree();

    }

    private void Update()
    {
        fsm.OnExecute();
        root.Execute();
        Debug.Log("state: " + fsm._currState);
    }

    void InitializeFsm()
    {
        fsm = new FSM<StatesEnum>();

        var flockingSteering = GetComponent<FlockingManager>();

        var persuitSteering = new Persuit(enemyModel.transform, enemyModel.lasPositionPlayer);

        var patrolSteering = new FollowWaypointsSteering(enemyModel.transform, enemyModel.lasPositionPlayer, enemyModel.NodesList, enemyModel.Nodesvalue);

        var reloadSteering = new FollowPathSteering(enemyModel.transform, boxsAmmo[0].transform);

        var goToLastPosition = new FollowPathSteering(enemyModel.transform, enemyModel.lasPositionPlayer);

        var backTobaseSteering = new FollowPathSteering(enemyModel.transform, enemyModel.baseTransform);

        var safePoint = new FollowPathSteering(enemyModel.transform);



        var tiredState = new TiredState<StatesEnum>(enemyModel);

        var reloadState = new ReloadState<StatesEnum>(enemyModel.genericBehaviour, reloadSteering, flockingSteering, boxsAmmo, enemyModel);

        var escapeState = new SafePointState<StatesEnum>(enemyModel.genericBehaviour, safePoint, flockingSteering, enemyModel, enemyModel.target.transform);

        var backBaseState = new MoveState<StatesEnum>(enemyModel.genericBehaviour, safePoint, flockingSteering, enemyModel);

        var patrolState = new MoveState<StatesEnum>(enemyModel.genericBehaviour, patrolSteering, flockingSteering, enemyModel);
        
        var goToLastPositionState = new MoveState<StatesEnum>(enemyModel.genericBehaviour, goToLastPosition, flockingSteering, enemyModel);

        var persuitState = new PersuitState<StatesEnum>(enemyModel.genericBehaviour, persuitSteering, flockingSteering, enemyModel, enemyModel.target.transform);

        var shootState = new ShootState<StatesEnum>(enemyModel, enemyModel.bullet, cantMaxToShoot);

      


        patrolState.AddTransition(StatesEnum.Persuit, persuitState);
        patrolState.AddTransition(StatesEnum.GetAmmo, reloadState);
        patrolState.AddTransition(StatesEnum.Shoot, shootState);
        patrolState.AddTransition(StatesEnum.Evade, escapeState);
        patrolState.AddTransition(StatesEnum.BackBase, backBaseState);


        persuitState.AddTransition(StatesEnum.Patrol, patrolState);
        persuitState.AddTransition(StatesEnum.GetAmmo, reloadState);
        persuitState.AddTransition(StatesEnum.Shoot, shootState);
        persuitState.AddTransition(StatesEnum.Tired, tiredState);
        persuitState.AddTransition(StatesEnum.Evade, escapeState);
        persuitState.AddTransition(StatesEnum.BackBase, backBaseState);
        persuitState.AddTransition(StatesEnum.GoToLastPoint, goToLastPositionState);

        reloadState.AddTransition(StatesEnum.Patrol, patrolState);
        reloadState.AddTransition(StatesEnum.Persuit, persuitState);
        reloadState.AddTransition(StatesEnum.Shoot, shootState);
        reloadState.AddTransition(StatesEnum.Tired, tiredState);
        reloadState.AddTransition(StatesEnum.Evade, escapeState);
        reloadState.AddTransition(StatesEnum.BackBase, backBaseState);


        shootState.AddTransition(StatesEnum.Persuit, persuitState);
        shootState.AddTransition(StatesEnum.Patrol, patrolState);
        shootState.AddTransition(StatesEnum.GetAmmo, reloadState);
        shootState.AddTransition(StatesEnum.Tired, tiredState);
        shootState.AddTransition(StatesEnum.Evade, escapeState);
        shootState.AddTransition(StatesEnum.BackBase, backBaseState);



        tiredState.AddTransition(StatesEnum.Persuit, persuitState);
        tiredState.AddTransition(StatesEnum.Patrol, patrolState);
        tiredState.AddTransition(StatesEnum.Shoot, shootState);
        tiredState.AddTransition(StatesEnum.GetAmmo, reloadState);
        tiredState.AddTransition(StatesEnum.Evade, escapeState);

        escapeState.AddTransition(StatesEnum.Tired, tiredState);
        escapeState.AddTransition(StatesEnum.Patrol, patrolState);
        escapeState.AddTransition(StatesEnum.BackBase, backBaseState);

        backBaseState.AddTransition(StatesEnum.Tired, tiredState);
        backBaseState.AddTransition(StatesEnum.Patrol, patrolState);
        backBaseState.AddTransition(StatesEnum.Persuit, persuitState);
        backBaseState.AddTransition(StatesEnum.GoToLastPoint, goToLastPositionState);
       

        fsm.SetInit(tiredState);
    }

    void InitializeTree()
    {
        var patrol = new ActionNode(() => fsm.Transition(StatesEnum.Patrol));

        var persuit = new ActionNode(() => fsm.Transition(StatesEnum.Persuit));

        var reload = new ActionNode(() => fsm.Transition(StatesEnum.GetAmmo));

        var shoot = new ActionNode(() => fsm.Transition(StatesEnum.Shoot));

        var tired = new ActionNode(() => fsm.Transition(StatesEnum.Tired));

        var escape = new ActionNode(() => fsm.Transition(StatesEnum.Evade));

        var backBase = new ActionNode(() => fsm.Transition(StatesEnum.BackBase));

        var goToLastPosition = new ActionNode(() => fsm.Transition(StatesEnum.GoToLastPoint));


        randomNodes.Add(goToLastPosition, enemyModel.IdleWeight);

        randomNodes.Add(patrol, enemyModel.PatrolWeight);

        var patrolRun = new RandomNode(randomNodes);


        //var qCanPatrol = new QuestionNode(IsEqual, patrol, goToLastPosition);

        var qCanShoot = new QuestionNode(CanShot, shoot, persuit);

        var qCheckAmmo = new QuestionNode(HaveAmmo, qCanShoot, reload);

        var qTargetPlayer = new QuestionNode(IsTargetView, qCheckAmmo, patrolRun);

        var qHaveAssemble = new QuestionNode(HaveAssemble, backBase, qTargetPlayer);

        var qTired = new QuestionNode(IsTired, tired, qHaveAssemble);

        var qHP = new QuestionNode(IsLowHp, escape, qTired);

        root = qHP;


    }

    private bool HaveAssemble()
    {
        return enemyModel.haveAssemble;
    }

    private bool IsEqual()
    {
        if (Vector3.Distance(enemyModel.lasPositionPlayer.position, positionPlayer) < 5)
        {
            return true;
        }

        else
        {
            positionPlayer = enemyModel.lasPositionPlayer.position;
            return false;
        }
    }
    private bool IsLowHp()
    {
        if (enemyModel.healthController.currentHealth <= 20 || enemyModel.IsScared)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsTired()
    {
        return enemyModel.IsTired;
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
        bool directLine = los.LOS();

        if (directLine && !enemyModel.isReady)
        {
            return false;
        }
        else if (directLine && enemyModel.isReady)
        {
            return true;
        }
        else if (!directLine && !enemyModel.isReady)
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
        return Vector3.Distance(enemyModel.transform.position, enemyModel.target.transform.position);
    }
    
}
