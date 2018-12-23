using UnityEngine;

namespace FromChallenge
{
    public class FSMState : MonoBehaviour
    {
        public bool UpdateTheSameFrameOfEnter;
        public bool FixedUpdateTheSameFrameOfEnter;

        public FSMActionWithConfiguration[] FSMEnterActions;
        public FSMActionWithConfiguration[] FSMUpdateActions;
        public FSMActionWithConfiguration[] FSMFixedActions;
        public FSMActionWithConfiguration[] FSMLateUpdateActions;
        public FSMActionWithConfiguration[] FSMExitActions;

        public FSMTransition[] FSMTransitions;

        public FSMState OnEnter()
        {
            return ProcessActionArrayWithTransitions(FSMEnterActions);
        }

        public FSMState OnUpdate()
        {
            return ProcessActionArrayWithTransitions(FSMUpdateActions);
        }

        public FSMState OnFixedUpdate()
        {
            return ProcessActionArrayWithTransitions(FSMFixedActions);
        }

        public FSMState OnLateUpdate()
        {
            return ProcessActionArrayWithTransitions(FSMLateUpdateActions);
        }

        public void OnExit()
        {
            //  Debug.Log("Exiting : " + gameObject.name);

            if (FSMExitActions != null)
            {
                foreach (var FSMAction in FSMExitActions)
                {
                    FSMAction.FSMAction.ExecuteAction();
                }
            }
        }

        private FSMState ProcessActionArrayWithTransitions(FSMActionWithConfiguration[] FSMActions)
        {
            if (FSMActions != null)
            {
                foreach (var FSMAction in FSMActions)
                {
                    FSMAction.FSMAction.ExecuteAction();
                    if (FSMAction.ComputeTransitionConditions)
                    {
                        var stateToMove = ProcessTransitions();
                        if (stateToMove != null)
                        {
                            return stateToMove;
                        }
                    }
                }

                if (FSMActions.Length > 0)
                {
                    return ProcessTransitions();
                }
            }

            return null;
        }


        private FSMState ProcessTransitions()
        {
            foreach (var FSMTransition in FSMTransitions)
            {
                if (FSMTransition.ComputeTransition())
                {
                    //    Debug.Log("Transition : " + FSMTransition.GetType().ToString() + " has responded positively. Switching to " + FSMTransition.StateToMove.gameObject.name);
                    return FSMTransition.StateToMove;
                }
            }
            return null;
        }

    }

    [System.Serializable]
    public class FSMActionWithConfiguration
    {
        public FSMAction FSMAction;
        public bool ComputeTransitionConditions;
    }

}
