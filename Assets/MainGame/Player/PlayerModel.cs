using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public void MoveFront(Vector3 direction, float currentSpeed)
    {
        transform.forward = direction;
        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    public void MoveSide(Vector3 direction, float currentSpeed)
    {
        transform.position += direction * currentSpeed * Time.deltaTime;
    }
}
