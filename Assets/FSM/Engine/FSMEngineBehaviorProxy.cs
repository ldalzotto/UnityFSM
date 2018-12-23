using UnityEngine;

namespace FromChallenge
{
    public class FSMEngineBehaviorProxy : MonoBehaviour
    {
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
    }
}
