using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This component takes tracked information from tracker
and creates proper locomotion animation with the animator controller
/!\ max is useful ????????
*/
public class LocomotionManager : MonoBehaviour
{

    private MotionTracker tracker;
    private Animator animator;

    public float maxVelX = 1.0f;
    public float maxVelZ = 1.0f;
    public bool dragCharacterWithKeyboard = true;
    public float smoothingFactor = 1.0f;

    private float smoothHoz;
    private float smoothVert;
    private float horizontalVel;
    private float verticalVel;


    void Start()
    {
        animator = GetComponent<Animator>();
        tracker = GetComponent<MotionTracker>();
    }

    // After Tracker has updated => we update the aniamtion
    void LateUpdate()
    {   // Reads data from Tracker
        float goalHorizontal = tracker.velocity.x;
        float goalVertical = tracker.velocity.z;

        //Smoothe the velocity values so that the animation is smooth
        horizontalVel = Mathf.SmoothDamp(horizontalVel, goalHorizontal, ref smoothHoz, smoothingFactor);
        verticalVel = Mathf.SmoothDamp(verticalVel, goalVertical, ref smoothVert, smoothingFactor);


        //Sets animator parameters depending on this velocity
        animator.SetFloat("VelX", horizontalVel/maxVelX);
        animator.SetFloat("VelZ", verticalVel/maxVelZ);
    }
}
