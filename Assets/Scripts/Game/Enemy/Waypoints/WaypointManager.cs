using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;

    public List<Path> Paths = new List<Path>();

    private void Awake()
    {
        Instance = this;
    }

    public Vector3 GetSpawnPosition(int pathIndex)
    {
        return Paths[pathIndex].WayPoints[0].position;
    }
}

[System.Serializable]
public class Path
{
    public List<Transform> WayPoints = new List<Transform>();
}
