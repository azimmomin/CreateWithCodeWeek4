using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static Action OnEnemyDied;

    [SerializeField] private float speed = 3f;

    private Rigidbody enemyRigidbody = null;
    private GameObject playerGameObject = null;

    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        Vector3 lookDirection = (playerGameObject.transform.position - transform.position).normalized;
        enemyRigidbody.AddForce(lookDirection * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bounds"))
        {
            OnEnemyDied?.Invoke();
            Destroy(gameObject);
        }
    }
}
