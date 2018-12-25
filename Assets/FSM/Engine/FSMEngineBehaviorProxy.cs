using UnityEngine;

namespace FromChallenge
{
    public class FSMEngineBehaviorProxy : MonoBehaviour
    {

#if FSM_DEBUG
        public FSMDebugConfiguration FSMDebugConfiguration;
#endif

        private static FSMEngineBehaviorProxy Instance = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
#if FSM_DEBUG
                InitializeLog();
#endif
            }
            else
            {
                Destroy(this);
            }
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

#if FSM_DEBUG
        private void OnApplicationQuit()
        {
            FSMDebug.Instance.CloseWriteStream();
        }
#endif

        private void InitializeLog()
        {
#if FSM_DEBUG
            FSMDebug.Instance.Initialize(FSMDebugConfiguration);
#endif
        }
    }
}
