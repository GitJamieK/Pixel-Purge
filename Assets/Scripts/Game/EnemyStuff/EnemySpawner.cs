using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public Transform player;

    public float spawnCooldown = 2f;
    public float spawnRadius = 10f;

    float spawnTimer;

    void Update() {

        // timer
        spawnTimer -= Time.deltaTime;

        // spawn time
        if (spawnTimer <= 0f) {

            // spawn enemy
            SpawnEnemy();

            // reset timer
            spawnTimer = spawnCooldown;
        }
    }

    void SpawnEnemy() {

        // random dir
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // spawn pos
        Vector3 spawnPosition = player.position + (Vector3)(randomDirection * spawnRadius);

        // enemy
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
