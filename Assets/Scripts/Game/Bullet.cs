using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 5f;
    public float lifeTime = 20f;

    public Vector2 direction;

    void Start() {
        // face towards movement
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // rotate sprite
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // auto delete
        Destroy(gameObject, lifeTime);
    }

    void Update() {
        // move bullet
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}