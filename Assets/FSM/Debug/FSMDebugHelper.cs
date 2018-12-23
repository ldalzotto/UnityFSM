using UnityEngine;
using UnityEditor;
using System;

namespace FromChallenge
{
    public class FSMDebugHelper
    {
        public static void EnterStateLog(FSM FSM, FSMState NewState)
        {
            FormatAndWriteLine("Entering in state : " + NewState.name, FSM);
        }

        public static void ExitingStateLog(FSM FSM, FSMState ExitState)
        {
            FormatAndWriteLine("Exit state : " + ExitState.name, FSM);
        }

        private static void FormatAndWriteLine(string line, FSM FSM)
        {
            string lineToWrite = String.Format("{0:u}", DateTime.UtcNow) + " - ";

            if (FSMDebug.Instance.FSMDebugConfiguration.DisplayFrameCount)
            {
                lineToWrite += "F(" + Time.frameCount + ") - ";
            }

            lineToWrite += FSM.name + "/" + FSM.GetInstanceID() + " - " + line;
            if (FSMDebug.Instance.FSMDebugConfiguration.WriteInFile)
            {
                FSMDebug.Instance.WriteLine(lineToWrite);
            }
            if (FSMDebug.Instance.FSMDebugConfiguration.WriteInConsole)
            {
                Debug.Log(lineToWrite);
            }

        }
    }
}

