
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 150f; // Adjust the mass as needed
        rb.centerOfMass = new Vector3(0, -0.5f, 0);  // Lower center of mass
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
