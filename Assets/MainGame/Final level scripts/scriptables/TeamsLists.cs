using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName="team",menuName = "team")]
public class TeamsLists : ScriptableObject
{
   public List<GameObject> Team = new List<GameObject>();
}
