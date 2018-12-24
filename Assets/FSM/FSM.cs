using UnityEngine;

namespace FromChallenge
{
    public class FSM : MonoBehaviour
    {
        public FSMState StartingFSMState;

        public FSMState CurrentFSMState;

        private void OnEnable()
        {
#if FSM_DEBUG
            try
            {
#endif
                ChangeState(StartingFSMState, false, false);
                FSMEngine.Instance.AddFSM(this);
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
                CurrentFSMState.OnUpdate();
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
                CurrentFSMState.OnFixedUpdate();
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
                CurrentFSMState.OnLateUpdate();
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

        public FSMTransition OnTransition()
        {
            return CurrentFSMState.OnTransition();
        }

        private void OnDisable()
        {
            FSMEngine.Instance.RemoveFSM(this);
        }


        public void ChangeState(FSMState newState, bool IsFixedUpdateExecuted, bool updateElligible)
        {
            if (newState == CurrentFSMState)
            {
                return;
            }

            if (CurrentFSMState != null)
            {
                CurrentFSMState.OnExit();
                if (updateElligible)
                {
                    FSMEngine.Instance.RemoveElligibleFSM(this);
                }

            }
            CurrentFSMState = newState;

#if FSM_DEBUG
            FSMDebugHelper.EnterStateLog(this, newState);
#endif
            CurrentFSMState.OnEnter();
            if (updateElligible)
            {
                FSMEngine.Instance.AddElligibleFSM(this);
            }

            var transitionTriggered = CurrentFSMState.OnTransition();
            if (transitionTriggered != null)
            {
#if FSM_DEBUG
                FSMDebugHelper.FSMTransitionSuccessful(this, transitionTriggered, "Enter");
#endif
                ChangeState(transitionTriggered.StateToMove, IsFixedUpdateExecuted, true);
            }
            else
            {
                if (CurrentFSMState.UpdateTheSameFrameOfEnter)
                {
                    OnUpdate(IsFixedUpdateExecuted);
                    var updateTransitionTriggered = CurrentFSMState.OnTransition();
                    if (updateTransitionTriggered != null)
                    {
#if FSM_DEBUG
                        FSMDebugHelper.FSMTransitionSuccessful(this, updateTransitionTriggered, "Enter");
#endif
                        ChangeState(updateTransitionTriggered.StateToMove, IsFixedUpdateExecuted, true);
                    }
                }

                if (CurrentFSMState.FixedUpdateTheSameFrameOfEnter && IsFixedUpdateExecuted)
                {
                    OnFixedUpdate();
                    var fixedUpdateTransitionTriggered = CurrentFSMState.OnTransition();
                    if (fixedUpdateTransitionTriggered != null)
                    {
#if FSM_DEBUG
                        FSMDebugHelper.FSMTransitionSuccessful(this, fixedUpdateTransitionTriggered, "Enter");
#endif
                        ChangeState(fixedUpdateTransitionTriggered.StateToMove, IsFixedUpdateExecuted, true);
                    }
                }
            }
        }
    }

}
