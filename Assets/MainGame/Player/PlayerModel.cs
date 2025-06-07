using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private PlayerBase playerBase;
    public Transform pointShoot;
    public BulletController bulletController;
    public GameObject bullet;
    public string owner = "Player";
    public PlayerBase PlayerBase { get; }
    public float Speed {  get => playerBase.Speed; }
    public HealthController healthController;

    private void Start()
    {
        healthController.SetMaxLife(100);
    }

    public void MoveFront(Vector3 direction)
    {
        transform.forward = InputManager.cameraController.transform.forward;
        transform.position += direction * playerBase.Speed * Time.deltaTime;
    }
    public void MoveSide(Vector3 direction)
    {
        transform.position += direction * playerBase.Speed * Time.deltaTime;
    }

    public void Shoot(GameObject bullet, PoolGeneric<GameObject> pool)
    {
        bulletController = bullet.GetComponent<BulletController>();
        bulletController.SetOwner(owner);
        bulletController.transform.position = pointShoot.transform.position;
        bulletController.transform.rotation = pointShoot.transform.rotation;
        bulletController.bulletModel.pool = pool;
    }

}
