using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public Transform player;

    public float spawnCooldown = 2f;
    public float spawnRadius = 10f;

    float baseSpawnCooldown;
    float spawnTimer;

    static float spawnCooldownMultiplier = 1f;

    void Awake() {

        // base cooldown
        baseSpawnCooldown = spawnCooldown;
    }

    void Update() {

        // timer
        spawnTimer -= Time.deltaTime;

        // spawn time
        if (spawnTimer <= 0f) {

            // spawn enemy
            SpawnEnemy();

            // reset timer
            spawnTimer = GetCurrentSpawnCooldown();
        }
    }

    float GetCurrentSpawnCooldown() {

        // final cooldown
        return baseSpawnCooldown * spawnCooldownMultiplier;
    }

    void SpawnEnemy() {
        // safety check
        if (player == null) {
            return;
        }

        // random dir
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // spawn pos
        Vector3 spawnPosition = player.position + (Vector3)(randomDirection * spawnRadius);

        // enemy
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public static void LevelUpSpawner() {
        // faster spawning
        spawnCooldownMultiplier *= 0.9f;

        Debug.Log("Enemy spawning got faster");
    }
}
