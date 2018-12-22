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

        public void AddFSM(FSM FSM)
        {
            FSMContainer.Add(FSM);
        }

        public void RemoveFSM(FSM FSM)
        {
            FSMContainer.Remove(FSM);
        }

        public void UpdateAll()
        {
            foreach (var FSM in FSMContainer)
            {
                FSM.OnUpdate();
            }
        }

        public void FixedUpdateAll()
        {
            foreach (var FSM in FSMContainer)
            {
                FSM.OnFixedUpdate();
            }
        }

        public void LateUpdateAll()
        {
            foreach (var FSM in FSMContainer)
            {
                FSM.OnLateUpdate();
            }
        }

    }
}
