using UnityEngine;

public class BulletModel : MonoBehaviour,IPooleable
{
    public string owner;
    private MeleeEnemyModel enemyModel;
    private EnemyModel enemyModelRange;
    private PlayerModel playerModel;
    private int damage = 10;
    public PoolGeneric<GameObject> pool;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Boids"))
        {
            
            enemyModel = collision.gameObject.GetComponent<MeleeEnemyModel>();
            if(enemyModel != null)
            {
                enemyModel._healthController.GetDamage(damage);
            }
            else
            {
                enemyModelRange = collision.gameObject.GetComponent<EnemyModel>();
                if(enemyModelRange != null && enemyModelRange.owner != owner)
                {
                    enemyModelRange.healthController.GetDamage(damage);
                }
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
