using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    float maxSpeed;
    float agentSize = 0.5f;
    public float speed;
    public Vector3 velocity;
    public Quaternion angleToGoal;

    public Vector3 goal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Assign a goal to the agent
    public void AssignGoal()
    {
        goal = new Vector3(Random.Range(-5.0f + agentSize, 5.0f - agentSize), agentSize, Random.Range(-5.0f + agentSize, 5.0f - agentSize));
        angleToGoal = Quaternion.LookRotation(this.goal - this.transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        print("coucou");
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

    }
}
