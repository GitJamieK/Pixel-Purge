using UnityEngine;
using TMPro;

public class PlayerAbilities : MonoBehaviour {

    public Player player;

    public KeyCode abilityKey = KeyCode.Q;

    public float abilityDuration = 10f;
    public float abilityCooldown = 40f;
    public float shootSpeedMultiplier = 0.5f;

    public TextMeshProUGUI durationText;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI keyText;

    float durationTimer = 0f;
    float cooldownTimer = 0f;

    bool abilityActive = false;
    bool abilityOnCooldown = false;

    void Start() {

        //player
        if (player == null) {
            player = GetComponent<Player>();
        }

        // key label
        if (keyText != null) {
            keyText.text = "Q";
        }

        // clear texts
        if (durationText != null) {
            durationText.text = "";
        }

        if (cooldownText != null) {
            cooldownText.text = "";
        }
    }

    void Update() {

        // use ability
        if (Input.GetKeyDown(abilityKey) && CanUseAbility()) {
            ActivateAbility();
        }

        // active timer
        if (abilityActive) {
            durationTimer -= Time.deltaTime;

            // show duration
            if (durationText != null) {
                durationText.text = Mathf.CeilToInt(durationTimer).ToString();
            }

            // active done
            if (durationTimer <= 0f) {
                EndAbility();
                StartCooldown();
            }
        }
        else {

            // clear duration
            if (durationText != null) {
                durationText.text = "";
            }
        }

        // cooldown timer
        if (abilityOnCooldown) {
            cooldownTimer -= Time.deltaTime;

            // show cooldown
            if (cooldownText != null) {
                cooldownText.text = Mathf.CeilToInt(cooldownTimer).ToString();
            }

            // cooldown done
            if (cooldownTimer <= 0f) {
                EndCooldown();
            }
        }
        else {

            // clear cooldown
            if (cooldownText != null) {
                cooldownText.text = "";
            }
        }
    }

    bool CanUseAbility() {

        // ready check
        return !abilityActive && !abilityOnCooldown;
    }

    void ActivateAbility() {

        // safety check
        if (player == null) {
            return;
        }

        // start active
        abilityActive = true;
        durationTimer = abilityDuration;

        // faster shots
        player.shootCooldownMultiplier = shootSpeedMultiplier;
    }

    void EndAbility() {

        // stop active
        abilityActive = false;
        durationTimer = 0f;

        // reset speed
        if (player != null) {
            player.shootCooldownMultiplier = 1f;
        }

        // clear text
        if (durationText != null) {
            durationText.text = "";
        }
    }

    void StartCooldown() {

        // start cooldown
        abilityOnCooldown = true;
        cooldownTimer = abilityCooldown;
    }

    void EndCooldown() {

        // stop cooldown
        abilityOnCooldown = false;
        cooldownTimer = 0f;

        // clear text
        if (cooldownText != null) {
            cooldownText.text = "";
        }
    }

    public bool IsAbilityActive() {
        return abilityActive;
    }

    public bool IsAbilityOnCooldown() {
        return abilityOnCooldown;
    }

    public float GetRemainingDuration() {
        return durationTimer;
    }

    public float GetRemainingCooldown() {
        return cooldownTimer;
    }
}