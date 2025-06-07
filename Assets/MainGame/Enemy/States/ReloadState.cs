using UnityEngine;
using System;
using Unity.Mathematics;

public class ReloadState<T> : State<T>
{
    Transform[] reloadsBoxs;
    EnemyModel self;
    Timer timer;
    Transform selected;
    float x= math.INFINITY;
    ISteering steering;
    public ReloadState(ISteering steering, Transform[] reloadsBoxs, EnemyModel self)
    {
        this.reloadsBoxs = reloadsBoxs;
        this.self = self;
        this.steering = steering;
        timer = new Timer(0, 5);
    }

    public override void Enter()
    {
        Debug.Log("Reload");
        timer.ResetTimer();
        base.Enter();
        for (int i = 0; i < reloadsBoxs.Length; i++)
        {
            float a = Vector3.Distance(self.transform.position, reloadsBoxs[i].position);

            if (a < x)
            {
                x = a;
                selected = reloadsBoxs[i];
            }
        }

        steering.Refresh(selected);
       
    }

    public override void Execute()
    {
        base.Execute();

        if (Vector3.Distance(self.transform.position, selected.position) < 2)
        {
            timer.Run();
            if (timer.IsCompleted())
            {
                self.isReady = true;
            }
        }
        else
        {
            self.Move(steering.GetDir());
        }
     
    }
    public override void Exit() => base.Exit();
}
