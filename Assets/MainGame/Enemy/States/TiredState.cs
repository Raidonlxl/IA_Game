using UnityEngine;

public class TiredState<T> : State<T>
{
    private BaseModel self;
    private Timer timer;
    
    public TiredState(BaseModel self)
    {
        this.self = self;
        timer = new Timer(0, 10f);
    }
    public override void Enter()
    {
        Debug.Log("Tired");
        base.Enter();
        timer.ResetTimer();
    }

    public override void Execute()
    {
        base.Execute();
       
        if (!timer.IsCompleted()) 
        { 
            timer.Run();
        }
        else
        {
            self.SetTired();
        }
    }
}
