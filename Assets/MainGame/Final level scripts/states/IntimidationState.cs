using UnityEngine;
using System.Collections.Generic;

public class IntimidationState<T> : State<T>
{
    LeaderModel model;

    public IntimidationState(LeaderModel model)
    {
        this.model = model;
    }
    public override void Enter()
    {
        model.Intimidate();
    }
}
