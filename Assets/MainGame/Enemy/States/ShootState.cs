using UnityEngine;

public class ShootState<T> : State<T>
{
    EnemyModel self;
    PoolGeneric<GameObject> pool;
    BulletController bulletController;
    float maxTimeToshot=0.3f;
    float currentTime;
    int cantMaxToShoot;
    int currentBullet;
    
    public ShootState(EnemyModel self,GameObject bullet, int cantMaxToShoot)
    {
        pool = new PoolGeneric<GameObject>();
        this.self = self;
        pool._originalPrefab = bullet;
        this.cantMaxToShoot = cantMaxToShoot;
        pool.InitializePool(cantMaxToShoot);
    }

    public override void Enter()
    {
        base.Enter();
        currentTime = 0;
        
    }

    public override void Execute()
    {
        base.Execute();
        if (currentTime >= maxTimeToshot)
        {
            if (currentBullet < cantMaxToShoot)
            {
                var instance = pool.GetFromPool();
                instance.SetActive(true);
                self.Shoot(instance);
                currentTime = 0;
                currentBullet++;
            }
            else
            {
                self.isReady = false;

            }
            
        }
        currentTime += Time.deltaTime;

    }

    public override void Exit() 
    {
        base.Exit();
        currentTime = 0;
        currentBullet = 0;

    }

}
