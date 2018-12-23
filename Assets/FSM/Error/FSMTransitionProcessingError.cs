using UnityEngine;
using UnityEditor;
using System;

namespace FromChallenge
{
    public class FSMTransitionProcessingError : Exception
    {
        public FSMTransitionProcessingError() : base() { }
        public FSMTransitionProcessingError(string message) : base(message) { }
        public FSMTransitionProcessingError(string message, Exception initialError) : base(message, initialError) { }


        public static FSMTransitionProcessingError FromDetailedExecutionInformation(string FSMTransitionName, Exception initialError)
        {
            return new FSMTransitionProcessingError("ERROR - The transition : " + FSMTransitionName + " has failed : " + initialError.Message, initialError);
        }
    }
}
