using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutOrganizer : MonoBehaviour
{
    [Header("===Matrix Builder===")]
    [HideInInspector] public int rowSize = 5;
    [Range(3,5)] public int columnSize = 3;
    [Header("===Object Spacing===")]
    [Range(0,10)]public float spaceBetweenColumns = 2;
    [Range(0,10)]public float spaceBetweenRows = 2;
    // Constructors
    public Transform[,] pickups3D;
    float middleColumn;
    float middleRow = 2;
    List<Transform> rowList = new List<Transform>();
    float columnIteration = 0;
    float rowIteration = 0;

    public void Populate3DArray()
    {
        Clear3DArray();
        pickups3D = new Transform[rowSize,columnSize];
        middleColumn = (float)((columnSize - 1f) / 2f);
        middleRow = (float)((rowSize - 1f) / 2f);
        foreach(Transform x in transform) rowList.Add(x);
        for(int x = 0; x < rowSize; x++)
        {
            for(int i = 0; i < columnSize; i++)
            {
                if(i < rowList[x].childCount)
                {
                    pickups3D[x, i] = rowList[x].GetChild(i);
                    Debug.Log("Added to the Matrix = "+ x.ToString() + "/" + i.ToString() + "/" + rowList[x].GetChild(i).name);
                }
                else if(i >= rowList[x].childCount && i < columnSize)
                {
                    GameObject go = new GameObject("Pickup-X");
                    go.transform.parent = rowList[x];
                    pickups3D[x,i] = go.transform;
                    Debug.Log("Couldn't fill row size, Created an object named = " + go.transform.name);
                } 
            }
        }
    }
    public void RowSpacing()
    {
        rowIteration = 0;
        foreach(Transform p in rowList) 
        {
            float zPos = (rowIteration - middleRow) * spaceBetweenRows;
            p.position = new Vector3(p.position.x, p.position.y, zPos); 
            rowIteration++;
            if(rowIteration >= rowSize) rowIteration = 0;
        }
        Debug.Log("Row Spacing Completed");

    }
    public void ColumnSpacing()
    {
        columnIteration = 0;
        Debug.Log(middleColumn);
        foreach(Transform p in pickups3D)
        {
            float xPos = (columnIteration - middleColumn) * spaceBetweenColumns;
            p.position = new Vector3(xPos, p.position.y, p.position.z);
            columnIteration++;
            if(columnIteration >= columnSize) columnIteration = 0;
        } 
        Debug.Log("Column Spacing Completed");
    }
    public void Clear3DArray()
    {
        pickups3D = null;
        columnIteration = 0;
        rowIteration = 0;
        rowList.Clear();
        Debug.Log("Matrix Cleared");
    }
}
