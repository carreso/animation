using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*This script moves the character in the scene according to the user input */
public class TestMotion : MonoBehaviour
{
    //This is the speed of our character, which means how fast her position is gonna change as we press the keys for moving
    public float characterSpeed;
    //This is the rotationspeed of our character, which means how fast her rotation is gonna change as we press the left and right keys 
    public float angularSpeed;
    private float velX;
    private float velZ;
    public bool fixOrientation = false;
    public Animator animator;
    public float smoothFactor;
    private float animationVelocityX;
    private float animationVelocityZ;
    public float smoothingFactor = 1.0f;

    private float smoothHoz;
    private float smoothVert;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        MovePosition();
        MoveRotation();

        /*
        animationVelocityX= Mathf.SmoothDamp(animationVelocityX, velX, ref smoothHoz, smoothingFactor);
        animationVelocityZ = Mathf.SmoothDamp(animationVelocityZ, velZ, ref smoothHoz, smoothingFactor);
        print(animationVelocityX);
        print(animationVelocityZ);
        animator.SetFloat("VelX", animationVelocityX);
        animator.SetFloat("VelZ", animationVelocityZ);*/
    }


    void MoveRotation()
    {
        //fixOrientation boolean has to be false if we want to rotate
        if (!fixOrientation) {
        float angleRotation = 0.0f;
            //if user has pressed left arrow
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                angleRotation = -angularSpeed * Time.deltaTime;
            }

            //if user has pressed right arrow
            else if (!Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                angleRotation = angularSpeed * Time.deltaTime;
            }
            else
            {
                return;
            }

            //We rotate according to this computed angle 
            transform.Rotate(new Vector3(0.0f, angleRotation, 0.0f));
        }
    }
    //Make the character move according to the user input
    void MovePosition()
    {
        computeVelocityfromInput2();
        transform.Translate(velX, 0, velZ );
    }

    //Computes velocity according to the user input
    void computeVelocityfromInput()
    {
        velX = 0.0f;
        velZ = 0.0f;

        //character moves up 
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            velZ = characterSpeed * Time.deltaTime;
        }
        //character goes down 
        if (!Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            velZ = -characterSpeed * Time.deltaTime;
        }
        //character moves to the left 
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            velX = -characterSpeed * Time.deltaTime;
        }
        //character moves to the right 
        if (!Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            velX = characterSpeed * Time.deltaTime;
        }


        //If we press the shift key we can run 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velX *= 2.0f;
            velZ *= 2.0f;
        }

        
    }

    //Computes velocity according to the user input - test
    void computeVelocityfromInput2()
    {
        velZ = Input.GetAxis("Vertical") * characterSpeed * Time.deltaTime;
        velX = Input.GetAxis("Horizontal") * characterSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velX *= 2.0f;
            velZ *= 2.0f;
        }

       
    }
}
