using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFollowPoints<T> : State<T>
{
    protected List<Vector3> _waypoints;
    int _index;
    protected Transform _entity;
    float _distanceToPoint = 0.2f;
    bool _isFinishPath;
    ObstacleAvoidance _avoidance;
    public StateFollowPoints(Transform entity, ObstacleAvoidance avoidance, float distanceToPoint = 0.2f)
    {
        _entity = entity;
        _distanceToPoint = distanceToPoint;
        _isFinishPath = true;
        _avoidance = avoidance;
    }
    public StateFollowPoints(Transform entity, List<Vector3> waypoints, ObstacleAvoidance avoidance, float distanceToPoint = 0.2f)
    {
        _entity = entity;
        _distanceToPoint = distanceToPoint;
        _waypoints = waypoints;
        _isFinishPath = true;
        _avoidance = avoidance;
    }

    
    public void SetWaypoints(List<Vector3> newPoints)
    {
        if (newPoints.Count == 0) return;
        _waypoints = newPoints;
        _index = 0;
        _isFinishPath = false;
        Debug.Log("waypoints: " + _waypoints.Count);
        OnStartPath();
    }
    protected void Run(Vector3 dir)
    {
        
        if (_isFinishPath) return;
        Vector3 point = _waypoints[_index];
        point.y = _entity.position.y;
        dir = point - _entity.position;
        _avoidance.GetDir(dir);
        if (dir.magnitude < _distanceToPoint)
        {
            if (_index + 1 < _waypoints.Count)
                _index++;
            else
            {
                _isFinishPath = true;
                Debug.Log("has finished path");
                OnFinishPath();
                return;
            }
        }
        
        OnMove(dir.normalized);
    }
    protected virtual void OnMove(Vector3 dir)
    {

    }
    protected virtual void OnStartPath()
    {

    }
    protected virtual void OnFinishPath()
    {

    }
    public bool IsFinishPath => _isFinishPath;
}
