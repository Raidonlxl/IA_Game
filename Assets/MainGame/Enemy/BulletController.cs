using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    public BulletModel bulletModel;
    public Timer timer;
    public TeamsLists allies;

    private void Start()
    {
        timer = new Timer(0,5);
        bulletModel.SetTeam(allies);
    }
    public void SetOwner(string owner)
    {
        bulletModel.owner = owner;
    }
    private void Update()
    {
        timer.Run();
        if (!timer.IsCompleted())
        {
            transform.position += transform.forward * Time.deltaTime * 5;
        }

        else
        {
            bulletModel.pool.Recycle(gameObject);
            timer.ResetTimer();
        }

    }

    
}
