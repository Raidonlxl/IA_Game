using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveSteering<T> : State<T>
{
    ISteering steering;
    EnemyModel enemyModel;
    MeleeEnemyModel meleeModel;
    private Timer timer;
    ISteering flocking;
    GenericBehaviour genericBehaviour;
    public MoveSteering(GenericBehaviour generic, ISteering steering,ISteering flocking, EnemyModel enemyModel)
    {
        this.steering = steering;
        this.enemyModel = enemyModel;
        timer = new Timer(0, 7);
        this.flocking = flocking;
        genericBehaviour = generic;
        enemyModel.lasPositionPlayer = enemyModel.target.transform;
        steering.Refresh(enemyModel.lasPositionPlayer);
    }
    public MoveSteering(GenericBehaviour generic, ISteering steering, ISteering flocking, MeleeEnemyModel enemyModel)
    {
        this.steering = steering;
        meleeModel = enemyModel;
        timer = new Timer(0, 7);
        this.flocking = flocking;
        genericBehaviour = generic; 
        
    }
    public MoveSteering(ISteering steering, KeyModel enemyModel)
    {
        this.steering = steering;
        timer = new Timer(0, 7);
    }

    public override void Enter()
    {
        base.Enter();
        timer.ResetTimer();
        Debug.Log(enemyModel.target.transform);
        steering.Refresh(enemyModel.lasPositionPlayer);
        if (steering.GetType()==typeof(MoveToWaypoints))
        {
           

            Debug.Log("PATROL");
        }
        else if (steering.GetType() == typeof(Persuit))
        {
            Debug.Log("PERSUIT");

        }

    }
    public override void Execute()
    {
        base.Execute();
        if (steering.GetDir() != null)
        {
            if (steering.GetType() == typeof(Persuit))
            {
                if (enemyModel != null)
                {
                    if (!enemyModel.isTired)
                    {
                        genericBehaviour.Dir = steering.GetDir();
                        enemyModel.Move(steering.GetDir());
                        timer.Run();
                    }
                    if (timer.IsCompleted())
                    {
                        enemyModel.isTired = true;
                    }
                }
                if (meleeModel != null)
                {
                    genericBehaviour.Dir = steering.GetDir();
                    meleeModel.Move(steering.GetDir());
                }
            }
            else
            {
         
                genericBehaviour.Dir = steering.GetDir();

                if (enemyModel != null) enemyModel.Move(flocking.GetDir());
                if (meleeModel != null) meleeModel.Move(flocking.GetDir());
            }
        }
    }
    public override void Exit()
    {
        base.Exit();

        
      
           //meleeModel.LastSeenPos.position = meleeModel.Target.transform.position;
        
       
    }
    public void ChangeSteering(ISteering newSteering)
    {
        steering = newSteering;
    }
    IEnumerator RedoRoullete()
    {
        yield return new WaitForSeconds(3f);
        if (steering.GetType() == typeof(MoveToWaypoints))
        {
            if (enemyModel != null) ChangeRoulleteValues(enemyModel._nodeskey, enemyModel._nodesvalue, enemyModel.target.transform);
            if (enemyModel != null) ChangeRoulleteValues(enemyModel._nodeskey, enemyModel._nodesvalue, meleeModel.Target);
        }
    }
    public void ChangeRoulleteValues(List<Node> nodes, List<float> weight, Transform target)
    {

        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 distance = target.transform.position - nodes[i].transform.position;
            if (distance.magnitude < 10)
            {
                weight[i] = distance.magnitude + 40;
            }
            else
            {
                weight[i] = distance.magnitude / 4;
            }
        }

    }
}
