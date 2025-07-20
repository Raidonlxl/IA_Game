using UnityEngine;

public class ShootState<T> : State<T>
{
    private BaseModel self;
    private PoolGeneric<GameObject> pool;
    private float maxTimeToShoot = 0.3f;
    private float currentTime;
    private int maxBulletsToShoot;
    private int currentBulletCount;

    public ShootState(BaseModel self, GameObject bulletPrefab, int maxBulletsToShoot)
    {
        this.self = self;
        this.maxBulletsToShoot = maxBulletsToShoot;

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
