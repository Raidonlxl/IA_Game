using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewCharacter", menuName = "AddCharacter/NewCharacter")]
public class PlayerBase : ScriptableObject
{
    [SerializeField] private float maxLife;
    [SerializeField] private float currentTiredTime = 0;
    [SerializeField] private float maxTiredTime;

    [SerializeField] private float currentConter = 0;
    [SerializeField] private float maxConter;

    [SerializeField] private float maxTimeRuning;
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;


    public float Speed { get => speed; }
    public float RunSpeed { get => runSpeed; }
    public float CurrentTiredTime { get => currentTiredTime; set => currentTiredTime = value; }
    public float MaxTimeTired { get => maxTiredTime;}
    public float MaxTimeRuning { get => maxTimeRuning;}
    public float CurrentConter { get => currentConter; set => currentConter = value; }
    public float MaxConter { get => maxConter;}


}
