using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
   
    public float elasticity_spring;
    public float dampening_spring;

    public Particle particle1;
    public Particle particle2;
    public float length_spring;

    //public Rope rope;

    public void InitializeSpring(float elasticity_spring, float dampening_spring, Particle particle1, Particle particle2)
    {
        this.elasticity_spring = elasticity_spring;
        this.dampening_spring = dampening_spring;
        this.particle1 = particle1;
        this.particle2 = particle2;
        this.length_spring = Vector3.Distance(particle1.position, particle2.position);//Initial distance between the two particles
        this.transform.position = (this.particle1.position + this.particle2.position) / 2;

    }

    public void ApplySpringForces()
    {
        //Debug.Log(this.particle2);

        Vector3 springForce = (this.elasticity_spring * (Vector3.Distance(particle1.position, particle2.position) - this.length_spring) + this.dampening_spring * Vector3.Dot((particle1.velocity - particle2.velocity), (particle1.position - particle2.position).normalized)) * (particle1.position - particle2.position).normalized;
        this.particle1.AddForce(-springForce);
        this.particle2.AddForce(springForce);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LateUpdate()
    {
        this.transform.position = (this.particle1.position + this.particle2.position) / 2;
    }





}
