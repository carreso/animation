using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{

    public Vector3 goal;
    private float sizeOfTheAgent = 0.5f;

    //Assign random goal to the agent within world limits
    public void AssignGoal(Agent a)
    {
        goal = new Vector3(Random.Range(-5.0f + sizeOfTheAgent, 5.0f - sizeOfTheAgent), sizeOfTheAgent, Random.Range(-5.0f + sizeOfTheAgent, 5.0f - sizeOfTheAgent));
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
