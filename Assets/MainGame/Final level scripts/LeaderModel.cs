using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LeaderModel : BaseModel
{
    [SerializeField] float intimidationweight;

    public LeaderStats LeaderStats;

    public GameObject ScareBubble;

    [SerializeField] float regroupWeight;
    public float IntimidationWeight { get => intimidationweight; set => intimidationweight = value; }

    public float RegroupWeight { get => regroupWeight; set => regroupWeight = value; }
    public List<Node> NodesList { get => nodeskey; }
    public List<float> Nodesvalue { get => nodesvalue; }

    public Transform baseTransform;



    Coroutine IntimidationTimeCooldown;

    private void Awake()
    {
        healthController.SetMaxLife(LeaderStats.MaxLife);
    }
    public override void Move(Vector3 direction)
    {
        direction = obs.GetDir(direction);
        transform.position += direction * LeaderStats.Speed * Time.deltaTime;
        RotateEnemy(direction);

    }

    public override void RotateEnemy(Vector3 direction)
    {
        base.RotateEnemy(direction);

    }

    public override void Shoot(GameObject bullet, PoolGeneric<GameObject> pool)
    {
        base.Shoot(bullet, pool);

    }
    public void Intimidate()
    {
        ScareBubble.SetActive(true);
        IntimidationTimeCooldown = StartCoroutine(IntimidationTime());
    }

    IEnumerator IntimidationTime()
    {
        yield return new WaitForSeconds(2f);
        ScareBubble.SetActive(false);
        IntimidationTimeCooldown = null;
    }
}
