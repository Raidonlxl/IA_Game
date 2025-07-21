using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class BaseModel : MonoBehaviour, IBoid
{
    public LeaderStats stats;
    public TeamsLists targets;
    public TeamsLists allies;
    public GameObject bullet;
    public ObstacleAvoidance obs;

    public Rigidbody rb;

    public bool isReady;
    public bool isHealed;
    public Transform home;
    public Transform target;

    public GameObject pointToShoot;
    public string owner = "Self";
    public BulletController bulletController;

    public HealthController healthController;

    public GenericBehaviour genericBehaviour;

    public Transform lasPositionPlayer;
    public List<Node> nodes;
    public bool IsTired;
    public List<Node> nodeskey = new List<Node>();
    public List<float> nodesvalue = new List<float>();
    public Vector3 Position => transform.position;

    public Vector3 Forward => transform.forward;

    [SerializeField] float idleweight;
    [SerializeField] float patrolweight;
    [SerializeField] float fleeweight;

    public float IdleWeight { get => idleweight; set => idleweight = value; }
    public float PatrolWeight { get => patrolweight; set => patrolweight = value; }
    public float FleeWeight { get => fleeweight; set => fleeweight = value; }
    private void Awake()
    {
        allies.Team.Add(gameObject);
        
    }
     
    public virtual void Move(Vector3 direction)
    {

        direction = obs.GetDir(direction);
  
        direction.y = rb.linearVelocity.y;
        rb.linearVelocity = direction * stats.Speed;

        RotateEnemy(direction);
    }

    public virtual void RotateEnemy(Vector3 direction)
    {
        transform.forward = Vector3.Lerp(transform.forward, direction.normalized, 0.2f);
        
    }

    public virtual void Shoot(GameObject bullet, PoolGeneric<GameObject> pool)
    {
        
        bulletController = bullet.GetComponent<BulletController>();
        bulletController.SetOwner(owner);
        bulletController.transform.position = pointToShoot.transform.position;
        bulletController.transform.rotation = pointToShoot.transform.rotation;
        bulletController.bulletModel.pool = pool;
        
    }
    public void SetTired()
    {
        IsTired = true;
    }
    public void Death() 
    {
        Destroy(gameObject);
    }
}
