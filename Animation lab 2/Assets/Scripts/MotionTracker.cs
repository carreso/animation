using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script keeps track of the movement of the character :
 *  Displacement vectors (world and local) !!!!!!!!!!
 *  Speed
 *  Forward vector

 */
public class MotionTracker : MonoBehaviour
{


    public Vector3 velocity;
    public float rotationSpeed;
    public Vector3 directionVector;

    private Vector3 previousPosition;
    private Quaternion previousRotation;

  
    void Start()
    {
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

   
    void Update()
    {

        //Computing velocity, orientation, and direction
        velocity = (transform.position - previousPosition) / Time.deltaTime;
        directionVector = transform.forward;
        Debug.DrawRay(transform.position, transform.forward);
        previousPosition = transform.position;
         



    }



}
