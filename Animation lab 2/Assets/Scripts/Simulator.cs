using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : MonoBehaviour
{
    /* Keep track of agents */

    public bool debugVisuals;
    public float threshold;
    private PathManager pathmanager;
    public static Simulator _instance = null;
    private ArrayList listOfAgents = new ArrayList(); //the list of all our agents
    public float smoothRotation = 1;
    private CrowdGenerator crowdGenerator;
    // Start is called before the first frame update
    void Start()
    {

        crowdGenerator = GetComponent<CrowdGenerator>();
        crowdGenerator.GenerateCrowd();
        SetGoalToAllAgents();
    }


    //Singleton pattern :  there can be only one instance of the Simulator Class
    public static Simulator GetInstance()
    {
        if (_instance == null)
        {
            GameObject _simulatorGameObject = new GameObject("Simulator");
            _instance = _simulatorGameObject.AddComponent<Simulator>();
        }
        return _instance;
    }
    

    // Update is called once per frame
    void Update()
    {
        moveAgentsTowardGoal();
        checkGoalsReached();
    }

    private void checkGoalsReached()
    {
        foreach (Agent a in listOfAgents)
        {
            if (Vector3.Distance(a.goal, a.transform.position) <= threshold)
            {
                a.AssignGoal();
                //print("coucougoal");
            }
        }

    }

    //For each agent, we make them move toward their goal at their speed
    private void moveAgentsTowardGoal()
    {
       foreach(Agent a in listOfAgents)
        {
            float positionChange = a.speed * Time.deltaTime;
            Vector3 direction = a.goal - a.transform.position;
            direction = direction.normalized;
            direction.y = 0.0f; 
            a.transform.position += positionChange * direction;

            //Quaternion angleToGoal = Quaternion.LookRotation(a.goal);
            //Quaternion.FromToRotation(this.transform.eulerAngles, goal - a.transform.position) ;
            //print(angleToGoal);

            // Dampen towards the target rotation
            //a.transform.rotation = Quaternion.Slerp(a.transform.rotation, a.angleToGoal, Time.deltaTime * smoothRotation);

            if (a.goal != Vector3.zero)
            {
                a.transform.rotation = Quaternion.Slerp(a.transform.rotation, a.angleToGoal, smoothRotation * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        
        foreach (Agent a in listOfAgents)
        {
            if (debugVisuals) Gizmos.DrawSphere(a.goal, 0.1f);
            
        }
    }

    /*
     * - Loop over agents
     *          - Read goal position from PathManager 
     *          - Set agent’s velocity towards the goal 
     *          
     * - Check scene boundaries         */
    void UpdateSimulation(float elapsedTime)
    {

    }

    public void AddAgent(Agent a)
    {
        listOfAgents.Add(a);
    }

    public void RemoveAgent(Agent a)
    {
        listOfAgents.Remove(a);
    }

    public Transform GetPosition(Agent a)
    {
        return a.transform;
    }

    public void SetGoalToAllAgents()
    {
        foreach (Agent a in listOfAgents)
        {
            a.AssignGoal();
        }
    }
}
