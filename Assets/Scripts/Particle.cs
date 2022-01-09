using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public float mass;
    public float elasticity_particle;

    public Vector3 previousPosition;
    public Vector3 nextPosition;
    public Vector3 totalForces;
    public Boolean ball1 = false;
    public List<Particle> neighbourParts;

    //initialize class
    public void InitializeParticle(Vector3 initialPosition, Vector3 initialVelocity, float elasticity, float deltaTime, float mass)
    {
        this.position = initialPosition;
        this.velocity = initialVelocity;
        this.mass = mass;
        this.elasticity_particle = elasticity;
        this.previousPosition = position - velocity * deltaTime;
        this.neighbourParts = new List<Particle>();
        this.totalForces = new Vector3(0.0f, 0.0f,0.0f);


}


    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    //Detects collision with a plane and corrects position if collision
    public void collisionWithPlane(Plane plane, float deltaTime, float friction){
        float sign = Vector3.Dot(this.nextPosition, plane.normal) + plane.d;
        sign += Vector3.Dot(this.position, plane.normal) + plane.d;

        if (sign <= 0)
        {
            this.PositionCorrection(plane.normal, plane.d, deltaTime, friction);
        }

    }

    //PositionCorrection with VerletSolver
    public void PositionCorrection(Vector3 normal, float d, float deltaTime, float friction){
        velocity = (position - previousPosition)/deltaTime;
        Vector3 normalVelocity = Vector3.Dot(velocity, normal) * normal; //projection 
        Vector3 tangentVelocity = velocity - normalVelocity;
        velocity =- (1+ elasticity_particle) *Vector3.Dot(velocity, normal) * normal;
        //friction
        velocity -= friction * tangentVelocity;
        //updating velocity
        position -= (1+ elasticity_particle) *(Vector3.Dot(position, normal) +d) * normal;
        previousPosition = position - velocity*deltaTime;
    }

    //Verlet Solver for a particle 
    public void VelvetSolver(float k, float deltaTime){
        
        this.nextPosition = this.position + k *(this.position-this.previousPosition) + (Time.deltaTime* Time.deltaTime * this.totalForces)/this.mass;
        previousPosition = this.position;
        this.position = this.nextPosition;
        velocity = (position - this.previousPosition) / deltaTime;
        //Debug.Log("position: " + position);
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        this.totalForces = new Vector3(0.0f,0.0f, 0.0f); //reset the forces at the end of the visualization
        this.position = this.transform.position;
    }

    public void Move()
    {
        this.transform.position = position;
    }


    //To compute intersection Point with Sphere
    Vector3 intersectionPoint(Vector3 Point1, Vector3 Point2, Sphere sphere)
    {
        Vector3 sphereCenter = sphere.centerPosition;
        float a = Mathf.Pow(Point2.x - Point1.x, 2) + Mathf.Pow(Point2.y - Point1.y, 2) + Mathf.Pow(Point2.z - Point1.z, 2);
        float b = 2 * ((Point2.x - Point1.x) * (sphereCenter.x - Point1.x) + ((Point2.y - Point1.y) * (sphereCenter.y - Point1.y)) + (Point2.z - Point1.z) * (sphereCenter.z - Point1.z));
        float c = Mathf.Pow(sphereCenter.x - Point1.x, 2) + Mathf.Pow(sphereCenter.y - Point1.y, 2) + Mathf.Pow(sphereCenter.z - Point1.z, 2);
        float t1 = (-b + Mathf.Sqrt(Mathf.Pow(b, 2) - 4 * a * c)) / 2 * a;
        float t2 = (-b - Mathf.Sqrt(Mathf.Pow(b, 2) - 4 * a * c)) / 2 * a;
        Debug.Log(t1);

        //if ((t1 < 1 || 0 < t1) && (t2 < 1 || 0 < t1))
        //{
        //    return Vector3.negativeInfinity;
        //}

        //On prend le plus petit des deux
        if (t1 <= t2)
        {
            return (Point1 * t1 + Point2 * (1 - t1));
        }
        return (Point1 * t2 + Point2 * (1 - t2)); ;

    }

    Vector3 intersectionPointSphere (Vector3 currentPos, Vector3 nextPos, Sphere sphere)
    {
        Vector3 l = sphere.centerPosition - currentPos;
        Vector3 d = (nextPos - currentPos).normalized; //vecteur direction du mouvement 
        float s = Vector3.Dot(l, d);
        float l_2 = Vector3.Dot(l, l);
        float r_2 = Mathf.Pow(sphere.radius, 2);

        if ( l_2 < r_2 || s>0)
        {
            float m_2 = Vector3.Dot(l, l) - Mathf.Pow(s, 2);
            if (m_2 < r_2)
            {
                float q = Mathf.Sqrt(r_2 - m_2);
                if (l_2 < r_2)
                {
                    float t = s - q;
                    return currentPos + t * d;
                }
            }
        }
        return currentPos;
    }

    //Detect collision With sphere and corrects position 
    public void collisionWithSphere(Sphere sphere, float deltaTime, float friction)
    {
        

        //Si nextPosition se trouve à l'intérieur de la sphère, il y a une collision
        if (Vector3.Distance(this.nextPosition, sphere.centerPosition) <= sphere.radius)
        {
            //Debug.Log("coucou ya collision");
            //Calculer le point d'intersection avec la sphère pour
            
            Vector3 intPoint = intersectionPointSphere(this.position, this.nextPosition, sphere);
            

            //Calculer l'equation du plan
            Vector3 normal = (this.position - sphere.centerPosition).normalized; //OP
            float d = -Vector3.Dot(normal, intPoint);
            //Debug.DrawLine(intPoint, intPoint + normal);

            //Corriger la position de la particule
            this.PositionCorrection(normal, d, deltaTime, friction);

        }

        //Sinon on fait rien
    }


    public void AddForce(Vector3 force)
    {
        this.totalForces += force;
    }


}