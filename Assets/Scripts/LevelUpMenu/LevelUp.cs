using ActionStack;
using UnityEngine;

namespace Game {

    public class LevelUp : ActionStack.ActionStack.ActionBehavior {
        private bool m_bIsDone;

        public override bool IsDone() {
            return m_bIsDone;
        }

        public void OnUpgradeChosen() {
            // give upgrade and close menu

            Debug.Log("given player upgrade");
            m_bIsDone = true;
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