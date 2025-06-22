using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class EnemyModel : MonoBehaviour, IBoid
{
    [Header("LineOfSight")]
    public EnemyBase enemyBase;
    public GameObject target;
    public GameObject bullet;
    public PlayerModel playerModel;
    [SerializeField]
    private ObstacleAvoidance obs;
    public bool isReady;
    public GameObject pointToShoot;
    public BulletController bulletController;

    public HealthController healthController;

    public GenericBehaviour genericBehaviour;

    public Transform lasPositionPlayer;
    public bool isTired;

    public string owner = "Enemy";
    public bool endWay;
    public List<Node> _nodeskey = new List<Node>();
    public List<float> _nodesvalue = new List<float>();
    public Vector3 Position => transform.position;

    public Vector3 Forward => transform.forward;

    private void Awake()
    {
        genericBehaviour = GetComponent<GenericBehaviour>();
        playerModel = target.GetComponent<PlayerModel>();
        obs = gameObject.GetComponent<ObstacleAvoidance>();
        isTired= true;
        healthController.SetMaxLife(50);
    }
 
    public virtual void Move(Vector3 direction)
    {
        
        direction = obs.GetDir(direction);
        transform.position += direction * enemyBase.Speed * Time.deltaTime;
        RotateEnemy(direction);
    }
    
    public virtual void RotateEnemy(Vector3 direction)
    {
        transform.forward = Vector3.Lerp(transform.forward,direction.normalized,0.2f);
    }

    public void Shoot(GameObject bullet, PoolGeneric<GameObject> pool)
    {
        bulletController = bullet.GetComponent<BulletController>();
        bulletController.SetOwner(owner);
        bulletController.transform.position = pointToShoot.transform.position;
        bulletController.transform.rotation = pointToShoot.transform.rotation;
        bulletController.bulletModel.pool = pool;
    }
}
