using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer<T> : State<T>
{
    ISteering steering;
    KeyModel keyModel;
    Transform target;
    LeaderBehaviour leaderBehaviour;
    Timer timer;
    public FollowPlayer(KeyModel keyModel, LeaderBehaviour leaderBehaviour, Transform target, ISteering steering)
    {
        this.steering = steering;
        this.keyModel = keyModel;
        this.target = target;
        this.leaderBehaviour = leaderBehaviour;
        timer = new Timer(0, 10);
    }
    public override void Enter()
    {
        base.Enter();
        timer.ResetTimer();
        leaderBehaviour.IsActive = true;
        leaderBehaviour.target = target;
        leaderBehaviour._isPursuit = true;
    }
    public override void Execute()
    {
        if (!timer.IsCompleted()) 
        {
            timer.Run();

            keyModel.Move(steering.GetDir());
        }
        else
        {
            keyModel.isTired = true;
        }
    }
    public override void Exit()
    {
        base.Exit();
        leaderBehaviour.IsActive = false;
        leaderBehaviour._isPursuit = false;

    }
}
