using UnityEngine;

public class BulletModel : MonoBehaviour,IPooleable
{
    public string owner;
    private MeleeEnemyModel enemyModel;
    private EnemyModel enemyModelRange;
    private PlayerModel playerModel;
    private BaseModel BaseModel;
    private int damage = 10;
    public PoolGeneric<GameObject> pool;
    private TeamsLists myTeam;

    private void OnCollisionEnter(Collision collision)
    {
        /*
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
        }*/
        Recycle(gameObject);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boids")) 
        {
            BaseModel = collision.gameObject.GetComponent<BaseModel>();
            if (!myTeam.Team.Contains(BaseModel.gameObject))
            {
                BaseModel.healthController.GetDamage(damage);
            }
        }
        
    }

    public void Recycle(GameObject gameObject)
    {
        pool.Recycle(gameObject);
    }

    public void SetTeam(TeamsLists MyTeam)
    {
        myTeam = MyTeam;
    }
}
