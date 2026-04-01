using ActionStack;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game {

    public class LevelUp : ActionStack.ActionStack.ActionBehavior {
        private bool m_bIsDone;

        //buttons
        public Button btn1;
        public Button btn2;
        public Button btn3;

        public TextMeshProUGUI txt1;
        public TextMeshProUGUI txt2;
        public TextMeshProUGUI txt3;

        Player player;

        enum UpgradesType {
            shootCooldown,
            maxHealth,
            bulletDamage,
            heal
        }

        UpgradesType[] currentUpgrades = new UpgradesType[3];

        void Start() {
            // Find player
            player = FindFirstObjectByType<Player>();

            // roll upgrades
            RollUpgrades();

            // assign buttons
            btn1.onClick.AddListener(() => OnUpgradeChosen(0));
            btn2.onClick.AddListener(() => OnUpgradeChosen(1));
            btn3.onClick.AddListener(() => OnUpgradeChosen(2));
        }

        void RollUpgrades() {

            for (int i = 0; i < 3; i++) {
                currentUpgrades[i] = (UpgradesType)Random.Range(0, 4);
            }

            // update text
            txt1.text = GetUpgradeText(currentUpgrades[0]);
            txt2.text = GetUpgradeText(currentUpgrades[1]);
            txt3.text = GetUpgradeText(currentUpgrades[2]);
        }

        string GetUpgradeText(UpgradesType type) {
            switch (type) {
                case UpgradesType.shootCooldown: return "Faster Shooting";
                case UpgradesType.maxHealth: return "+ Max health";
                case UpgradesType.bulletDamage: return "+ Bullet Damage";
                case UpgradesType.heal: return "Heal";
            }
            return "Upgrades";
        }

        public void OnUpgradeChosen(int index) {

            UpgradesType chosen = currentUpgrades[index];

            switch (chosen) {

                case UpgradesType.shootCooldown:
                    player.shootCooldown *= 0.8f; // faster
                    break;

                case UpgradesType.maxHealth:
                    player.maxHealth += 20;
                    player.currentHealth += 20;
                    break;
                
                case UpgradesType.bulletDamage:
                    player.bulletDamage += 1;
                    break;

                case UpgradesType.heal:
                    player.currentHealth += 30;
                    if (player.currentHealth > player.maxHealth)
                        player.currentHealth = player.maxHealth;
                    break;
            }

            Debug.Log("Chose upgrade: " + chosen);

            // close levelup menu
            m_bIsDone = true;
        }

        public override bool IsDone() {
            return m_bIsDone;
        }

        public override void OnEnd() {
            base.OnEnd();

            // resume game
            Time.timeScale = 1f;

            // remove menu
            Destroy(gameObject);
        }

        public static LevelUp Create(Transform parent) {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/LevelUpMenu/LevelUpMenu");

            // safety check
            if (prefab == null) {
                Debug.LogError("Could not load LevelUpMenu prefab.");
                return null;
            }

            GameObject go = Instantiate(prefab, parent);

            // pause game
            Time.timeScale = 0f;

            return go.GetComponent<LevelUp>();
        }
    }
}