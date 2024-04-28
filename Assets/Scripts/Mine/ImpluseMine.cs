using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ImpulseMine : MonoBehaviour
{
    [SerializeField] private float explosionForce;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] Rigidbody playerRB;

    [SerializeField] float shotForce; 
    [SerializeField] float arcHeight; 
    [SerializeField] float gravity; 

    [SerializeField] float radius;

    bool movementActive;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        movementActive = true;
        spawnPoint = GameObject.FindGameObjectWithTag("MineSpawn");
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        Shoot();
    }

    void Shoot()
    {
        float projectileVelo = Mathf.Sqrt(-2 * gravity * arcHeight);

        Vector3 playerHoriVelo = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z);

        Vector3 relativeVelocity = playerHoriVelo + spawnPoint.transform.forward * shotForce;

        Vector3 velocity = relativeVelocity + spawnPoint.transform.up * projectileVelo;

        rb.velocity = velocity;

        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    private void Update()
    {
        if (transform.position.y <= initialPosition.y)
        {
            rb.useGravity = true;
        }

        if (!movementActive)
        {
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
        }
    }

    IEnumerator StartImplusion()
    {

        yield return new WaitForSeconds(1f);

        GetComponent<Renderer>().material.color = Color.red;

        yield return new WaitForSeconds(0.3f);

        RaycastHit[] playerScanned = Physics.SphereCastAll(transform.position, radius, Vector3.up);
        if (playerScanned.Length != 0)
        {
            for (int i = 0; i < playerScanned.Length; i++)
            {
                RaycastHit hit;

                hit = playerScanned[i];

                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    // Apply explosion force to the player
                    Rigidbody playerRB = hit.transform.gameObject.GetComponent<Rigidbody>();

                    Vector3 direction = hit.transform.position - transform.position;
                    playerRB.AddForce(direction.normalized * explosionForce, ForceMode.Impulse);
                }
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            movementActive = false;
            StartCoroutine(StartImplusion());
        }
    }
}