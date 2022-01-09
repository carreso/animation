using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float d;
    public Vector3 normal;
    

    // Start is called before the first frame update
    void Start()
    {
        this.normal = transform.up.normalized;
        this.d = -Vector3.Dot(normal, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
