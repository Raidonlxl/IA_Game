using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{

    [SerializeField] EnemyModel enemyModel;
    private FSM<StatesEnum> fsm;
    ITreeNode root;
    ISteering steering;
    [SerializeField] Transform[] boxsAmmo;
    int cantMaxToShoot=5;

    float currentTime;
    float maxTimeTired=7;
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
        var steeringPatrol = new Patrol(enemyModel.transform, waypoints);


        var goToReload = new ReloadState<StatesEnum>(boxsAmmo, enemyModel);

        var idle = new Idle<StatesEnum>(enemyModel.transform);

        var patrol = new PatrolState<StatesEnum>(steeringPatrol, enemyModel);

        var persuit = new FollowSteering<StatesEnum>(steeringPersuit, enemyModel);

        var shoot = new ShootState<StatesEnum>(enemyModel,enemyModel.bullet,cantMaxToShoot);

        idle.AddTransition(StatesEnum.Patrol, patrol);
        idle.AddTransition(StatesEnum.Persuit, persuit);

        patrol.AddTransition(StatesEnum.Idle, idle);
        patrol.AddTransition(StatesEnum.Persuit, persuit);
        patrol.AddTransition(StatesEnum.GetAmmo, goToReload);

        persuit.AddTransition(StatesEnum.Idle, idle);
        persuit.AddTransition(StatesEnum.Patrol, patrol);
        persuit.AddTransition(StatesEnum.GetAmmo, goToReload);
        persuit.AddTransition(StatesEnum.Shoot, shoot);

        goToReload.AddTransition(StatesEnum.Patrol, patrol);
        goToReload.AddTransition(StatesEnum.Persuit, persuit);
        goToReload.AddTransition(StatesEnum.Shoot, shoot);

        shoot.AddTransition(StatesEnum.Persuit, persuit);
        shoot.AddTransition(StatesEnum.GetAmmo, goToReload);


        fsm.SetInit(patrol);
    }

    void InitializeTree()
    {
        var idle = new ActionNode(() => fsm.Transition(StatesEnum.Idle));
        var patrol = new ActionNode(() => fsm.Transition(StatesEnum.Patrol));
        var persuit = new ActionNode(() => fsm.Transition(StatesEnum.Persuit));
        var reload = new ActionNode(() => fsm.Transition(StatesEnum.GetAmmo));
        var shoot = new ActionNode(()=> fsm.Transition(StatesEnum.Shoot));

       

        var qCanShoot = new QuestionNode(CanShot, shoot, persuit);

        var qCheckAmmo = new QuestionNode(HaveAmmo, qCanShoot, reload);

        var qTargetPlayer = new QuestionNode(IsTargetView, qCheckAmmo, patrol);

        var qTired = new QuestionNode(IsTired, qTargetPlayer, persuit);

        root = qTired;
    }

    private bool IsTired()
    {
        currentTime +=Time.deltaTime;
        if (currentTime <= maxTimeTired)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool CanShot()
    {
        if (los.LOS() && GetDistanceTarget() > 5 && enemyModel.isReady)
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
            return true;
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