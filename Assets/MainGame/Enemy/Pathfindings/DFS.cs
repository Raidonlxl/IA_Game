using System.Collections.Generic;
using System;
using UnityEngine;

public class DFS
{
    public static List<T> Run<T>(T start, Func<T, bool> isSatisfied, Func<T, List<T>> getConnections, int watchdog = 500, int watchdogPath = 500)
    {
        Dictionary<T, T> parents = new Dictionary<T, T>();
        Stack<T> pending = new Stack<T>();
        HashSet<T> visited = new HashSet<T>();

        pending.Push(start);
        while (pending.Count > 0)
        {
            watchdog--;
            if (watchdog <= 0) break;
            T current = pending.Pop();
            Debug.Log("DFS");
            if (isSatisfied(current))
            {
                List<T> path = new List<T>();
                path.Add(current);
                while (parents.ContainsKey(path[path.Count - 1]))
                {
                    watchdogPath--;
                    if (watchdogPath <= 0) break;
                    path.Add(parents[path[path.Count - 1]]);
                }
                path.Reverse();
                return path;
            }
            else
            {
                visited.Add(current);
                List<T> connections = getConnections(current);

                for (int i = 0; i < connections.Count; i++)
                {
                    T child = connections[i];
                    if (visited.Contains(child) || pending.Contains(child)) continue;
                    pending.Push(child);
                    parents[child] = current;
                }
            }
        }

        return new List<T>();
    }
}
