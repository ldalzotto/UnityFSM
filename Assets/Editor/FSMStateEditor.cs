using UnityEditor;
using UnityEngine;

namespace FromChallenge
{
    [CustomEditor(typeof(FSMState), true)]
    public class FSMStateEditor : Editor
    {

        private FoldableReordableList EnterActions;
        private FoldableReordableList UpdateActions;
        private FoldableReordableList FixedActions;
        private FoldableReordableList LateUpdateActions;
        private FoldableReordableList ExitActions;

        private FoldableReordableList Transitions;

        private void OnEnable()
        {
            EnterActions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMEnterActions"), true, true, true, true, "On Enter Actions",
              (Rect rect, int index, bool isActive, bool isFocused) =>
              {
                  var element = EnterActions.serializedProperty.GetArrayElementAtIndex(index);
                  DrawFSmAction(rect, element);
              });

            UpdateActions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMUpdateActions"), true, true, true, true, "On Update Actions",
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = UpdateActions.serializedProperty.GetArrayElementAtIndex(index);
                DrawFSmAction(rect, element);
            });

            FixedActions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMFixedActions"), true, true, true, true, "On Fixed Update Actions",
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = FixedActions.serializedProperty.GetArrayElementAtIndex(index);
                DrawFSmAction(rect, element);
            });

            LateUpdateActions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMLateUpdateActions"), true, true, true, true, "On Late Update Actions",
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = LateUpdateActions.serializedProperty.GetArrayElementAtIndex(index);
                DrawFSmAction(rect, element);
            });

            ExitActions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMExitActions"), true, true, true, true, "On Exit Actions",
              (Rect rect, int index, bool isActive, bool isFocused) =>
              {
                  var element = ExitActions.serializedProperty.GetArrayElementAtIndex(index);
                  DrawFSmAction(rect, element);
              });

            Transitions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMTransitions"), true, true, true, true, "State Transitions",
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = Transitions.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, element, GUIContent.none);
            });
        }

        private static void DrawFSmAction(Rect rect, SerializedProperty element)
        {
            EditorGUI.PropertyField(new Rect(rect.position, new Vector2(rect.width * 2 / 3, rect.height)), element.FindPropertyRelative("FSMAction"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(new Vector2(rect.position.x + rect.width * 2 / 3, rect.position.y), new Vector2(rect.width * 1 / 3, rect.height)), element.FindPropertyRelative("ComputeTransitionConditions"), GUIContent.none);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var FSMStateTarget = (FSMState)target;

            FSMStateTarget.UpdateTheSameFrameOfEnter = EditorGUILayout.Toggle("Call update at the same frame of enter.", FSMStateTarget.UpdateTheSameFrameOfEnter);

            EditorGUILayout.Separator(); EditorGUILayout.Separator();
            EnterActions.DoLayoutList();
            EditorGUILayout.Separator(); EditorGUILayout.Separator();
            UpdateActions.DoLayoutList();
            EditorGUILayout.Separator(); EditorGUILayout.Separator();
            FixedActions.DoLayoutList();
            EditorGUILayout.Separator(); EditorGUILayout.Separator();
            LateUpdateActions.DoLayoutList();
            EditorGUILayout.Separator(); EditorGUILayout.Separator();
            ExitActions.DoLayoutList();
            EditorGUILayout.Separator(); EditorGUILayout.Separator();
            Transitions.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}