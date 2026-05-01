using UnityEngine;
using TMPro;

public class PlayerBarsUI : MonoBehaviour {

    public Player player;

    public RectTransform hpFillRect;
    public RectTransform xpFillRect;

    public TMP_Text hpText;
    public TMP_Text xpText;

    public float barSmoothSpeed = 10f;

    void Start() {

        //find player
        if (player == null) {
            player = FindFirstObjectByType<Player>();
        }

        //start bars
        SetBarsInstant();
    }

    void Update() {

        // if missing player
        if (player == null) {
            return;
        }

        // update bars
        UpdateBars();
    }

    void SetBarsInstant() {

        // hp safety
        if (player.maxHealth > 0) {

            // hp percent
            float hpPercent = (float)player.currentHealth / player.maxHealth;

            // set hp scale
            hpFillRect.localScale = new Vector3(Mathf.Clamp01(hpPercent), 1f, 1f);

            // set hp text
            hpText.text = player.currentHealth + " / " + player.maxHealth;
        }

        // xp saftey
        if (player.xpToNextLevel > 0) {

            // xp percent
            float xpPercent = (float)player.currentXp / player.xpToNextLevel;

            // set xp scale
            xpFillRect.localScale = new Vector3(Mathf.Clamp01(xpPercent), 1f, 1f);

            // set xp text
            xpText.text = player.currentXp + " / " + player.xpToNextLevel;
        }
    }

    void UpdateBars() {

        //hp saftey
        if (player.maxHealth > 0) {

            // hp percent
            float hpPercent = (float)player.currentHealth / player.maxHealth;

            // target hp
            float targetHpScale = Mathf.Clamp01(hpPercent);

            // smooth hp
            float newHpScale = Mathf.Lerp(hpFillRect.localScale.x, targetHpScale, Time.deltaTime * barSmoothSpeed);

            // set hp scale
            hpFillRect.localScale = new Vector3(newHpScale, 1f, 1f);

            // set hp text
            hpText.text = player.currentHealth + " / " + player.maxHealth;
        }

        // xp safety
        if (player.xpToNextLevel > 0) {

            // xp percent
            float xpPercent = (float)player.currentXp / player.xpToNextLevel;

            // target xp
            float targetXpScale = Mathf.Clamp01(xpPercent);

            // smooth xp
            float newXpScale = Mathf.Lerp(xpFillRect.localScale.x, targetXpScale, Time.deltaTime * barSmoothSpeed);

            // set xp scale
            xpFillRect.localScale = new Vector3(newXpScale, 1f, 1f);

            // set xp text
            xpText.text = player.currentXp + " / " + player.xpToNextLevel;
        }
    }
}
