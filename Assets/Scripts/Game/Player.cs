using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject bulletPrefab;
    public float shootCooldown = 1.5f;

    float shootTimer;
    Vector2 aimDirection;

    void Update() {
        // aim update
        LookAtMouse();

        // timer
        shootTimer -= Time.deltaTime;

        // left click
        if (Input.GetMouseButton(0) && shootTimer <= 0f) {
            // shoot now
            Shoot();

            // reset timer
            shootTimer = shootCooldown;
        }
    }

    void LookAtMouse() {
        // mouse screen pos
        Vector3 mousePosition = Input.mousePosition;

        // camera distance
        mousePosition.z = 10f;

        // screen -> world
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // aim direction
        aimDirection = (worldMousePosition - transform.position).normalized;

        // direction angle
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // sprite offset
        angle -= 115f;

        //  rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot() {
        // bullet + forward offset
        GameObject newBullet = Instantiate(bulletPrefab, transform.position + (Vector3)(aimDirection * 0.6f), Quaternion.identity);

        // script
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        // direction
        bulletScript.direction = aimDirection;
    }
}