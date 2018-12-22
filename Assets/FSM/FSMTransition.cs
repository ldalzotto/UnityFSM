using UnityEngine;

namespace FromChallenge
{
    public abstract class FSMTransition : MonoBehaviour
    {
        public FSMState StateToMove;
        public abstract bool ComputeTransition();
    }
}