using UnityEngine;

public class Intimidate : MonoBehaviour
{
    public TeamsLists enemies;

    void OnTriggerEnter(Collider other)
    {
       var gul = other.gameObject.GetComponent<GulModel>();
        if(gul != null)
        {
            if (enemies.Team.Contains(gul.gameObject))
            {
                gul.IsScared = true;
            }
        }
    }
}
