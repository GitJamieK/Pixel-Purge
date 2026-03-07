using UnityEngine;

namespace Game {
    public class Game : ActionStack.ActionStack.ActionBehavior {

        private void Start() {
            ActionStack.ActionStack.Main.PushAction(this);
        }


        public override bool IsDone() {
            return false;
        }
    }
}