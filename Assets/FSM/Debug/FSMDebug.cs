using UnityEngine;
using UnityEditor;
using System.IO;

namespace FromChallenge
{
    public class FSMDebug
    {
        private static FSMDebug instance = null;

        public static FSMDebug Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FSMDebug();

                }
                return instance;
            }
        }

        public FSMDebugConfiguration FSMDebugConfiguration { get => _fSMDebugConfiguration; }

        private FSMDebug() { }

        private FSMDebugConfiguration _fSMDebugConfiguration;
        private string _absoluteLogPath;
        private StreamWriter _streamWriter;

        public void Initialize(FSMDebugConfiguration FSMDebugConfiguration)
        {
            _fSMDebugConfiguration = FSMDebugConfiguration;
            _absoluteLogPath = Application.dataPath.Replace("/Assets", "") + FSMDebugConfiguration.RelativeLogPath;
            CreateFileIfNotExists();
            _streamWriter = new StreamWriter(_absoluteLogPath, true);
        }

        private void CreateFileIfNotExists()
        {
            if (File.Exists(_absoluteLogPath))
            {
                var fs = File.Create(_absoluteLogPath);
                fs.Dispose();
            }
        }

        public void CloseWriteStream()
        {
            _streamWriter.Close();
        }

        public void WriteLine(string line)
        {
            _streamWriter.WriteLine(line);
        }

    }
}
