using UnityEngine;

public class GulModel : BaseModel
{

    public override void Move(Vector3 direction)
    {
        base.Move(direction);
    }

    public override void RotateEnemy(Vector3 direction)
    {
        base.RotateEnemy(direction);
    }

    public override void Shoot(GameObject bullet, PoolGeneric<GameObject> pool)
    {
        base.Shoot(bullet, pool);
    }
}
