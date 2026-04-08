using ActionStack;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {
    public class DeathMenu : ActionStack.ActionStack.ActionBehavior {
        private bool m_bIsDone;

        public override bool IsDone() {
            return m_bIsDone;
        }

        public void OnRestart() {

            //unpause game
            Time.timeScale = 1f;

            //close
            m_bIsDone = true;

            //restart game
            SceneManager.LoadScene("Game");
        }

        public void OnQuit() {

            //unpause 
            Time.timeScale = 1f;

            //close
            m_bIsDone = true;

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public override void OnEnd() {
            base.OnEnd();

            //unpause
            Time.timeScale = 1f;

            //remove menu
            Destroy(gameObject);
        }

        public static DeathMenu Create(Transform parent) {
            
            GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/DeathMenu/DeathMenu");

            // safety check
            if (prefab == null) {
                Debug.LogError("Coild not load DeathMenu.");
                return null;
            }

            GameObject go = Instantiate(prefab, parent);

            //unpause
            Time.timeScale = 0f;

            return go.GetComponent<DeathMenu>();
        }
    }
}