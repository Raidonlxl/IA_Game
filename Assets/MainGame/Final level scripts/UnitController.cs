using UnityEngine;

public class UnitController : MonoBehaviour
{
    BaseModel model;
    FSM<StatesEnum> fsm;
    Tree root;
    LineOfSight los;

    [SerializeField] float idleweight;
    [SerializeField] float patrolweight;
    [SerializeField] float fleeweight;

    public float IdleWeight { get => idleweight; set => idleweight = value; }
    private void Awake()
    {
        model = GetComponent<BaseModel>();
        los = GetComponent<LineOfSight>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initializeFSM();
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnExecute();
    }
    //mover esta funcion rearrangeweights a el model
    void initializeFSM()
    {
        fsm = new FSM<StatesEnum>();
        var idle = new idleState<StatesEnum>(model, IdleWeight);

        fsm.SetInit(idle);
    }
}
