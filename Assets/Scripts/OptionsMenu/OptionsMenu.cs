using ActionStack;
using UnityEngine;

namespace Game {

    public class OptionsMenu : ActionStack.ActionStack.ActionBehavior {
        private bool m_bIsDone;

        #region Properties

        #endregion

        public override bool IsDone() {
            return m_bIsDone;
        }


        public void OnBack() {
            Debug.Log("Discard User Changes");
            m_bIsDone = true;
        }

        public void OnApply() {
            Debug.Log("Apply User Changes");
            m_bIsDone = true;
        }

        public override void OnEnd() {
            base.OnEnd();
            Destroy(gameObject);
        }

        public static OptionsMenu Create(Transform parent) {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/OptionsMenu/OptionsMenu");

            //saftey check
            if (prefab == null) {
                Debug.LogError("Could not load OptionsMenu prefab.");
                return null;
            }

            GameObject go = Instantiate(prefab, parent);
            return go.GetComponent<OptionsMenu>();
        }
    }
}