using UnityEngine;

public class idleState<T> : State<T>
{
    BaseModel model;
    
    public idleState(BaseModel model)
    {
        this.model = model;
    }
    public override void Enter()
    {
        base.Enter();
        int hp = model.healthController.currentHealth;
        int max = model.stats.MaxLife;
        if (hp <= max / 2)
        {
            model.FleeWeight += 40f;
            model.IdleWeight = 10f; 
        }
        else
        {
            model.FleeWeight = 10f;
            model.IdleWeight += 10f; 
        }
    }
   
}
