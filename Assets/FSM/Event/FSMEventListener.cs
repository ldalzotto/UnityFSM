using UnityEngine;
using System.Collections;

namespace FromChallenge
{
    public abstract class FSMEventListener : MonoBehaviour
    {

        public abstract void ReceiveMessage(object FSMEventMessage);

        private void OnEnable()
        {
            FSMEventsManager.Instance.AddListen(this);
        }

        private void OnDisable()
        {
            FSMEventsManager.Instance.RemoveListen(this);
        }
    }

}
