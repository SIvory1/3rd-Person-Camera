using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            collision.transform.position = new Vector3(0, 0, 0);
        }
    }
}
