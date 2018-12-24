using System;
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

        public void OnEnter()
        {
            ProcessActionArray(FSMEnterActions);
        }

        public void OnUpdate()
        {
            ProcessActionWithConfigurationArray(FSMUpdateActions);
        }

        public void OnFixedUpdate()
        {
            ProcessActionWithConfigurationArray(FSMFixedActions);
        }

        public void OnLateUpdate()
        {
            ProcessActionWithConfigurationArray(FSMLateUpdateActions);
        }

        public FSMTransition OnTransition()
        {
            return ProcessTransitions();
        }

        public void OnExit()
        {
            if (FSMExitActions != null)
            {
                foreach (var FSMAction in FSMExitActions)
                {
                    try
                    {
                        FSMAction.FSMAction.ExecuteAction();
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e, this);
                        throw FSMActionProcessingError.FromDetailedExecutionInformation(FSMAction.GetType().ToString(), e);
                    }

                }
            }
        }

        private void ProcessActionArray(FSMAction[] FSMActions)
        {
            foreach (var FSMAction in FSMActions)
            {
                try
                {
                    FSMAction.ExecuteAction();
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                    throw FSMActionProcessingError.FromDetailedExecutionInformation(FSMAction.GetType().ToString(), e);
                }

            }
        }

        private void ProcessActionWithConfigurationArray(FSMActionWithConfiguration[] FSMActions)
        {
            foreach (var FSMAction in FSMActions)
            {
                try
                {
                    FSMAction.FSMAction.ExecuteAction();
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                    throw FSMActionProcessingError.FromDetailedExecutionInformation(FSMAction.GetType().ToString(), e);
                }
            }
        }

        private FSMTransition ProcessTransitions()
        {
            foreach (var FSMTransition in FSMTransitions)
            {
                try
                {
                    if (FSMTransition.ComputeTransition())
                    {
                        return FSMTransition;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                    throw FSMTransitionProcessingError.FromDetailedExecutionInformation(FSMTransition.GetType().ToString(), e);
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
