using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName="team",menuName = "final level/team", order =0)]
public class TeamsLists : ScriptableObject
{
   public List<GameObject> Team = new List<GameObject>();
}
