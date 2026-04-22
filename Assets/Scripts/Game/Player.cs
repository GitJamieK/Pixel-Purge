using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public GameObject bulletPrefab;
    public float shootCooldown = 1.5f;
    public float shootCooldownMultiplier = 1f;

    public int maxHealth = 100;
    public int currentHealth = 100;

    public int level = 1;
    public int currentXp = 0;
    public int xpToNextLevel = 100;

    public int bulletDamage = 1;

    float shootTimer;
    Vector2 aimDirection;

    void Update() {

        // aim update
        LookAtMouse();

        // timer
        shootTimer -= Time.deltaTime;

        // left click
        if (Input.GetMouseButton(0) && shootTimer <= 0f) {

            Shoot();

            // reset timer
            shootTimer = GetCurrentShootCooldown();
        }
    }

    public float GetCurrentShootCooldown() {

        // final cooldown
        return shootCooldown * shootCooldownMultiplier;
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

        // spawn pos
        Vector3 spawnPosition = transform.position + (Vector3)(aimDirection * 0.5f);

        // make bullet
        GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // script
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        // direction
        bulletScript.direction = aimDirection;

        // damage
        bulletScript.damage = bulletDamage;
    }

    public void TakeDamage(int damageAmount) {

        // lose health
        currentHealth -= damageAmount;

        // temp debug log
        Debug.Log("player took " + damageAmount + "damage");

        // clamp low
        if (currentHealth < 0) {
            currentHealth = 0;
        }

        // dead check
        if (currentHealth <= 0) {
            Die();
        }
    }

    public void GainXp(int xpAmount) {

        // add xp
        currentXp += xpAmount;

        //temp debug log
        Debug.Log("player gained " + xpAmount + "xp");

        // level loop
        while (currentXp >= xpToNextLevel) {
            // spend xp
            currentXp -= xpToNextLevel;

            // level up
            LevelUp();

            // stop extra levelups for now
            break;
        }
    }

    void LevelUp() {
        // next level
        level += 1;

        // heal
        currentHealth = maxHealth;

        // more xp needed (10%)
        xpToNextLevel = Mathf.CeilToInt(xpToNextLevel * 1.1f);

        // enemy level up
        Enemy.LevelUpEnemies();

        Debug.Log("Level Up! Level: " + level);

        // find canvas
        Canvas canvas = FindFirstObjectByType<Canvas>();

        // safety check
        if (canvas == null) {
            Debug.LogError("No Canvas found for LevelUpMenu.");
            return;
        }

        // create menu
        Game.LevelUp levelUpMenu = Game.LevelUp.Create(canvas.transform);

        // safety check
        if (levelUpMenu == null) {
            Debug.LogError("Could not create LevelUpMenu.");
            return;
        }

        // push action
        ActionStack.ActionStack.Main.PushAction(levelUpMenu);
    }

    void Die() {
        Debug.Log("Player died");

        //find canvas
        Canvas canvas = FindFirstObjectByType<Canvas>();

        // saftey check
        if (canvas == null) {
            Debug.LogError("No Canvas found for DeathMenu.");
            return;
        }

        // create
        Game.DeathMenu deathMenu = Game.DeathMenu.Create(canvas.transform);

        if (deathMenu == null) {
            Debug.LogError("Could not create DeathMenu.");
            return;
        }

        // push action
        ActionStack.ActionStack.Main.PushAction(deathMenu);
    }
}