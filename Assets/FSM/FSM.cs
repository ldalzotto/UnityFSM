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
            ChangeState(StartingFSMState);
        }

        public void OnUpdate()
        {
            ChangeState(CurrentFSMState.OnUpdate());
        }

        public void OnFixedUpdate()
        {
            CurrentFSMState.OnLateUpdate();
        }

        public void OnLateUpdate()
        {
            CurrentFSMState.OnLateUpdate();
        }

        private void OnDestroy()
        {
            FSMEngine.Instance.RemoveFSM(this);
        }


        private void ChangeState(FSMState newState)
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
            CurrentFSMState.OnEnter();

            if (CurrentFSMState.UpdateTheSameFrameOfEnter)
            {
                OnUpdate();
            }
        }
    }

}
