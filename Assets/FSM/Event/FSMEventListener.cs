using UnityEngine;
using System.Collections;
using System;

namespace FromChallenge
{
    public abstract class FSMEventListener : MonoBehaviour
    {

        public void ReceiveMessage(object FSMEventMessage)
        {
            try
            {
                MessageHandling(FSMEventMessage);
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
#if FSM_DEBUG
                FSMDebugHelper.ReceivingMessageError(this, e);
#endif
            }

        }

        public abstract void MessageHandling(object FSMEventMessage);

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
