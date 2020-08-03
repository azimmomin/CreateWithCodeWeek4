using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 75f;
    [SerializeField] private bool useNaturalMotion = true;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float directionModifier = useNaturalMotion ? -1 : 1;
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime * directionModifier);
    }
}
