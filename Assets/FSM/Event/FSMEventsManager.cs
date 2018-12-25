using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FromChallenge
{
    public class FSMEventsManager
    {

        private static FSMEventsManager instance = null;

        public static FSMEventsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FSMEventsManager();
                }
                return instance;
            }
        }

        public delegate void MessageDispatcherHandler(object FSMEventMessage);

        private Dictionary<string, MessageDispatcherHandler> MessageDispatcher = new Dictionary<string, MessageDispatcherHandler>();

        public void AddListen(FSMEventListener FSMEventListener)
        {
            if (!MessageDispatcher.ContainsKey(FSMEventListener.GetType().ToString()))
            {
                MessageDispatcher.Add(FSMEventListener.GetType().ToString(), null);
            }

            MessageDispatcher[FSMEventListener.GetType().ToString()] += FSMEventListener.ReceiveMessage;
        }

        public void RemoveListen(FSMEventListener FSMEventListener)
        {
            MessageDispatcher[FSMEventListener.GetType().ToString()] -= FSMEventListener.ReceiveMessage;
        }

        public void SendMessage(object FSMEventMessage, string ListenerType, GameObject gameObjectCaller, object caller)
        {
#if FSM_DEBUG
            FSMDebugHelper.FSMSendingMessage(gameObjectCaller, caller);
#endif
            var listenerEvent = MessageDispatcher[ListenerType];
            if (listenerEvent != null)
            {
                listenerEvent.Invoke(FSMEventMessage);
            }

        }

    }

}
