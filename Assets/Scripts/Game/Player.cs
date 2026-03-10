using UnityEngine;

public class Player : MonoBehaviour {
    void Update() {
        // aim update
        LookAtMouse();
    }

    void LookAtMouse() {
        // mouse screen pos
        Vector3 mousePosition = Input.mousePosition;

        // camera distance
        mousePosition.z = 10f;

        // screen -> world
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // player -> mouse
        Vector2 direction = worldMousePosition - transform.position;

        // direction angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // sprite offset
        angle -= 115f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}