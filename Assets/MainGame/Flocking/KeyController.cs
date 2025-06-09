using UnityEngine;

public class KeyController : MonoBehaviour
{
    public Rigidbody target;
    FSM<StatesEnum> _fsm;
    KeyModel keyModel;
    ITreeNode root;
    FlockingManager flocking;

    private void Start()
    {
        keyModel = GetComponent<KeyModel>();
        InitializeFSM();
    }
    void InitializeFSM()
    {
        var leaderBehaviour = GetComponent<LeaderBehaviour>();
        var obs = GetComponent<ObstacleAvoidance>();
        flocking = GetComponent<FlockingManager>();

        _fsm = new FSM<StatesEnum>();

        var idleState = new Idle<StatesEnum>(transform,leaderBehaviour);

        var steeringPersuit = new Persuit(transform, target.transform, 2);

        var tiredState = new TiredState<StatesEnum>(keyModel);
        var moveState = new FollowPlayer<StatesEnum>(keyModel,leaderBehaviour,target.transform,flocking);

        idleState.AddTransition(StatesEnum.Run, moveState);
        idleState.AddTransition(StatesEnum.Tired, tiredState);
           
        moveState.AddTransition(StatesEnum.Idle, idleState);
        moveState.AddTransition(StatesEnum.Tired, tiredState);

        tiredState.AddTransition(StatesEnum.Idle, idleState);
        tiredState.AddTransition(StatesEnum.Run, moveState);

        _fsm.SetInit(idleState);

        var idle = new ActionNode(()=>_fsm.Transition(StatesEnum.Idle));
        var move = new ActionNode(()=>_fsm.Transition(StatesEnum.Run));
        var tired = new ActionNode(()=>_fsm.Transition(StatesEnum.Tired));

        var qCanMove = new QuestionNode(IsFarAway, idle, move);

        var qTired = new QuestionNode(IsTired, tired, qCanMove);

        root = qTired;

    }

    public bool IsFarAway()
    {
        return Vector3.Distance(keyModel.transform.position, target.transform.position) > flocking.radius;
    }

    private bool IsTired()
    {
        return keyModel.isTired;
    }
    void Update()
    {
        root.Execute();
        _fsm.OnExecute();
    }

}
