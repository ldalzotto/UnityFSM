using UnityEngine;

namespace FromChallenge
{
    public class FSMState : MonoBehaviour
    {
        public bool UpdateTheSameFrameOfEnter;
        public bool FixedUpdateTheSameFrameOfEnter;

        public FSMAction[] FSMEnterActions;
        public FSMActionWithConfiguration[] FSMUpdateActions;
        public FSMActionWithConfiguration[] FSMFixedActions;
        public FSMActionWithConfiguration[] FSMLateUpdateActions;
        public FSMActionWithConfiguration[] FSMExitActions;

        public FSMTransition[] FSMTransitions;

        public FSMTransition OnEnter()
        {
            return ProcessActionArrayWithTransition(FSMEnterActions);
        }

        public FSMTransition OnUpdate()
        {
            return ProcessActionWithConfigurationArrayWithTransitions(FSMUpdateActions);
        }

        public FSMTransition OnFixedUpdate()
        {
            return ProcessActionWithConfigurationArrayWithTransitions(FSMFixedActions);
        }

        public FSMTransition OnLateUpdate()
        {
            return ProcessActionWithConfigurationArrayWithTransitions(FSMLateUpdateActions);
        }

        public void OnExit()
        {
            if (FSMExitActions != null)
            {
                foreach (var FSMAction in FSMExitActions)
                {
                    FSMAction.FSMAction.ExecuteAction();
                }
            }
        }

        private FSMTransition ProcessActionArrayWithTransition(FSMAction[] FSMActions)
        {
            if (FSMActions != null)
            {
                foreach (var FSMAction in FSMActions)
                {
                    FSMAction.ExecuteAction();
                }

                if (FSMActions.Length > 0)
                {
                    return ProcessTransitions();
                }
            }

            return null;
        }

        private FSMTransition ProcessActionWithConfigurationArrayWithTransitions(FSMActionWithConfiguration[] FSMActions)
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


        private FSMTransition ProcessTransitions()
        {
            foreach (var FSMTransition in FSMTransitions)
            {
                if (FSMTransition.ComputeTransition())
                {
                    return FSMTransition;
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
