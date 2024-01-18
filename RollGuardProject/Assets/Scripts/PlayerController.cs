using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    private GameObject focalPoint;
    public bool hasPowerup;
    private float powerupStenght = 15.0f;
    public GameObject powerupIndicator;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }
    void Update()
    {
        powerupIndicator.transform.position = transform.position + new Vector3(0,-0.5f, 0);
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward* forwardInput* Time.deltaTime* speed );
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        // Declare a new variable to get the rigidbody of enemy object
        Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        // Declare a new variable to get the direction away from the "player".
        Vector3 awayfromPlayer = (collision.gameObject.transform.position - transform.position);
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Debug.Log("Collided with " + collision.gameObject.name + "with powerup set to " + hasPowerup);
            // when player collides with "Enemy", Enemy object gets "AddForce" from the player angle
            enemyRigidbody.AddForce(awayfromPlayer * powerupStenght, ForceMode.Impulse);
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(6);
        powerupIndicator.SetActive(false);
        hasPowerup = false;
    }
}
