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

        public static void FSMTransitionSuccessful(FSM FSM, FSMTransition FSMTransition, string WorkflowCallname)
        {
            FormatAndWriteLine("The transition : " + FSMTransition.GetType().ToString() + " has responded positively when " + WorkflowCallname + " was called. Switching to state : " + FSMTransition.StateToMove.name, FSM);
        }

        public static void FSMActionProcessError(FSM FSM, FSMActionProcessingError FSMActionProcessingError)
        {
            FormatAndWriteLine(FSMActionProcessingError.Message, FSM);
            FSMDebug.Instance.Write(FSMActionProcessingError.InnerException.StackTrace + Environment.NewLine + FSMActionProcessingError.StackTrace + Environment.NewLine);
        }

        public static void FSMTransitionProcessError(FSM FSM, FSMTransitionProcessingError FSMTransitionProcessingError)
        {
            FormatAndWriteLine(FSMTransitionProcessingError.Message, FSM);
            FSMDebug.Instance.Write(FSMTransitionProcessingError.InnerException.StackTrace + Environment.NewLine + FSMTransitionProcessingError.StackTrace + Environment.NewLine);
        }

        public static void FSMSendingMessage(object sender)
        {
            string lineToWrite = Format();
            lineToWrite += (sender.GetType() + " has sended message.");
            if (FSMDebug.Instance.FSMDebugConfiguration.WriteInFile)
            {
                FSMDebug.Instance.WriteLine(lineToWrite);
            }
            if (FSMDebug.Instance.FSMDebugConfiguration.WriteInConsole)
            {
                Debug.Log(lineToWrite);
            }

        }

        private static void FormatAndWriteLine(string line, FSM FSM)
        {
            string lineToWrite = Format();

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

        private static string Format()
        {
            string lineToWrite = String.Format("{0:u}", DateTime.UtcNow) + " - ";

            if (FSMDebug.Instance.FSMDebugConfiguration.DisplayFrameCount)
            {
                lineToWrite += "F(" + Time.frameCount + ") - ";
            }

            return lineToWrite;
        }
    }
}

