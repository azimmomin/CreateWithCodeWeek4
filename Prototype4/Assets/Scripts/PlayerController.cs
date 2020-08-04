using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject cameraFocalPoint = null;
    [SerializeField] private GameObject powerUpIndicator = null;
    [SerializeField] private float powerUpStrength = 15f;

    private Rigidbody playerRigidbody = null;

    private float verticalInput;
    private bool hasPowerUp = false;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
    }

    private void LateUpdate()
    {
        Vector3 indicatorPos = powerUpIndicator.transform.position;
        indicatorPos.x = transform.position.x;
        indicatorPos.z = transform.position.z;

        powerUpIndicator.transform.position = indicatorPos;
    }

    private void FixedUpdate()
    {
        playerRigidbody.AddForce(cameraFocalPoint.transform.forward * (speed * verticalInput));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            powerUpIndicator.SetActive(true);
            StartCoroutine(PowerUpCountdown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerUp)
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
                enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            }
        }
    }

    private IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(7f);
        powerUpIndicator.SetActive(false);
        hasPowerUp = false;
    }
}
