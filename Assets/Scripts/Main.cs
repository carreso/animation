using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{   //BALLS
    public GameObject ballPrefab;
    public GameObject[] balls;
    public GameObject [] spheres;
    private GameObject[] planes;
    public float elasticity_particles;

    //ROPE
    public float length;
    public float nbOfParticles;
    public GameObject ropePrefab;
    public float elasticity_spring;
    public float dampening_spring;

    //PARAMETERS
    public Vector3 gravity;
    public float k;
    public float deltaTime;
    private Rope myRope;
    public float friction;

    
   
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;
        GameObject rope = Instantiate(ropePrefab);
        rope.GetComponent<Rope>().InitializeRope(length, nbOfParticles, elasticity_particles, elasticity_spring, dampening_spring);
        myRope = rope.GetComponent<Rope>();

        balls = GameObject.FindGameObjectsWithTag("ParticleTag");
        planes = GameObject.FindGameObjectsWithTag("PlaneCollisionTag");
        spheres = GameObject.FindGameObjectsWithTag("SphereCollisionTag");
        
        //Debug.Log(planes);
        Debug.Log(balls.Length);
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;
       //Add forces gravity + springs


        myRope.springForcesAndGravity(gravity);

        //Debug.Log(balls.Length);
        foreach (GameObject ball in balls)
        {
            Particle ballObj = ball.GetComponent<Particle>();
            if (!ballObj.ball1)
            {
                //Solve with Velvet

                ballObj.VelvetSolver(k, deltaTime);

                //Check Collisions with planes
                foreach (GameObject plane in planes)
                {
                    Plane p = plane.GetComponent<Plane>();
                    ballObj.collisionWithPlane(p, deltaTime, friction); //This does the correction
                }

                //Check Collisions with Spheres
                foreach (GameObject sphere in spheres)
                {
                    Sphere s = sphere.GetComponent<Sphere>();
                    ballObj.collisionWithSphere(s, deltaTime, friction); //This does the correction
                }


                //move
                ballObj.Move();
                
            }
        }
    }
}
