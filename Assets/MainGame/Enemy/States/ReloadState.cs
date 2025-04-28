using UnityEngine;
using System;
using Unity.Mathematics;

public class ReloadState<T> : State<T>
{
    Transform[] reloadsBoxs;
    EnemyModel self;
    float timer;
    float maxTimeRecharger=5;
    bool isReady;
    Transform selected;
    float x= math.INFINITY;
    public ReloadState(Transform[] reloadsBoxs, EnemyModel self)
    {
        this.reloadsBoxs = reloadsBoxs;
        this.self = self;
       
    }

    public override void Enter()
    {
        base.Enter();
        timer = 0;
        

    }

    public override void Execute()
    {
        base.Execute();


        for (int i = 0; i < reloadsBoxs.Length; i++)
        {
            float a = Vector3.Distance(self.transform.position, reloadsBoxs[i].position);

            if (a < x)
            {
                x = a;
                selected = reloadsBoxs[i];
            }
        }

        if(Vector3.Distance(self.transform.position, selected.position) > 2)
        {
           Vector3 direction = selected.position - self.transform.position;
            self.Move(direction.normalized);    
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > maxTimeRecharger)
            {
                self.isReady = true;
            }
        }
     
    }

    public override void Exit() => base.Exit();
}
