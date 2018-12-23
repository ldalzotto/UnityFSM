using UnityEngine;

namespace FromChallenge
{
    public class FSM : MonoBehaviour
    {
        public FSMState StartingFSMState;

        private FSMState CurrentFSMState;

        private void Awake()
        {
            FSMEngine.Instance.AddFSM(this);
            ChangeState(StartingFSMState, false);
        }

        public void OnUpdate(bool IsFixedUpdateExecuted)
        {
            ChangeState(CurrentFSMState.OnUpdate(), IsFixedUpdateExecuted);
        }

        public void OnFixedUpdate()
        {
            ChangeState(CurrentFSMState.OnFixedUpdate(), true);
        }

        public void OnLateUpdate(bool IsFixedUpdateExecuted)
        {
            ChangeState(CurrentFSMState.OnLateUpdate(), IsFixedUpdateExecuted);
        }

        private void OnDestroy()
        {
            FSMEngine.Instance.RemoveFSM(this);
        }


        private void ChangeState(FSMState newState, bool IsFixedUpdateExecuted)
        {
            if (newState == null || newState == CurrentFSMState)
            {
                return;
            }

            if (CurrentFSMState != null)
            {
                CurrentFSMState.OnExit();
            }
            CurrentFSMState = newState;

            var onEnterSwitchState = CurrentFSMState.OnEnter();
            if (onEnterSwitchState != null)
            {
                ChangeState(onEnterSwitchState, IsFixedUpdateExecuted);
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
