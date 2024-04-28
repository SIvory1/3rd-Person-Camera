using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            rb.AddForce(-transform.forward * 1, ForceMode.Force);

        }

    }
}
