using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class BaseModel : MonoBehaviour
{
    public LeaderStats stats;
    public TeamsLists targets;
    public TeamsLists allies;
    public GameObject bullet;
    [SerializeField] private ObstacleAvoidance obs;

    public bool isReady;
    public bool isHealed;

    public Transform target;

    public GameObject pointToShoot;

    public BulletController bulletController;

    public HealthController healthController;

    public GenericBehaviour genericBehaviour;

    public Transform lasPositionPlayer;
    public List<Node> nodes;
    public bool isTired;
    public List<Node> nodeskey = new List<Node>();
    public List<float> nodesvalue = new List<float>();
    public Vector3 Position => transform.position;

    public Vector3 Forward => transform.forward;

    private void Awake()
    {
        allies.Team.Add(gameObject);
    }
     
    public virtual void Move(Vector3 direction)
    {
        /*
        direction = obs.GetDir(direction);
        transform.position += direction * enemyBase.Speed * Time.deltaTime;
        RotateEnemy(direction);*/
    }

    public virtual void RotateEnemy(Vector3 direction)
    {/*
        transform.forward = Vector3.Lerp(transform.forward, direction.normalized, 0.2f);
        */
    }

    public virtual void Shoot(GameObject bullet, PoolGeneric<GameObject> pool)
    {
        /*
        bulletController = bullet.GetComponent<BulletController>();
        bulletController.SetOwner(owner);
        bulletController.transform.position = pointToShoot.transform.position;
        bulletController.transform.rotation = pointToShoot.transform.rotation;
        bulletController.bulletModel.pool = pool;
        */
    }
    public void SetTired()
    {
        isTired = true;
    }
}
