using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn = null;
    [SerializeField] private Vector2 spawnRange = Vector3.zero;

    private void Start()
    {
        Instantiate(
            objectToSpawn,
            GenerateSpawnPosition(),
            objectToSpawn.transform.rotation
        );
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange.x, spawnRange.x);
        float spawnPosZ = Random.Range(-spawnRange.y, spawnRange.y);

        // Set the X and Z to the random values we calculated. Keep the Y the
        // same, as what is set in the prefab. Ensure the prefab's Y value is
        // set such that it is right above the game floor.
        return new Vector3(
            spawnPosX,
            objectToSpawn.transform.position.y,
            spawnPosZ
        );
    }

}
