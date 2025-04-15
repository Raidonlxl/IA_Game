using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    private EnemyModel enemyModel;
    private FSM<StatesEnum> fsm;

    [SerializeField] private Transform[] waypoints;
    [SerializeField] Rigidbody target;
    private void Start()
    {
        enemyModel = GetComponent<EnemyModel>();
    }

    private void Update()
    {

        if (LineOfSight.LOS(enemyModel.transform, enemyModel.target, enemyModel.enemyBase.Range, enemyModel.enemyBase.Angle, enemyModel.enemyBase.ObstacleMask))
        {
            Debug.Log("Watching");
        }
    }

    void InitializeSteering()
    {
        var seek = new Seek(enemyModel.transform, enemyModel.transform);
        var flee = new Flee(enemyModel.transform,enemyModel.target);
      
    }
    void InitializeFsm()
    {
        fsm = new FSM<StatesEnum>();

        var idle = new Idle(enemyModel.transform);
        var patrol = new Patrol(enemyModel.transform,waypoints);
        var persuit = new Persuit(transform, target);
    }
}
