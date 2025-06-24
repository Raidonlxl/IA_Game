using UnityEngine;

public class ShootState<T> : State<T>
{
    private EnemyModel self;
    private PoolGeneric<GameObject> pool;
    private float maxTimeToShoot = 0.3f;
    private float currentTime;
    private int maxBulletsToShoot;
    private int currentBulletCount;
    ISteering steering;

    public ShootState(EnemyModel self, GameObject bulletPrefab, int maxBulletsToShoot,ISteering steering)
    {
        this.self = self;
        this.maxBulletsToShoot = maxBulletsToShoot;
        this.steering = steering;
        pool = new PoolGeneric<GameObject>();
        pool.originalPrefab = bulletPrefab;
        pool.InitializePool(maxBulletsToShoot);
    }

    public override void Enter()
    {
        Debug.Log("shoot");
        base.Enter();
        currentTime = 0f;
        currentBulletCount = 0;
    }

    public override void Execute()
    {
        base.Execute();
        currentTime += Time.deltaTime;

        if (currentTime >= maxTimeToShoot && currentBulletCount < maxBulletsToShoot)
        {
            GameObject bullet = pool.GetFromPool();

            if (bullet != null)
            {
                self.Shoot(bullet,pool);

                currentBulletCount++;
                currentTime = 0f;
            }
        }

        if (currentBulletCount >= maxBulletsToShoot)
        {
            self.isReady = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
        currentTime = 0f;
        currentBulletCount = 0;
        
    }
}
