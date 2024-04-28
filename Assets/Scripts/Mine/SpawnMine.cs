using Cinemachine;
using UnityEngine;

public class SpawnMine : MonoBehaviour
{

    [SerializeField] GameObject minePrefab;
    [SerializeField] Transform playerTransform;

    [SerializeField] Transform spawnPoint;
    public CinemachineFreeLook freeLookCamera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            InitMine();
    }

    void InitMine()
    {
      
        // Get the forward direction of the camera
        Vector3 cameraForward = freeLookCamera.transform.forward;

        // Calculate the spawn position based on the camera's position and its forward direction
        Vector3 spawnPosition = spawnPoint.transform.position + cameraForward;

        Instantiate(minePrefab, spawnPosition, Quaternion.identity);
    }
}
