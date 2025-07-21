using UnityEngine;

public class CallAssemblyState<T> : State<T>
{
    TeamsLists myteam;
    public CallAssemblyState(BaseModel enemyModel)
    {
        myteam = enemyModel.allies;
    }

    public override void Enter()
    {
        base.Enter();
        for (int i = 0; i < myteam.Team.Count; i++)
        {
            myteam.Team[i].haveAssemble = true;
        }
    }
}
