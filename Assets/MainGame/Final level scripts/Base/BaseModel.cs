using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class BaseModel : MonoBehaviour
{
    public LeaderStats stats;
    public TeamsLists targets;
    public TeamsLists allies;
    public GameObject bullet;
    [SerializeField] public ObstacleAvoidance obs;

    public bool isReady;
    public bool isHealed;
    private bool isScared;
    private bool isTired;

    public Transform target;

    public Transform baseTransform;

    public GameObject pointToShoot;

    public BulletController bulletController;

    public HealthController healthController;

    public GenericBehaviour genericBehaviour;

    public Transform lasPositionPlayer;



    [SerializeField] protected List<Node> nodeskey;
    [SerializeField] protected List<float> nodesvalue;

    public bool IsScared { get => isScared;}
    public bool IsTired { get => isTired;}
    public Vector3 Position => transform.position;

    public Vector3 Forward => transform.forward;

    private void Awake()
    {
       // allies.Team.Add(gameObject);
   
    }
     
    public virtual void Move(Vector3 direction)
    {
        /*
        direction = obs.GetDir(direction);
        transform.position += direction * enemyBase.Speed * Time.deltaTime;
        RotateEnemy(direction);*/
    }

    public virtual void RotateEnemy(Vector3 direction)
    {
        transform.forward = Vector3.Lerp(transform.forward, direction.normalized, 0.2f);
        
    }

    public virtual void Shoot(GameObject bullet, PoolGeneric<GameObject> pool)
    {
        
        bulletController = bullet.GetComponent<BulletController>();
        //bulletController.SetOwner(owner);
        bulletController.transform.position = pointToShoot.transform.position;
        bulletController.transform.rotation = pointToShoot.transform.rotation;
        bulletController.bulletModel.pool = pool;
        
    }
    public void SetTired()
    {
        isTired = true;
    }
   
    public void SetRested()
    {
        isTired=false;
    }
    public void SetScared()
    {
        isScared = true;
    }

    public void SetNormal()
    {
        isScared = false;
    }
}
