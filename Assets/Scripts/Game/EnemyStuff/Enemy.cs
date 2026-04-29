using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 1.5f;
    public int health = 1;
    public int damageToPlayer = 10;
    public int xpValue = 10;

    //base stats
    float baseSpeed;
    int baseHealth;
    int baseDamageToPlayer;

    // gloabval scaling
    static float speedMultiplier = 1f;
    static float healthMultiplier = 1f;
    static float damageMultiplier = 1f;

    // active enemies
    static List<Enemy> activeEnemies = new List<Enemy>();

    Transform player;

    void Awake() {
        // set base stats
        baseSpeed = speed;
        baseHealth = health;
        baseDamageToPlayer = damageToPlayer;
    }

    void OnEnable() {
        //add enemy
        activeEnemies.Add(this);
    }

    void OnDisable() {

        // remove enemy
        activeEnemies.Remove(this);
    }

    void Start() {
        //scaled stats
        ApplyCurrentScaling();

        // find player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");


        if (playerObject != null) {
            player = playerObject.transform;
        }
    }

    void Update() {

        // if player null
        if (player == null)
            return;

        // towards player
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;

        // move enemy
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void ApplyCurrentScaling() {
        //scale hp
        health = Mathf.CeilToInt(baseHealth * healthMultiplier);

        //sacle damage
        damageToPlayer = Mathf.CeilToInt(baseDamageToPlayer * damageMultiplier);

        // scale speed
        speed = baseSpeed * speedMultiplier;
    }

    public static void LevelUpEnemies() {
        // stronger enemies
        healthMultiplier *= 1.1f;
        damageMultiplier *= 1.1f;
        speedMultiplier *= 1.1f;

        // update alive enemies
        foreach (Enemy enemy in activeEnemies) {
            if (enemy != null) {
                enemy.ApplyCurrentScaling();
            }
        }

        Debug.Log("Enemies got stronger");
    }

    public void TakeDamage(int damageAmount) {

        // lose health
        health -= damageAmount;

        //deth check
        if (health <= 0) {
            Die();
        }
    }

    void Die() {

        // safety check
        if (player != null) {

            // player script
            Player playerScript = player.GetComponent<Player>();

            // give xp
            if (playerScript != null) {
                playerScript.GainXp(xpValue);
            }
        }

        //destroy enemy
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {

        //hit player
        if (other.CompareTag("Player")) {

            // player script
            Player playerScript = other.GetComponent<Player>();

            //damage player
            if (playerScript != null) {
                playerScript.TakeDamage(damageToPlayer);
            }

            //destroy enemy
            Destroy(gameObject);
        }

        // hit bullet
        if (other.CompareTag("Bullet")) {

            // bullet script
            Bullet bulletScript = other.GetComponent<Bullet>();

            // damage enemy
            if (bulletScript != null) {
                TakeDamage(bulletScript.damage);
            }

            //destroy bullet
            Destroy(other.gameObject);
        }
    }
}
