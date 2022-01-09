using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Vector3 centerPosition;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        this.centerPosition = this.transform.localPosition;
        this.radius = this.transform.localScale.x/2;
        
    }

    // Update is called once per frame
    void Update()
    {
        this.centerPosition = this.transform.localPosition;
    }
}
