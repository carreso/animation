using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGenerator : MonoBehaviour
{


    public int nbOfAgents;
    public GameObject AgentPrefab;
    private float sizeOfTheAgent = 0.5f;
    private Simulator simulator;
    

    void Start()
    {
        //print("coucou from crowdgenerator");
        simulator = GetComponent<Simulator>();
        
        
    }

    //Generates a crowd of agents - number of agents is defined by  the public variable  nbOfAgents
    public void GenerateCrowd()
    {
        for (int i = 0; i < nbOfAgents; i++)
        {
            //Instantiate agent prefab at random position and random orientation
            var newAgent = Object.Instantiate(AgentPrefab, randomPosition(), randomOrientation());
            Agent agent = newAgent.GetComponent<Agent>();
            simulator.AddAgent(agent);

        }
    }

    //Returns a random position contained in the plane XZ =[-5,5]x[-5,5] and check if obtained position is not currently occupied by another agent
    Vector3 randomPosition()
    {
        Vector3 position = new Vector3(Random.Range(-5.0f + sizeOfTheAgent, 5.0f - sizeOfTheAgent), sizeOfTheAgent, Random.Range(-5.0f + sizeOfTheAgent, 5.0f - sizeOfTheAgent));
        var hitColliders = Physics.OverlapSphere(position, sizeOfTheAgent);

        if (hitColliders.Length > 1) //then you have someone with a collider here
            return randomPosition(); // so let's compute a new position

        return position;
    }


    //Returns a random orientation around the y axis between 0 and 179 degrees
    Quaternion randomOrientation()
    {
        Quaternion orientation = Quaternion.Euler(0, Random.Range(0.0f, 179.0f), 0);
        return orientation;
    }

    
    void Update()
    {
        
    }
}
