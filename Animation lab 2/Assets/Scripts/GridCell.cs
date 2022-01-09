using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;


    public class GridCell : Node {

        // Constructors 
        public GridCell(int i, Vector3 center, float cellSize) :base (i)
        {
            this.cellSize = cellSize;

            this.center = center;
            Debug.DrawRay(center, center + new Vector3(0, 0, 1));
        }

        public GridCell(Node n) : base(n)
        {
        }

        // You add any data needed to represent a grid cell node
        protected float cellSize;
        protected bool occupied;
        protected Vector3 center;
        

        //Methods to implement your grid cell node class

        public float getSize()
        {
            return this.cellSize;
        }

        public bool isOccupied()
        {
            return this.occupied;
        }

        public void setOccupied(bool occupied)
        {
            this.occupied = occupied;
        }

        public Vector3 getCenter()
        {
            return this.center;
        }
    }

