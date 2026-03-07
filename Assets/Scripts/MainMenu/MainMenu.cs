using ActionStack;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {

    public class MainMenu : ActionStack.ActionStack.ActionBehavior {

        private void Start() {
            ActionStack.ActionStack.Main.PushAction(this);
        }


        public void OnNewGame() {
            SceneManager.LoadScene("Game");
        }

        public void OnOptions() {
            //create and push OptionsMenu on main stack
            Canvas canvas = GetComponentInChildren<Canvas>();
            OptionsMenu om = OptionsMenu.Create(canvas.transform);
            ActionStack.ActionStack.Main.PushAction(om);

        }

        public void OnQuit() {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }




        public override bool IsDone() {
            return false;
        }
    }
}