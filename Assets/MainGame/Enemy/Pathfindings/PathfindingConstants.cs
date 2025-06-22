using System.Collections.Generic;
using UnityEngine;

public static class PathfindingConstants
{
    public const float nearRadius = 5;
    public static LayerMask nodeMask = LayerMask.GetMask("Nodes");
    public static LayerMask obsMask = LayerMask.GetMask("Wall");
    
}
