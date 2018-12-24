using System.Collections.Generic;
using UnityEngine;

namespace FromChallenge
{

    public class FSMEngine
    {
        private static FSMEngine instance = null;

        public static FSMEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FSMEngine();

                }
                return instance;
            }
        }

        private FSMEngine() { }

        public void ClearReference()
        {
            instance = null;
        }

        private Dictionary<int, FSM> FSMContainer = new Dictionary<int, FSM>();

        private Dictionary<int, FSM> FSMUpdateElligible = new Dictionary<int, FSM>();
        private Dictionary<int, FSM> FSMFixedUpdateElligible = new Dictionary<int, FSM>();
        private Dictionary<int, FSM> FSMLateUpdateElligible = new Dictionary<int, FSM>();

        private bool IsFixedUpdateExecuted;

        public void AddFSM(FSM FSM)
        {
            FSMContainer.Add(FSM.GetInstanceID(), FSM);
            AddElligibleFSM(FSM);
        }

        public void AddElligibleFSM(FSM FSM)
        {
            if (FSM.CurrentFSMState.FSMUpdateActions.Length > 0)
            {
                FSMUpdateElligible.Add(FSM.GetInstanceID(), FSM);
            }
            if (FSM.CurrentFSMState.FSMFixedActions.Length > 0)
            {
                FSMFixedUpdateElligible.Add(FSM.GetInstanceID(), FSM);
            }
            if (FSM.CurrentFSMState.FSMLateUpdateActions.Length > 0)
            {
                FSMLateUpdateElligible.Add(FSM.GetInstanceID(), FSM);
            }
        }

        public void RemoveFSM(FSM FSM)
        {
            FSMContainer.Remove(FSM.GetInstanceID());
            RemoveElligibleFSM(FSM);
        }

        public void RemoveElligibleFSM(FSM FSM)
        {
            FSMUpdateElligible.Remove(FSM.GetInstanceID());
            FSMFixedUpdateElligible.Remove(FSM.GetInstanceID());
            FSMLateUpdateElligible.Remove(FSM.GetInstanceID());
        }

        public void FixedUpdateAll()
        {
            IsFixedUpdateExecuted = true;
            foreach (var FSM in FSMFixedUpdateElligible)
            {
                FSM.Value.OnFixedUpdate();
            }
        }

        public void UpdateAll()
        {
            foreach (var FSM in FSMUpdateElligible)
            {
                FSM.Value.OnUpdate(IsFixedUpdateExecuted);
            }
        }


        public void LateUpdateAll()
        {
            foreach (var FSM in FSMLateUpdateElligible)
            {
                FSM.Value.OnLateUpdate(IsFixedUpdateExecuted);
            }

            ProcessTransitions();

            IsFixedUpdateExecuted = false;
        }

        private void ProcessTransitions()
        {
            //process transitions
            foreach (var FSM in FSMContainer)
            {
                var triggeredTransition = FSM.Value.OnTransition();
                if (triggeredTransition != null)
                {
                    FSM.Value.ChangeState(triggeredTransition.StateToMove, IsFixedUpdateExecuted, true);
                }
            }
        }
    }
}
