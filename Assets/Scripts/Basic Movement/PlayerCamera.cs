using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [SerializeField] Transform cameraPos;
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;
    [SerializeField] Rigidbody rb;

    [SerializeField] private float rotSpeed;

    // Update is called once per frame
    void Update()
    {
        if (cameraPos != null)
        {
            transform.position = cameraPos.position;
        }

          if (cameraPos == null)
          {            
                    Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
                    orientation.forward = viewDir.normalized;

                    float moveX = Input.GetAxisRaw("Horizontal");
                    float moveY = Input.GetAxisRaw("Vertical");
                    Vector3 inputDir = orientation.forward * moveY + orientation.right * moveX;

                    if (inputDir != Vector3.zero)
                    {
                        playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotSpeed);
                    }                   
          }
    }
}
