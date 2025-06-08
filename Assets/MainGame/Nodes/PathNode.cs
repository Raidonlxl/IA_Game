using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PathNode : MonoBehaviour
{
    public List<PathNode> neighborgs;


    private void OnDrawGizmos()
    {
        for (int i = 0; i < neighborgs.Count; i++)
        {

            Gizmos.DrawLine(transform.position, neighborgs[i].transform.position);

        }
    }
}