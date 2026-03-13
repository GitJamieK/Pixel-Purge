using UnityEngine;
using TMPro;

public class DebugStatsUI : MonoBehaviour {

    public Player player;
    public TextMeshProUGUI debugText;

    void Update() {

        // show stats
        debugText.text =
            "HEALTH: " + player.currentHealth + " / " + player.maxHealth + "\n" +
            "LEVEL: " + player.level + "\n" +
            "XP: " + player.currentXp + " / " + player.xpToNextLevel + "\n" +
            "BULLET DMG: " + player.bulletDamage + "\n" +
            "SHOOT CD: " + player.shootCooldown;
    }
}
