using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MineLogic : MonoBehaviour
{

    bool onGround;
    // Update is called once per frame
    void Update()
    {
        if (!onGround)
        {
            Vector3 john = new Vector3(0, 0, Mathf.Cos(Time.time * 14) + Mathf.Sin(Time.time * 14));
            transform.Rotate(john, Time.deltaTime * 700, Space.Self);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            onGround = true;
            transform.up = Vector3.up;
        }
    }

}
