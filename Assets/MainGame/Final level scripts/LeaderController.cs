using UnityEngine;
using System.Collections.Generic;

public class LeaderController : MonoBehaviour
{
    [SerializeField] LeaderModel model;

    private FSM<StatesEnum> fsm;

    Vector3 positionPlayer;

    ITreeNode root;

    int cantMaxToShoot = 8;

    [SerializeField] LineOfSight los;

    [SerializeField] private Transform[] boxsAmmo;

    Dictionary<ITreeNode, float> notchasingroulette = new Dictionary<ITreeNode, float>();
    Dictionary<ITreeNode, float> lowHProulette = new Dictionary<ITreeNode, float>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        model = GetComponent<LeaderModel>();
        los.Initialize(model.transform, model.target.transform, model.LeaderStats.Range, model.LeaderStats.Angle, model.LeaderStats.WallsMask);
        positionPlayer = model.lasPositionPlayer.position;
        InitializeFSM();
        InitializeTree();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeFSM()
    {
        fsm = new FSM<StatesEnum>();

        var flockingSteering = GetComponent<FlockingManager>();

        var persuitSteering = new Persuit(model.transform, model.lasPositionPlayer);

        var patrolSteering = new FollowWaypointsSteering(model.transform, model.lasPositionPlayer, model.NodesList, model.Nodesvalue);

        var reloadSteering = new FollowPathSteering(model.transform, boxsAmmo[0].transform);

        var goToLastPosition = new FollowPathSteering(model.transform, model.lasPositionPlayer);

        var backTobaseSteering = new FollowPathSteering(model.transform, model.baseTransform);

        var safePoint = new FollowPathSteering(model.transform);

        var idlestate = new idleState<StatesEnum>(model);

        var tiredState = new TiredState<StatesEnum>(model);

        var reloadState = new ReloadState<StatesEnum>(model.genericBehaviour, reloadSteering, flockingSteering, boxsAmmo, model);

        var escapeState = new SafePointState<StatesEnum>(model.genericBehaviour, safePoint, flockingSteering, model, model.target.transform);

        var backBaseState = new MoveState<StatesEnum>(model.genericBehaviour, safePoint, flockingSteering, model);

        var patrolState = new MoveState<StatesEnum>(model.genericBehaviour, patrolSteering, flockingSteering, model);

        var goToLastPositionState = new MoveState<StatesEnum>(model.genericBehaviour, goToLastPosition, flockingSteering, model);

        var persuitState = new PersuitState<StatesEnum>(model.genericBehaviour, persuitSteering, flockingSteering, model, model.target.transform);

        var shootState = new ShootState<StatesEnum>(model, model.bullet, cantMaxToShoot);

        var intimidateState = new IntimidationState<StatesEnum>(model);

        var CallAsemblyState = new CallAssemblyState<StatesEnum>(model);

        idlestate.AddTransition(StatesEnum.Patrol, patrolState);
        idlestate.AddTransition(StatesEnum.GetAmmo, reloadState);
        idlestate.AddTransition(StatesEnum.Shoot, shootState);
        idlestate.AddTransition(StatesEnum.Tired, tiredState);
        idlestate.AddTransition(StatesEnum.Evade, escapeState);
        idlestate.AddTransition(StatesEnum.BackBase, backBaseState);
        idlestate.AddTransition(StatesEnum.GoToLastPoint, goToLastPositionState);
        idlestate.AddTransition(StatesEnum.Intimidate, intimidateState);
        idlestate.AddTransition(StatesEnum.Callassembly, CallAsemblyState);

        patrolState.AddTransition(StatesEnum.Persuit, persuitState);
        patrolState.AddTransition(StatesEnum.GetAmmo, reloadState);
        patrolState.AddTransition(StatesEnum.Shoot, shootState);
        patrolState.AddTransition(StatesEnum.Evade, escapeState);
        patrolState.AddTransition(StatesEnum.BackBase, backBaseState);
        patrolState.AddTransition(StatesEnum.Idle, idlestate);
        patrolState.AddTransition(StatesEnum.Intimidate, intimidateState);
        patrolState.AddTransition(StatesEnum.Callassembly, CallAsemblyState);

        persuitState.AddTransition(StatesEnum.Patrol, patrolState);
        persuitState.AddTransition(StatesEnum.GetAmmo, reloadState);
        persuitState.AddTransition(StatesEnum.Shoot, shootState);
        persuitState.AddTransition(StatesEnum.Tired, tiredState);
        persuitState.AddTransition(StatesEnum.Evade, escapeState);
        persuitState.AddTransition(StatesEnum.BackBase, backBaseState);
        persuitState.AddTransition(StatesEnum.GoToLastPoint, goToLastPositionState);
        persuitState.AddTransition(StatesEnum.Idle, idlestate);
        persuitState.AddTransition(StatesEnum.Intimidate, intimidateState);
        persuitState.AddTransition(StatesEnum.Callassembly, CallAsemblyState);

        reloadState.AddTransition(StatesEnum.Patrol, patrolState);
        reloadState.AddTransition(StatesEnum.Persuit, persuitState);
        reloadState.AddTransition(StatesEnum.Shoot, shootState);
        reloadState.AddTransition(StatesEnum.Tired, tiredState);
        reloadState.AddTransition(StatesEnum.Evade, escapeState);
        reloadState.AddTransition(StatesEnum.BackBase, backBaseState);
        reloadState.AddTransition(StatesEnum.Idle, idlestate);
        reloadState.AddTransition(StatesEnum.Callassembly, CallAsemblyState);


        shootState.AddTransition(StatesEnum.Persuit, persuitState);
        shootState.AddTransition(StatesEnum.Patrol, patrolState);
        shootState.AddTransition(StatesEnum.GetAmmo, reloadState);
        shootState.AddTransition(StatesEnum.Tired, tiredState);
        shootState.AddTransition(StatesEnum.Evade, escapeState);
        shootState.AddTransition(StatesEnum.BackBase, backBaseState);
        shootState.AddTransition(StatesEnum.Idle, idlestate);
        shootState.AddTransition(StatesEnum.Callassembly, CallAsemblyState);


        tiredState.AddTransition(StatesEnum.Persuit, persuitState);
        tiredState.AddTransition(StatesEnum.Patrol, patrolState);
        tiredState.AddTransition(StatesEnum.Shoot, shootState);
        tiredState.AddTransition(StatesEnum.GetAmmo, reloadState);
        tiredState.AddTransition(StatesEnum.Evade, escapeState);
        tiredState.AddTransition(StatesEnum.Idle, idlestate);
        tiredState.AddTransition(StatesEnum.Intimidate, intimidateState);
        tiredState.AddTransition(StatesEnum.Callassembly, CallAsemblyState);

        escapeState.AddTransition(StatesEnum.Tired, tiredState);
        escapeState.AddTransition(StatesEnum.Patrol, patrolState);
        escapeState.AddTransition(StatesEnum.BackBase, backBaseState);
        escapeState.AddTransition(StatesEnum.Idle, idlestate);
        escapeState.AddTransition(StatesEnum.Callassembly, CallAsemblyState);

        backBaseState.AddTransition(StatesEnum.Tired, tiredState);
        backBaseState.AddTransition(StatesEnum.Patrol, patrolState);
        backBaseState.AddTransition(StatesEnum.Persuit, persuitState);
        backBaseState.AddTransition(StatesEnum.GoToLastPoint, goToLastPositionState);
        backBaseState.AddTransition(StatesEnum.Idle, idlestate);
        backBaseState.AddTransition(StatesEnum.Callassembly, CallAsemblyState);

        intimidateState.AddTransition(StatesEnum.Patrol, patrolState);
        intimidateState.AddTransition(StatesEnum.GetAmmo, reloadState);
        intimidateState.AddTransition(StatesEnum.Shoot, shootState);
        intimidateState.AddTransition(StatesEnum.Tired, tiredState);
        intimidateState.AddTransition(StatesEnum.Evade, escapeState);
        intimidateState.AddTransition(StatesEnum.BackBase, backBaseState);
        intimidateState.AddTransition(StatesEnum.GoToLastPoint, goToLastPositionState);
        intimidateState.AddTransition(StatesEnum.Callassembly, CallAsemblyState);

        CallAsemblyState.AddTransition(StatesEnum.Patrol, patrolState);
        CallAsemblyState.AddTransition(StatesEnum.GetAmmo, reloadState);
        CallAsemblyState.AddTransition(StatesEnum.Shoot, shootState);
        CallAsemblyState.AddTransition(StatesEnum.Tired, tiredState);
        CallAsemblyState.AddTransition(StatesEnum.Evade, escapeState);
        CallAsemblyState.AddTransition(StatesEnum.BackBase, backBaseState);
        CallAsemblyState.AddTransition(StatesEnum.GoToLastPoint, goToLastPositionState);
        CallAsemblyState.AddTransition(StatesEnum.Intimidate, intimidateState);
        

        fsm.SetInit(tiredState);
    }
    void InitializeTree()
    {
        var idle = new ActionNode(() => fsm.Transition(StatesEnum.Idle));

        var patrol = new ActionNode(() => fsm.Transition(StatesEnum.Patrol));

        var persuit = new ActionNode(() => fsm.Transition(StatesEnum.Persuit));

        var reload = new ActionNode(() => fsm.Transition(StatesEnum.GetAmmo));

        var shoot = new ActionNode(() => fsm.Transition(StatesEnum.Shoot));

        var tired = new ActionNode(() => fsm.Transition(StatesEnum.Tired));

        var escape = new ActionNode(() => fsm.Transition(StatesEnum.Evade));

        var backBase = new ActionNode(() => fsm.Transition(StatesEnum.BackBase));

        var goToLastPosition = new ActionNode(() => fsm.Transition(StatesEnum.GoToLastPoint));

        var intimidate = new ActionNode(() => fsm.Transition(StatesEnum.Intimidate));

        var callassembly = new ActionNode(() => fsm.Transition(StatesEnum.Callassembly));

        notchasingroulette.Add(idle, model.IdleWeight);
        notchasingroulette.Add(patrol, model.PatrolWeight);
        notchasingroulette.Add(intimidate, model.IntimidationWeight);

        lowHProulette.Add(escape, model.FleeWeight);
        lowHProulette.Add(callassembly, model.RegroupWeight);

        var notchasingaction = new RandomNode(notchasingroulette);

        var lowhpaction = new RandomNode(lowHProulette);

        var qCanShoot = new QuestionNode(CanShot,shoot, persuit);

        var qCheckAmmo = new QuestionNode(HaveAmmo, qCanShoot, reload);

        var qTargetPlayer = new QuestionNode(IsTargetView, qCheckAmmo, notchasingaction);

        var qIsTired = new QuestionNode(IsTired, tired, qTargetPlayer);

        var qhasassemble = new QuestionNode(HaveAssemble, backBase, qIsTired);

        var qlowHP = new QuestionNode(IsLowHp, lowhpaction, qhasassemble);

        root = qlowHP;
    }
    private bool HaveAssemble()
    {
        return model.haveAssemble;
    }
    private bool IsEqual()
    {
        if (Vector3.Distance(model.lasPositionPlayer.position, positionPlayer) < 5)
        {
            return true;
        }

        else
        {
            positionPlayer = model.lasPositionPlayer.position;
            return false;
        }
    }
    private bool IsLowHp()
    {
        int hpcheck = model.LeaderStats.MaxLife;
        if (model.healthController.currentHealth <= hpcheck/2)
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
        return model.IsTired;
    }
    private bool CanShot()
    {
        if (los.LOS() && GetDistanceTarget() < 5 && model.isReady)
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
        return model.isReady;
    }
    private bool IsTargetView()
    {
        bool directLine = los.LOS();

        if (directLine && !model.isReady)
        {
            return false;
        }
        else if (directLine && model.isReady)
        {
            return true;
        }
        else if (!directLine && !model.isReady)
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
        return Vector3.Distance(model.transform.position, model.target.transform.position);
    }
}
