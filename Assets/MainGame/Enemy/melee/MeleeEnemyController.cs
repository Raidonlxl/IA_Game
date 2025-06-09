using System;
using System.Collections.Generic;
using UnityEngine;


public class MeleeEnemyController : MonoBehaviour
{
    [SerializeField] MeleeEnemyModel _model;
    [SerializeField] Transform _target;
    
  
    FSM<StatesEnum> _fsm;
    ITreeNode _root;
    LineOfSight _los;
    
    ObstacleAvoidance _avoidance;
    ISteering _persuit;
    IMove _move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _move = _model;
        _los = GetComponent<LineOfSight>();
        _los.Initialize(transform, _target, _model.Stats.Range, _model.Stats.Angle, _model.Stats.ObstacleMask);
        _avoidance = GetComponent<ObstacleAvoidance>();
        InitializeFSM();
        InitializeTree();
        
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.OnExecute();
        _root.Execute();
        Debug.Log(_fsm._currState);
        Debug.Log(_model.IsOnLastSeenPos);
    }


    void InitializeFSM()
    {
        _persuit = new Persuit(transform, _target, 2f);
        _fsm = new FSM<StatesEnum>();
        var idle = new EnemyIdleState<StatesEnum>(_model);
        var patrol = new EnemyPatrolState<StatesEnum>(_model, _persuit, _avoidance, transform, _move, _target);
        var chase = new EnemyChaseState<StatesEnum>(_target,_persuit, _model, _avoidance, transform,_move, _target);
        var attack = new EnemyAttackState<StatesEnum>(_model);
        var dead = new EnemyDeadState<StatesEnum>(gameObject, _model);
        var gotolastpoint = new EnemyGoToLastPointState<StatesEnum>(_model, _persuit, _avoidance, transform, _move, _target);

        idle.AddTransition(StatesEnum.Patrol, patrol);
        idle.AddTransition(StatesEnum.Persuit, chase);
        idle.AddTransition(StatesEnum.Shoot, attack);
        idle.AddTransition(StatesEnum.Dead, dead);
        idle.AddTransition(StatesEnum.GoToLastPoint, gotolastpoint);

        patrol.AddTransition(StatesEnum.Idle, idle);
        patrol.AddTransition(StatesEnum.Persuit, chase);
        patrol.AddTransition(StatesEnum.Shoot, attack);
        patrol.AddTransition(StatesEnum.Dead, dead);
        patrol.AddTransition(StatesEnum.GoToLastPoint, gotolastpoint);

        chase.AddTransition(StatesEnum.Idle, idle);
        chase.AddTransition(StatesEnum.Patrol, patrol);
        chase.AddTransition(StatesEnum.Shoot, attack);
        chase.AddTransition(StatesEnum.Dead, dead);
        chase.AddTransition(StatesEnum.GoToLastPoint, gotolastpoint);

        attack.AddTransition(StatesEnum.Idle, idle);
        attack.AddTransition(StatesEnum.Persuit, chase);
        attack.AddTransition(StatesEnum.Patrol, patrol);
        attack.AddTransition(StatesEnum.Dead, dead);
        attack.AddTransition(StatesEnum.GoToLastPoint, gotolastpoint);

        gotolastpoint.AddTransition(StatesEnum.Idle, idle);
        gotolastpoint.AddTransition(StatesEnum.Persuit, chase);
        gotolastpoint.AddTransition(StatesEnum.Patrol, patrol);
        gotolastpoint.AddTransition(StatesEnum.Shoot, attack);
        gotolastpoint.AddTransition(StatesEnum.Dead, dead);

        _fsm.SetInit(idle);
    }

    void InitializeTree()
    {
        ITreeNode idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        ITreeNode patrol = new ActionNode(() => _fsm.Transition(StatesEnum.Patrol));
        ITreeNode chase = new ActionNode(() => _fsm.Transition(StatesEnum.Persuit));
        ITreeNode attack = new ActionNode(() => _fsm.Transition(StatesEnum.Shoot));
        ITreeNode dead = new ActionNode(() => _fsm.Transition(StatesEnum.Dead));
        ITreeNode gotolastpoint = new ActionNode(() => _fsm.Transition(StatesEnum.GoToLastPoint));

        ITreeNode qIsPatroling = new QuestionNode(QuestionIsIdling, idle, patrol);
        ITreeNode qHasReachedFoe = new QuestionNode(QuestionHasReachedFoe, attack, chase);
        ITreeNode qHasReachedLastSeenPos = new QuestionNode(() => _model.IsOnLastSeenPos, qIsPatroling, gotolastpoint);
        ITreeNode qLOS = new QuestionNode(QuestionLOS, qHasReachedFoe, qHasReachedLastSeenPos);
        ITreeNode qIsAlive = new QuestionNode(() => _model.IsAlive, dead, qLOS);
        _root = qIsAlive;
    }

    bool QuestionLOS()
    {
        if (_los.LOS())
        {
            return true;
        }
        else if (_model.IsChasing)
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
        if(_los.CustomLOS(1f))
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
