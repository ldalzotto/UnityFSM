using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FromChallenge
{
    public class FoldableReordableList : ReorderableList
    {

        private bool displayed;

        public FoldableReordableList(SerializedObject sObject, SerializedProperty sProperty, bool draggable, bool displayHeader, bool displayAddButton, bool displayRemovebutton, string listTitle, float elementHeightFactor, ElementCallbackDelegate elementCallbackDelegate)
               : base(sObject, sProperty, draggable, displayHeader, displayAddButton, displayRemovebutton)
        {
            this.drawHeaderCallback = (Rect rect) =>
            {
                rect.x += 15;
                displayed = EditorGUI.Foldout(rect, displayed, listTitle, true);
            };
            this.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                if (displayed)
                {
                    rect.y += 2;
                    rect.height -= 5;
                    elementCallbackDelegate(rect, index, isActive, isFocused);
                }
            };
            elementHeightCallback = (int index) =>
            {
                if (displayed)
                {
                    return elementHeight * elementHeightFactor;
                }
                else
                {
                    return 0;
                }
            };

        }
    }

}
