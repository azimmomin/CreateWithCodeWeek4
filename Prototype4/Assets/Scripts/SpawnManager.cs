using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn = null;
    [SerializeField] private GameObject powerUpToSpawn = null;
    [SerializeField] private Vector2 spawnRange = Vector3.zero;
    // This specifies how many waves it will take to spawn 1 power up.
    [SerializeField] private int powerUpSpawnRatio = 3;

    private int waveNumber = 1;
    private int activeEnemies = 0;

    private void Awake()
    {
        EnemyController.OnEnemyDied += OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        activeEnemies--;
        if (activeEnemies <= 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerUp(waveNumber);
        }
    }

    private void SpawnEnemyWave(int waveNumber)
    {
        for (int i = 0; i < waveNumber; i++)
        {
            Instantiate(
                enemyToSpawn,
                GenerateSpawnPosition(),
                enemyToSpawn.transform.rotation
            );
            activeEnemies++;
        }
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
            enemyToSpawn.transform.position.y,
            spawnPosZ
        );
    }

    private void SpawnPowerUp(int waveNumber)
    {
        if (waveNumber == 1 || waveNumber % powerUpSpawnRatio == 0)
            Instantiate(powerUpToSpawn, GenerateSpawnPosition(), powerUpToSpawn.transform.rotation);
    }

    private void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerUp(waveNumber);
    }

    private void OnDestroy()
    {
        EnemyController.OnEnemyDied -= OnEnemyDied;
    }
}
