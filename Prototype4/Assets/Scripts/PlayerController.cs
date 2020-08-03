using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject cameraFocalPoint = null;

    private Rigidbody playerRigidbody = null;
    private float verticalInput;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        playerRigidbody.AddForce(cameraFocalPoint.transform.forward * (speed * verticalInput));
    }
}
