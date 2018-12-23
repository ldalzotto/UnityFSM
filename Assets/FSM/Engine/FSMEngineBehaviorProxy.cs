using UnityEngine;

namespace FromChallenge
{
    public class FSMEngineBehaviorProxy : MonoBehaviour
    {

#if FSM_DEBUG
        public FSMDebugConfiguration FSMDebugConfiguration;
#endif

        private void Awake()
        {

#if FSM_DEBUG
            InitializeLog();
#endif
        }

        private void Update()
        {
            FSMEngine.Instance.UpdateAll();
        }

        private void FixedUpdate()
        {
            FSMEngine.Instance.FixedUpdateAll();
        }

        private void LateUpdate()
        {
            FSMEngine.Instance.LateUpdateAll();
        }

        private void OnDestroy()
        {
#if FSM_DEBUG
            FSMDebug.Instance.CloseWriteStream();
#endif
        }

        private void InitializeLog()
        {
            FSMDebug.Instance.Initialize(FSMDebugConfiguration);
        }
    }
}
