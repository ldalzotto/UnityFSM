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
            EnterActions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMEnterActions"), true, true, true, true, "On Enter Actions", 1f,
              (Rect rect, int index, bool isActive, bool isFocused) =>
              {
                  var element = EnterActions.serializedProperty.GetArrayElementAtIndex(index);
                  DrawFSMAction(rect, element);
              });

            UpdateActions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMUpdateActions"), true, true, true, true, "On Update Actions", 2f,
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = UpdateActions.serializedProperty.GetArrayElementAtIndex(index);
                DrawFSmActionWithConfiguration(rect, element);
            });

            FixedActions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMFixedActions"), true, true, true, true, "On Fixed Update Actions", 2f,
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = FixedActions.serializedProperty.GetArrayElementAtIndex(index);
                DrawFSmActionWithConfiguration(rect, element);
            });

            LateUpdateActions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMLateUpdateActions"), true, true, true, true, "On Late Update Actions", 2f,
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = LateUpdateActions.serializedProperty.GetArrayElementAtIndex(index);
                DrawFSmActionWithConfiguration(rect, element);
            });

            ExitActions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMExitActions"), true, true, true, true, "On Exit Actions", 2f,
              (Rect rect, int index, bool isActive, bool isFocused) =>
              {
                  var element = ExitActions.serializedProperty.GetArrayElementAtIndex(index);
                  DrawFSmActionWithConfiguration(rect, element);
              });

            Transitions = new FoldableReordableList(serializedObject, serializedObject.FindProperty("FSMTransitions"), true, true, true, true, "State Transitions", 1f,
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = Transitions.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, element, GUIContent.none);
            });
        }

        private static void DrawFSMAction(Rect rect, SerializedProperty element)
        {
            EditorGUI.PropertyField(rect, element, GUIContent.none);
        }

        private static void DrawFSmActionWithConfiguration(Rect rect, SerializedProperty element)
        {
            EditorGUI.PropertyField(new Rect(rect.position, new Vector2(rect.width, (rect.height / 2) - 2)), element.FindPropertyRelative("FSMAction"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.position + new Vector2(0, rect.height / 2), new Vector2(rect.width, rect.height / 2)), element.FindPropertyRelative("ComputeTransitionConditions"), new GUIContent("Compute transition conditions : ", "If this is true, then immediatly after selected action has been computed, all transitions in the 'State Transitions' field will be evaluated and may cause the FSM to switch FSMState. If FSMState is switched, then all following actions are skipped."));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var FSMStateTarget = (FSMState)target;

            FSMStateTarget.UpdateTheSameFrameOfEnter = EditorGUILayout.Toggle(new GUIContent("Call update at the same frame of enter.", "If this is true, when the FSM switch states then all actions inside 'On Update Actions' will be immediately executed after 'On Enter Actions'. And transitions too."), FSMStateTarget.UpdateTheSameFrameOfEnter);
            FSMStateTarget.FixedUpdateTheSameFrameOfEnter = EditorGUILayout.Toggle(new GUIContent("Call fixed update at the same frame of enter.", "If this is true, when the FSM switch states then all actions inside 'On Fixed Update Actions' will be immediately executed after 'On Enter Actions'. And transitions too. Actions will be executed only if physics engine has updated during the current frame, otherwise, Fixed Update actions have no reason to be executed."), FSMStateTarget.FixedUpdateTheSameFrameOfEnter);

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