using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GulModel : BaseModel,IBoid
{
    [SerializeField] public NormalUnitStats unitStats;
    public List<Node> NodesList{ get => nodeskey; }
    public List<float> Nodesvalue { get => nodesvalue; }

   
    private void Awake()
    {
        healthController.SetMaxLife(unitStats.MaxLife);
    }
    public override void Move(Vector3 direction)
    {
        direction = obs.GetDir(direction);
        transform.position += direction * unitStats.Speed * Time.deltaTime;
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
}
