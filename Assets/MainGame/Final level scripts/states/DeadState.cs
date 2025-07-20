using UnityEngine;

public class DeadState<T> : State<T>
{
    BaseModel model;

    public DeadState(BaseModel model)
    {
        this.model = model;
    }
    
}
