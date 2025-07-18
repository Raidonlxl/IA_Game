using System.Collections.Generic;
using UnityEngine;

public class LeaderModel : MonoBehaviour
{
    public LeaderStats stats;
    public TeamsLists targets;
    public TeamsLists allies;
    public GameObject bullet;
    [SerializeField]
    private ObstacleAvoidance obs;
    public bool isReady;
    public bool isHealed;
    public GameObject pointToShoot;
    public BulletController bulletController;

    public HealthController healthController;

    public GenericBehaviour genericBehaviour;

    public Transform lasPositionPlayer;
    public List<Node> nodes;
    public bool isTired;
    public List<Node> nodeskey = new List<Node>();
    public List<float> nodesvalue = new List<float>();
    public Vector3 Position => transform.position;

    public Vector3 Forward => transform.forward;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
