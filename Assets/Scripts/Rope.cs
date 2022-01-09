using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float length;
    public float nbOfParticles;
    public GameObject particle; //initiate particles

    public List<Particle> particles = new List<Particle>();
    public float elasticity_particles;
    public float elasticity_spring;
    public float dampening_spring;
    private float springLength;


    //private List<Spring> springs;

    public void InitializeRope(float length, float nbOfParticles, float elasticity_particles, float elasticity_spring, float dampening_spring)
    {
        this.length = length;
        this.nbOfParticles = nbOfParticles;
        this.springLength = this.length / (this.nbOfParticles - 1);
        this.elasticity_particles = elasticity_particles;
        this.elasticity_spring = elasticity_spring;
        this.dampening_spring = dampening_spring;

       // Debug.Log(springLength);
        for (int i = 0; i < this.nbOfParticles ; i++)
        {

            //Cloning the Prefab
            GameObject part = Instantiate(particle);
            
            //So that we know we are at ball 1
            if (i == 0)
            {
                part.GetComponent<Particle>().ball1 = true;
            }
            //Initialize particle
            Vector3 initialPosition = (transform.right * this.length * i) / (this.nbOfParticles-1);
            Vector3 initialVelocity = new Vector3(0.0f, 0.0f, 0.0f);

            if (part != null)
            {
                
                Particle part_obj = part.GetComponent<Particle>();
                part_obj.elasticity_particle = this.elasticity_particles;
                part_obj.InitializeParticle(initialPosition, initialVelocity, elasticity_particles, 0.08f, 0.7f);

                //Vector3 initialPosition, Vector3 initialVelocity, float elasticity, float deltaTime, float mass
                //part_obj.VelvetSolver(0.99f, 0.07f,new Vector3(1,1,1));
                //float k, float deltaTime, vector3 f
                //part_obj.Move();
                this.particles.Add(part_obj); //We add each particle to the linked list


            }


        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void springForcesAndGravity(Vector3 gravity)
    {
        //Vector3 TotalForces;
        Vector3 springForce;

        //Pour chaque particule
        for (int i = 0; i < this.particles.Count -2; i++)
        {
            //Debug.Log("i"+ i);
            //Debug.Log("nb parti : "+particles.Count);
            
            Particle part = this.particles[i];
            //On réinitialise TotalForces
            
            //On calcule F1m1
            float relative_distance = Vector3.Distance(this.particles[i + 1].position, part.position);
            Vector3 quotient = (this.particles[i + 1].position - part.position) / relative_distance;
            float part_1 = elasticity_spring * (relative_distance - this.springLength);
            //Debug.Log(elasticity_spring);
            float part_2 = dampening_spring * Vector3.Dot(this.particles[i + 1].velocity - part.velocity, quotient);
            springForce = (part_1 + part_2) * quotient;
            //TotalForces += Force;

            //On applique F1m1 à P1
            this.particles[i].totalForces += springForce;
            //Debug.Log(springForce);
            //On applique -F1m1 à P2
            this.particles[i+1].totalForces += - springForce;

            
        }

        for (int i=0; i<this.nbOfParticles -1; i++) {
            particles[i].totalForces += gravity;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}