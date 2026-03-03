using ActionStack;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {

    public class MainMenu : ActionStack.ActionStack.ActionBehavior {

        private void Start() {
            ActionStack.ActionStack.Main.PushAction(this);
        }

        public override bool IsDone() {
            return false;
        }
         





        public void Play() {
            SceneManager.LoadScene("Game");
        }
        public void Quit() {
            Application.Quit();
        }
    }
}