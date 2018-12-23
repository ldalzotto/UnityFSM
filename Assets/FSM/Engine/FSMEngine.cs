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

        private List<FSM> FSMContainer = new List<FSM>();
        private bool IsFixedUpdateExecuted;

        public void AddFSM(FSM FSM)
        {
            FSMContainer.Add(FSM);
        }

        public void RemoveFSM(FSM FSM)
        {
            FSMContainer.Remove(FSM);
        }

        public void FixedUpdateAll()
        {
            IsFixedUpdateExecuted = true;
            foreach (var FSM in FSMContainer)
            {
                FSM.OnFixedUpdate();
            }
        }

        public void UpdateAll()
        {
            foreach (var FSM in FSMContainer)
            {
                FSM.OnUpdate(IsFixedUpdateExecuted);
            }
        }


        public void LateUpdateAll()
        {
            foreach (var FSM in FSMContainer)
            {
                FSM.OnLateUpdate(IsFixedUpdateExecuted);
            }

            IsFixedUpdateExecuted = false;
        }

    }
}
