using UnityEngine;
using System.Collections;

namespace FromChallenge
{
    public class FSMEventsManagerBehaviorProxy : MonoBehaviour
    {
        public static FSMEventsManagerBehaviorProxy Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }
    }

}
