using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 1.5f;
    public int health = 1;
    public int damageToPlayer = 10;
    public int xpValue = 10;

    Transform player;

    void Start() {
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

    public void TakeDamage(int damageAmount) {

        // lose health
        health -= damageAmount;

        //deth check
        if (health <= 0) { Die(); }
    }

    void Die() {

        // player script
        Player playerScript = player.GetComponent<Player>();

        //xp
        playerScript.GainXp(xpValue);

        //destroy enemy
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {

        //hit player
        if (other.CompareTag("Player")) {

            // player script
            Player playerScript = other.GetComponent<Player>();

            //damage
            playerScript.TakeDamage(damageToPlayer);

            //destroy enemy
            Destroy(gameObject);
        }

        // hit bullet
        if (other.CompareTag("Bullet")) {

            // bullet script
            Bullet bulletScript = other.GetComponent<Bullet>();

            // damage
            TakeDamage(bulletScript.damage);

            //destroy bullet
            Destroy(other.gameObject);
        }
    }
}
