using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    private Grid grid;

    public int width, height; //number of cells we want

    public float xMin;//where the grid will be positionned
    public float xMax;
    public float zMin;
    public float zMax;
    public float obstacleProbability;
    public float cellSize;

    public GameObject prefabObstacle;
    public bool boolDebugVisuals;
    // Start is called before the first frame update
    void Start()
    {
       
        grid = new Grid();
        grid.create2DGrid(width, height, xMin, xMax, zMin, zMax, obstacleProbability, cellSize);
        createObstacles();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void createObstacles()
    {
        foreach (GridCell cell in grid.nodes)
        {
            if (cell.isOccupied())
            {
                var obstacle = Object.Instantiate(prefabObstacle, cell.getCenter() + new Vector3(0,0.5f,0), new Quaternion(0,0,0,0));
            }
        }
    }
    //helps to debug
    private void OnDrawGizmos()
    {
        if (boolDebugVisuals)
        {
            foreach(GridCell cell in grid.nodes)
            {
                if (cell != null) {
                    Gizmos.DrawSphere(cell.getCenter(), 0.2f);
                    Handles.Label(cell.getCenter()+ new Vector3(0.3f, 0,0), cell.getId().ToString());
                }
                
            }

            foreach (GridConnections connections in grid.connections)
            {
                foreach (CellConnection connection in connections.connections) {
                    if (connection != null)
                        Gizmos.DrawLine(connection.getFromNode().getCenter(), connection.getToNode().getCenter());
                }

            }

        }
    }
}
