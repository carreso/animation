using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCloth : MonoBehaviour
{   //BALLS
    public GameObject ballPrefab;
    public GameObject[] balls;
    public GameObject[] spheres;
    private GameObject[] planes;
    private GameObject[] springs;
    public Cloth cloth;
    public float elasticity_particles;
    
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
        
        balls = GameObject.FindGameObjectsWithTag("ParticleTag");
        planes = GameObject.FindGameObjectsWithTag("PlaneCollisionTag");
        spheres = GameObject.FindGameObjectsWithTag("SphereCollisionTag");
        //springs = GameObject.FindGameObjectsWithTag("SpringTag");

        //Debug.Log("hi" + springs.Length);
        //Debug.Log(balls.Length);

    }

    // Update is called once per frame
    void Update()
    {
        if (balls.Length == 0)
        {
            balls = GameObject.FindGameObjectsWithTag("ParticleTag");
            
        }


        deltaTime = 0.07f;
        cloth.simulateSpringForces();


        foreach (GameObject ball in balls)
        {
            
            Particle ballObj = ball.GetComponent<Particle>();
            if (!ballObj.ball1)
            {
                //AddForces
                //Gravity
                ballObj.totalForces += gravity;

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
