using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionStack {
    public class ActionStack : MonoBehaviour {

        public interface IAction {

            void OnBegin(bool bFirstTime); //flag to check if first time on begin or not

            void OnUpdate();

            void OnEnd();

            bool IsDone();

        }

        public abstract class Action : IAction {

            public virtual bool IsDone() {return true; }

            public virtual void OnBegin(bool bFirstTime) {}

            public virtual void OnEnd() {}

            public virtual void OnUpdate() {}


            public override string ToString() {
                return GetType().Name;
            }
        }

        public abstract class ActionBehavior : MonoBehaviour, IAction {

            public virtual bool IsDone() {return true; }

            public virtual void OnBegin(bool bFirstTime) {}

            public virtual void OnEnd() {}

            public virtual void OnUpdate() {}


            public override string ToString() {
                return GetType().Name;
            }
        }

        public abstract class ActionObject : ScriptableObject, IAction {

            public virtual bool IsDone() {return true; }

            public virtual void OnBegin(bool bFirstTime) {}

            public virtual void OnEnd() {}

            public virtual void OnUpdate() {}


            public override string ToString() {
                return GetType().Name;
            }
        }

        private List<IAction>           m_actionStack = new List<IAction>();
        private HashSet<IAction>        m_firstTimeActions = new HashSet<IAction>();
        private IAction                 m_currentAction;

        #region Properties

        public List<IAction> Stack => m_actionStack;                                        /* see whats on the stack */
        public IAction CurrentAction => m_currentAction;                                    /* see what is current action */
        public bool IsEmpty => m_currentAction == null && m_actionStack.Count == 0;         /* see if the stack is empty, is something running on the stack? */

        #endregion

        public void PushAction(IAction action) {                                            /* push on the stack */
            if (action != null) {                                                           /* check so action is not null */
                
                m_actionStack.RemoveAll(a => a == action);                                  /* is the action already on the stack? */
                m_actionStack.Insert(0, action);                                            /* add action to top of stack */

                if (m_currentAction != null &&                                              /* reset the current action */
                    m_currentAction != action) {                                            /* current action is not = to the just pushed action */
                    m_currentAction = null;                                                 /* then, set current action to null */
                }
            }
        }

        protected virtual void Update() {                                                  /* Unity's Update */
            UpdateActions();                                                               /* call UpdateActions every frame */
        }

        protected virtual void UpdateActions() {
            if (IsEmpty) {return; }                                                        /* do we have actions? */

            while (m_currentAction == null &&                                              /* dont have a current action selected yet*/
                   m_actionStack.Count > 0) {                                                    /* but we do have an action in it */
                m_currentAction = m_actionStack[0];                                        /* grab first action from stack and set it as m_currentAction */

                bool bFirstTime = !m_firstTimeActions.Contains(m_currentAction);           /* checking hashset if it contains the actions, if possitive means not first action */
                m_firstTimeActions.Add(m_currentAction);                                   /* add to it the hashset */
                m_currentAction.OnBegin(bFirstTime);                                       /* call OnBegin */

                if (m_currentAction != null) {                                             /* did OnBegin push or remove another action? */
                    if (m_actionStack.Count > 0 &&                                          
                        m_currentAction != m_actionStack[0]){                              /* current action is not = to first item */
                        m_currentAction = null;                                            /* then, set current action to null */
                        UpdateActions();                                                   /* recursive call back to UpdateActions */
                        return;
                    }
                }
            }
            

            if (m_currentAction != null) {                                                     /* call OnUpdate */
                m_currentAction.OnUpdate();                                                    /* update action */

                if (m_actionStack.Count > 0 &&                                                 /* is action still current action? */                                       
                    m_currentAction == m_actionStack[0]){                                      /* current action is == to first item */

                    if (m_currentAction.IsDone()) {                                            /* is action done? */
                        m_actionStack.RemoveAt(0);                                             /* remove from stack */
                        m_currentAction.OnEnd();                                               /* current action cleanup */
                        m_firstTimeActions.Remove(m_currentAction);                            /* remove action from firstTimeActions */
                        m_currentAction = null;
                    }
                }
                else {                                                                         /* if not the current action */
                    m_currentAction = null;                                                    /* reset action to null */
                }
            }
        }
    }   
}