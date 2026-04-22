using UnityEngine;
using TMPro;

public class DebugStatsUI : MonoBehaviour {

    public Player player;
    public PlayerAbilities playerAbilities;
    public TextMeshProUGUI debugText;

    void Update() {
        // Ability
        if (playerAbilities == null && player != null) {
            playerAbilities = player.GetComponent<PlayerAbilities>();
        }

        string abilityInfo = "ABILITY: READY";

        // active info
        if (playerAbilities != null) {

            if (playerAbilities.IsAbilityActive()) {
                abilityInfo = "ABILITY ACTIVE: " + Mathf.CeilToInt(playerAbilities.GetRemainingDuration());
            }
            else if (playerAbilities.IsAbilityOnCooldown()) {
                abilityInfo = "ABILITY CD: " + Mathf.CeilToInt(playerAbilities.GetRemainingCooldown());
            }
        }

        // show stats
        debugText.text =
            "HEALTH: " + player.currentHealth + " / " + player.maxHealth + "\n" +
            "LEVEL: " + player.level + "\n" +
            "XP: " + player.currentXp + " / " + player.xpToNextLevel + "\n" +
            "BULLET DMG: " + player.bulletDamage + "\n" +
            "SHOOT CD: " + player.GetCurrentShootCooldown() + "\n" +
            abilityInfo;
    }
}