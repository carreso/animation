using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;

public class Grid : FiniteGraph<GridCell, CellConnection, GridConnections>
{ 
    
    public void create2DGrid(int width, int height, float xMin, float xMax, float zMin, float zMax, float obstacleProbability, float cellSize)
    {
        //créer les cellules
        for (int i=0; i<height; i++)
        {
            for (int j=0; j<width; j++)
            {
                
                Vector3 center = new Vector3((xMin + cellSize/2) + j*cellSize, 0.0f, (zMin + cellSize / 2) - i * cellSize);
                GridCell cell = new GridCell(i*width + j, center, cellSize);
                
                //randomly put it occupied or not
                bool obstacle = (Random.Range(0.0f, 1.0f) <= obstacleProbability);
                Debug.Log(obstacle);
                cell.setOccupied(obstacle);

                //add it to the graph
                nodes.Add(cell);
                
            }
        }

        //pour chaque cellule, créer les connections avec les voisins si elle n'est pas occupée
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GridCell cellToConnect = getNode(i*width + j);
                
                //this is the list of all my connections
                GridConnections allMyCellConnections = new GridConnections();
                GridCell neighbour = getNode((i * width) + j+1);//right
                createConnectionWithNeighbour(cellToConnect, neighbour, allMyCellConnections, cellSize, leftOrRight: true);
                neighbour = getNode(i * width + j - 1);//left
                createConnectionWithNeighbour(cellToConnect, neighbour, allMyCellConnections, cellSize, leftOrRight: true);
                neighbour = getNode((i + 1) * width + j);//bottom
                createConnectionWithNeighbour(cellToConnect, neighbour, allMyCellConnections, cellSize) ;
                neighbour = getNode((i - 1) * width + j);//top
                createConnectionWithNeighbour(cellToConnect, neighbour, allMyCellConnections, cellSize);

                
                neighbour = getNode((i + 1) * width + j + 1); //diagonal bottom right
                createConnectionWithNeighbour(cellToConnect, neighbour, allMyCellConnections, cellSize, diagonal: true);
                neighbour = getNode((i + 1) * width + j - 1); //diagonal bottom left
                createConnectionWithNeighbour(cellToConnect, neighbour, allMyCellConnections, cellSize, diagonal: true);
                neighbour = getNode((i - 1) * width + j + 1); //diagonal top right
                createConnectionWithNeighbour(cellToConnect, neighbour, allMyCellConnections, cellSize, diagonal: true);
                neighbour = getNode((i - 1) * width + j - 1);//diagonal top left
                createConnectionWithNeighbour(cellToConnect, neighbour, allMyCellConnections, cellSize, diagonal: true);

                //then add this list to the list of the connections of the graph
                this.connections.Add(allMyCellConnections);

                /*description of all the neighbours
                GridCell neighbourright = getNode(i * width + j + 1);
                GridCell neighbourleft = getNode(i * width  + j-1);
                GridCell neighbourbot = getNode((i+1) * width + j);
                GridCell neighbourtop = getNode((i-1) * width + j);

                GridCell neighbourdiagbotright = getNode((i+1) * width + j+1);
                GridCell neighbourdiagnbotleft = getNode((i + 1) * width + j-1);
                GridCell neighbourdiagtopright = getNode((i - 1) * width + j+1);
                GridCell neighbourdiagtopleft = getNode((i - 1) * width + j-1);*/

            }
        }


    }

    void createConnectionWithNeighbour(GridCell cellToConnect, GridCell neighbour, GridConnections allMyCellConnections, float cellSize, bool leftOrRight=false, bool diagonal=false)
    {
        bool condition = true;
        if (neighbour != null)
        {
            if (leftOrRight) { condition = (cellToConnect.getCenter().z == neighbour.getCenter().z); }
            if (diagonal) { condition = (cellToConnect.getCenter().z == neighbour.getCenter().z + cellSize) || (cellToConnect.getCenter().z == neighbour.getCenter().z - cellSize); }
            if (condition)
            { 
                CellConnection connection = new CellConnection(cellToConnect, neighbour);
                allMyCellConnections.Add(connection);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
