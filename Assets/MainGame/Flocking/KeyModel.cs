using UnityEngine;

public class KeyModel : EnemyModel, IBoid
{
    /*
    public Vector3 Position => transform.position;
    ObstacleAvoidance obs;
    public Vector3 Forward => transform.forward;

    private void Start()
    {
        obs = gameObject.GetComponent<ObstacleAvoidance>();
        playerModel = target.GetComponent<PlayerModel>();
        isTired = true;
        healthController.SetMaxLife(50);
    }
    public override void Move(Vector3 direction)
    {
        direction = obs.GetDir(direction);
        transform.position += direction * 5* Time.deltaTime;
        RotateEnemy(direction);
    }

    public override void RotateEnemy(Vector3 direction)
    {
        transform.forward = Vector3.Lerp(transform.forward, direction.normalized, 0.2f);
    }*/

}
