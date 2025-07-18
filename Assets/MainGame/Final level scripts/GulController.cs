using UnityEngine;

public class GulController : MonoBehaviour
{

    [SerializeField] GulModel enemyModel;

    [SerializeField] private FSM<StatesEnum> fsm;

    ITreeNode root;

    int cantMaxToShoot = 5;

    [SerializeField] LineOfSight los;

    /*
    private void Start()
    {
        enemyModel = GetComponent<GulModel>();

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

        var steeringFlocking = GetComponent<FlockingManager>();

        var steeringPersuit = new Persuit(enemyModel.transform, enemyModel.lasPositionPlayer, enemyModel.playerModel.Speed);

        var steeringPatrol = new MoveToWaypoints(enemyModel.transform, enemyModel._nodeskey, enemyModel._nodesvalue, false);

        var steeringPathing = new MoveToWaypoints(enemyModel.transform, enemyModel.target.transform, false);

        var steeringReaload = new Persuit(enemyModel.transform, boxsAmmo[0]);

        var steeringSafePoint = new MoveToWaypoints(enemyModel.transform, safepoint, false);



        var tired = new TiredState<StatesEnum>(enemyModel);

        var goToReload = new ReloadState<StatesEnum>(enemyModel.genericBehaviour, steeringReaload, steeringFlocking, boxsAmmo, enemyModel);

        var goToHeal = new SafePoint<StatesEnum>(enemyModel.genericBehaviour, steeringSafePoint, steeringFlocking, enemyModel, enemyModel.target.transform, enemyModel.nodes);

        var idle = new Idle<StatesEnum>(enemyModel.transform);

        var patrol = new MoveSteering<StatesEnum>(enemyModel.genericBehaviour, steeringPatrol, steeringFlocking, enemyModel);

        var persuit = new MoveSteering<StatesEnum>(enemyModel.genericBehaviour, steeringPersuit, steeringFlocking, enemyModel);

        var movePathing = new MoveSteering<StatesEnum>(enemyModel.genericBehaviour, steeringPathing, steeringFlocking, enemyModel);

        var shoot = new ShootState<StatesEnum>(enemyModel, enemyModel.bullet, cantMaxToShoot, steeringPatrol);

        idle.AddTransition(StatesEnum.Patrol, patrol);
        idle.AddTransition(StatesEnum.Persuit, persuit);
        idle.AddTransition(StatesEnum.Tired, tired);
        idle.AddTransition(StatesEnum.GetAmmo, goToReload);
        idle.AddTransition(StatesEnum.Shoot, shoot);
        idle.AddTransition(StatesEnum.Evade, goToHeal);


        patrol.AddTransition(StatesEnum.Idle, idle);
        patrol.AddTransition(StatesEnum.Persuit, persuit);
        patrol.AddTransition(StatesEnum.GetAmmo, goToReload);
        patrol.AddTransition(StatesEnum.Tired, tired);
        patrol.AddTransition(StatesEnum.Shoot, shoot);
        patrol.AddTransition(StatesEnum.Evade, goToHeal);

        persuit.AddTransition(StatesEnum.Idle, idle);
        persuit.AddTransition(StatesEnum.Patrol, patrol);
        persuit.AddTransition(StatesEnum.GetAmmo, goToReload);
        persuit.AddTransition(StatesEnum.Shoot, shoot);
        persuit.AddTransition(StatesEnum.setPathing, movePathing);
        persuit.AddTransition(StatesEnum.Tired, tired);
        persuit.AddTransition(StatesEnum.Evade, goToHeal);

        goToReload.AddTransition(StatesEnum.Patrol, patrol);
        goToReload.AddTransition(StatesEnum.Persuit, persuit);
        goToReload.AddTransition(StatesEnum.Shoot, shoot);
        goToReload.AddTransition(StatesEnum.Tired, tired);
        goToReload.AddTransition(StatesEnum.Evade, goToHeal);


        shoot.AddTransition(StatesEnum.Persuit, persuit);
        shoot.AddTransition(StatesEnum.Patrol, patrol);
        shoot.AddTransition(StatesEnum.GetAmmo, goToReload);
        shoot.AddTransition(StatesEnum.Tired, tired);
        shoot.AddTransition(StatesEnum.Evade, goToHeal);


        tired.AddTransition(StatesEnum.setPathing, movePathing);
        tired.AddTransition(StatesEnum.Persuit, persuit);
        tired.AddTransition(StatesEnum.Idle, idle);
        tired.AddTransition(StatesEnum.Patrol, patrol);
        tired.AddTransition(StatesEnum.Shoot, shoot);
        tired.AddTransition(StatesEnum.GetAmmo, goToReload);
        tired.AddTransition(StatesEnum.Evade, goToHeal);

        goToHeal.AddTransition(StatesEnum.Tired, tired);
        goToHeal.AddTransition(StatesEnum.Patrol, patrol);

        fsm.SetInit(idle);
    }

    void InitializeTree()
    {
        var idle = new ActionNode(() => fsm.Transition(StatesEnum.Idle));
        var patrol = new ActionNode(() => fsm.Transition(StatesEnum.Patrol));
        var persuit = new ActionNode(() => fsm.Transition(StatesEnum.Persuit));
        var reload = new ActionNode(() => fsm.Transition(StatesEnum.GetAmmo));
        var shoot = new ActionNode(() => fsm.Transition(StatesEnum.Shoot));
        var setPathing = new ActionNode(() => fsm.Transition(StatesEnum.setPathing));
        var tired = new ActionNode(() => fsm.Transition(StatesEnum.Tired));
        var escape = new ActionNode(() => fsm.Transition(StatesEnum.Evade));

        var qCanShoot = new QuestionNode(CanShot, shoot, persuit);

        var qCheckAmmo = new QuestionNode(HaveAmmo, patrol, reload);

        var qTargetPlayer = new QuestionNode(IsTargetView, qCanShoot, qCheckAmmo);

        var qTired = new QuestionNode(IsTired, tired, qTargetPlayer);

        var qHP = new QuestionNode(IsLowHp, escape, qTired);

        root = qHP;


    }

    private bool IsLowHp()
    {
        if (enemyModel.healthController.currentHealth <= 20)
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
    */
}
