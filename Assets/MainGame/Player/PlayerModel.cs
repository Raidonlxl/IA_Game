using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private PlayerBase playerBase;
    public PlayerBase PlayerBase { get; }
    public float Speed {  get => playerBase.Speed; }
    public void MoveFront(Vector3 direction)
    {
        transform.forward = direction;
        transform.position += direction * playerBase.Speed * Time.deltaTime;
    }

    public void MoveSide(Vector3 direction)
    {
        transform.position += direction * playerBase.Speed * Time.deltaTime;
    }
}
