using UnityEngine;

public class BulletModel : MonoBehaviour,IPooleable
{
    public string owner;
    private EnemyModel enemyModel;
    private PlayerModel playerModel;
    private int damage = 10;
    public PoolGeneric<GameObject> pool;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyModel = collision.gameObject.GetComponent<EnemyModel>();

            if (enemyModel.owner != owner)
            {
                enemyModel.healthController.GetDamage(damage);
            }
        }

        else if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerModel = collision.gameObject.GetComponent<PlayerModel>();

            if (playerModel.owner != owner)
            {
                playerModel.healthController.GetDamage(damage);
            }
        }
        Recycle(gameObject);
      
     

    }

    public void Recycle(GameObject gameObject)
    {
        pool.Recycle(gameObject);
    }
}
