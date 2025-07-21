using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName="team",menuName = "final level/team", order =0)]
public class TeamsLists : ScriptableObject
{
   public List<BaseModel> Team = new List<BaseModel>();
}
