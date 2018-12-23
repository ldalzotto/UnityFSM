using UnityEngine;
using System.Collections;
using System;

namespace FromChallenge
{
    public class FSMActionProcessingError : Exception
    {
        public FSMActionProcessingError() : base() { }
        public FSMActionProcessingError(string message) : base(message) { }
        public FSMActionProcessingError(string message, Exception initialError) : base(message, initialError) { }


        public static FSMActionProcessingError FromDetailedExecutionInformation(string FSMActionName, Exception initialError)
        {
            return new FSMActionProcessingError("ERROR - The action : " + FSMActionName + " has failed : " + initialError.Message, initialError);
        }
    }

}
