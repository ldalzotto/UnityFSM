using UnityEngine;

namespace FromChallenge
{
    public class FSM : MonoBehaviour
    {
        public FSMState StartingFSMState;

        private FSMState CurrentFSMState;

        private void Start()
        {
            FSMEngine.Instance.AddFSM(this);
#if FSM_DEBUG
            try
            {
#endif
            ChangeState(StartingFSMState, false);
#if FSM_DEBUG
            }
            catch (FSMActionProcessingError e)
            {

                FSMDebugHelper.FSMActionProcessError(this, e);
            }
            catch (FSMTransitionProcessingError e)
            {
                FSMDebugHelper.FSMTransitionProcessError(this, e);

            }
#endif
        }

        public void OnUpdate(bool IsFixedUpdateExecuted)
        {
#if FSM_DEBUG
            try
            {
#endif
            var transitionTriggered = CurrentFSMState.OnUpdate();
            if (transitionTriggered != null)
            {
                ChangeState(transitionTriggered.StateToMove, IsFixedUpdateExecuted);
#if FSM_DEBUG
                FSMDebugHelper.FSMTransitionSuccessful(this, transitionTriggered, "Update");
#endif
            }
#if FSM_DEBUG
        }
            catch (FSMActionProcessingError e)
            {
                FSMDebugHelper.FSMActionProcessError(this, e);
            }
            catch (FSMTransitionProcessingError e)
            {
                FSMDebugHelper.FSMTransitionProcessError(this, e);

            }
#endif
        }

        public void OnFixedUpdate()
        {
#if FSM_DEBUG
            try
            {
#endif
            var transitionTriggered = CurrentFSMState.OnFixedUpdate();
            if (transitionTriggered != null)
            {
#if FSM_DEBUG
                    FSMDebugHelper.FSMTransitionSuccessful(this, transitionTriggered, "FixedUpdate");
#endif
                ChangeState(transitionTriggered.StateToMove, true);
            }
#if FSM_DEBUG
        }
            catch (FSMActionProcessingError e)
            {

                FSMDebugHelper.FSMActionProcessError(this, e);
        }
            catch (FSMTransitionProcessingError e)
            {
                FSMDebugHelper.FSMTransitionProcessError(this, e);
            }
#endif
        }

        public void OnLateUpdate(bool IsFixedUpdateExecuted)
        {
#if FSM_DEBUG
            try
            {
#endif
            var transitionTriggered = CurrentFSMState.OnLateUpdate();
            if (transitionTriggered != null)
            {
#if FSM_DEBUG
                    FSMDebugHelper.FSMTransitionSuccessful(this, transitionTriggered, "LateUpdate");
#endif
                ChangeState(transitionTriggered.StateToMove, IsFixedUpdateExecuted);
            }
#if FSM_DEBUG
        }
            catch (FSMActionProcessingError e)
            {
                FSMDebugHelper.FSMActionProcessError(this, e);
        }
            catch (FSMTransitionProcessingError e)
            {
                FSMDebugHelper.FSMTransitionProcessError(this, e);
            }
#endif
        }

        private void OnDestroy()
        {
            FSMEngine.Instance.RemoveFSM(this);
        }


        private void ChangeState(FSMState newState, bool IsFixedUpdateExecuted)
        {
            if (newState == CurrentFSMState)
            {
                return;
            }

            if (CurrentFSMState != null)
            {
                CurrentFSMState.OnExit();
            }
            CurrentFSMState = newState;

#if FSM_DEBUG
            FSMDebugHelper.EnterStateLog(this, newState);
#endif
            var transitionTriggered = CurrentFSMState.OnEnter();
            if (transitionTriggered != null)
            {
#if FSM_DEBUG
                FSMDebugHelper.FSMTransitionSuccessful(this, transitionTriggered, "Enter");
#endif
                ChangeState(transitionTriggered.StateToMove, IsFixedUpdateExecuted);
            }
            else
            {
                if (CurrentFSMState.UpdateTheSameFrameOfEnter)
                {
                    OnUpdate(IsFixedUpdateExecuted);
                }

                if (CurrentFSMState.FixedUpdateTheSameFrameOfEnter && IsFixedUpdateExecuted)
                {
                    OnFixedUpdate();
                }
            }
        }
    }

}
