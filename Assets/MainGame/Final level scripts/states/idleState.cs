using UnityEngine;

public class idleState<T> : State<T>
{
    BaseModel model;
    float idleweight;
    
    public idleState(BaseModel model, float idleweight)
    {
        this.model = model;
        this.idleweight = idleweight;
    }
    public override void Enter()
    {
        base.Enter();
        int hp = model.healthController.currentHealth;
        int max = model.stats.MaxLife;
        if (hp <= max / 2)
        {
            idleweight += 10f;
        }

    }
    public override void Execute() 
    {
        int hp = model.healthController.currentHealth;
        int max = model.stats.MaxLife;
        if (hp <= max / 2)
        {
            idleweight += 10f;
        }
    }
}
