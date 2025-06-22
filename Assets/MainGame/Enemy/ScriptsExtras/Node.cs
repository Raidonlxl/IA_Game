using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neightbourds;//<- Esto es lo unico que importa
    public float weight = 1;

    //Si utilizan este codigo con los raycast en el start/update/realtime son un punto menos por raycast.
    
    
    
    private void Start()
    {
        /*
        GetNeightbourd(Vector3.right);
        GetNeightbourd(Vector3.left);
        GetNeightbourd(Vector3.forward);
        GetNeightbourd(Vector3.back);*/
    }
    private void Update()
    {
        
    }
    void GetNeightbourd(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, 50f))
        {
            var node = hit.collider.GetComponent<Node>();
            if (node != null)
                neightbourds.Add(node);
        }
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < neightbourds.Count; i++)
        {

            Gizmos.DrawLine(transform.position, neightbourds[i].transform.position);

        }
    }

}
