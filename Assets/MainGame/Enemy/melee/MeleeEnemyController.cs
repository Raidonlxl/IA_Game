using System;
using UnityEngine;


public class MeleeEnemyController : MonoBehaviour
{
    [SerializeField] MeleeEnemyModel _model;
    [SerializeField] Transform[] _wayPoints;
    [SerializeField] Rigidbody _target;
    [SerializeField] bool _chooseSteering;
    [SerializeField] bool _enableSteering;
  
    FSM<StatesEnum> _fsm;
    ITreeNode _root;
    
    ObstacleAvoidance _avoidance;
    ISteering _patrol;
    ISteering _persuit;

    public Action OnAttack;
    public Action OnDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _avoidance = GetComponent<ObstacleAvoidance>();
        intializeStreering();
        InitializeFSM();
        InitializeTree();
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.OnExecute();
        _root.Execute();
    }

    void intializeStreering()
    {
        _patrol = new Patrol(transform, _wayPoints);
        _persuit = new Persuit(transform, _target);
    }

    void InitializeFSM()
    {
        _fsm = new FSM<StatesEnum>();
        var idle = new EnemyIdleState<StatesEnum>(_model);
        var patrol = new EnemyPatrolState<StatesEnum>(_model, _patrol, _avoidance);
        var chase = new EnemyChaseState<StatesEnum>(_persuit, _model, _avoidance);
        var attack = new EnemyAttackState<StatesEnum>(_model);
        var dead = new EnemyDeadState<StatesEnum>(gameObject, _model);

        idle.AddTransition(StatesEnum.Patrol, patrol);
        idle.AddTransition(StatesEnum.Persuit, chase);
        idle.AddTransition(StatesEnum.Shoot, attack);
        idle.AddTransition(StatesEnum.Dead, dead);

        patrol.AddTransition(StatesEnum.Idle, idle);
        patrol.AddTransition(StatesEnum.Persuit, chase);
        patrol.AddTransition(StatesEnum.Shoot, attack);
        patrol.AddTransition(StatesEnum.Dead, dead);

        chase.AddTransition(StatesEnum.Idle, idle);
        chase.AddTransition(StatesEnum.Patrol, patrol);
        chase.AddTransition(StatesEnum.Shoot, attack);
        chase.AddTransition(StatesEnum.Dead, dead);

        attack.AddTransition(StatesEnum.Idle, idle);
        attack.AddTransition(StatesEnum.Persuit, chase);
        attack.AddTransition(StatesEnum.Patrol, patrol);
        attack.AddTransition(StatesEnum.Dead, dead);

        _fsm.SetInit(idle);
    }

    void InitializeTree()
    {
        ITreeNode idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        ITreeNode patrol = new ActionNode(() => _fsm.Transition(StatesEnum.Patrol));
        ITreeNode chase = new ActionNode(() => _fsm.Transition(StatesEnum.Persuit));
        ITreeNode attack = new ActionNode(() => _fsm.Transition(StatesEnum.Shoot));
        ITreeNode dead = new ActionNode(() => _fsm.Transition(StatesEnum.Dead));

        ITreeNode qIsPatroling = new QuestionNode(QuestionIsIdling, idle, patrol);
        ITreeNode qHasReachedFoe = new QuestionNode(QuestionHasReachedFoe, attack, chase);
        ITreeNode qLOS = new QuestionNode(QuestionLOS, qHasReachedFoe, qIsPatroling);
        ITreeNode qIsAlive = new QuestionNode(() => _model.IsAlive, dead, qLOS);
        _root = qIsAlive;
    }

    bool QuestionLOS()
    {
        if (LineOfSight.LOS(transform, _target.transform, _model.Stats.Range, _model.Stats.Angle, _model.Stats.ObstacleMask))
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    bool QuestionHasReachedFoe()
    {
        if(LineOfSight.LOS(transform, _target.transform, 1, _model.Stats.Angle, _model.Stats.ObstacleMask))
        {

            return true;
        }
        else if(_model.IsAttacking)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool QuestionIsIdling()
    {
        return _model.IsIdling;
    }
}
