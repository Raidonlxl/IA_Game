using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "AddEnemy/NewEnemy")]
public class EnemyBase : ScriptableObject
{
    [SerializeField] private float maxLife;
    [SerializeField] private float currentTiredTime = 0;
    [SerializeField] private float maxTiredTime;

    [SerializeField] private float currentConter = 0;
    [SerializeField] private float maxConter;

    [SerializeField] private float maxTimeRuning;
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float angle;
    [SerializeField] private float range;
    [SerializeField] private LayerMask obsMask;

    public float Speed { get => speed; }
    public float RunSpeed { get => runSpeed; }
    public float CurrentTiredTime { get => currentTiredTime; set => currentTiredTime = value; }
    public float MaxTimeTired { get => maxTiredTime; }
    public float MaxTimeRuning { get => maxTimeRuning; }
    public float CurrentConter { get => currentConter; set => currentConter = value; }
    public float MaxConter { get => maxConter; }
    public float Angle { get => angle;}
    public float Range { get => range;}
    public LayerMask ObstacleMask { get => obsMask;}

}
