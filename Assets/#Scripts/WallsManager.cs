using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsManager : MonoBehaviour
{
    public List<Wall> walls = new List<Wall>();

    public BoolVariable wallsPurchasedStatus;

    private void Start()
    {
        foreach(Transform x in transform)
        {
            Wall wall = x.GetComponent<Wall>();
            wall.DeactivateWall();
            walls.Add(wall);
        }
    }

    public void ReBuildWalls()
    {
        foreach(Wall wall in walls)
        {
            wall.Build();
        }
    }
}
