using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "DebugConfiguration", menuName = "FSM/Debug/DebugConfiguration", order = 1)]
public class FSMDebugConfiguration : ScriptableObject
{
    public bool WriteInFile;
    public string RelativeLogPath;
    public bool WriteInConsole;
    public bool DisplayFrameCount;
}