using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(eventCalls))]

public class EeventCall : PropertyDrawer
{
    SerializedProperty callType;
    SerializedProperty DialogueKey;
    SerializedProperty timelineObj;
    SerializedProperty obj;

    private void OnEnable()
    {
        //callType = serializedObject.FindProperty("callType");

    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //
        callType = property.FindPropertyRelative("callType");
        DialogueKey = property.FindPropertyRelative("DialogueKey");
        timelineObj = property.FindPropertyRelative("timelineObj");
        obj = property.FindPropertyRelative("obj");


        EditorGUI.BeginProperty(position, label, property);
        Rect foldOutBox = new Rect(position.min.x, position.min.y,position.size.x,EditorGUIUtility.singleLineHeight);
    property.isExpanded =    EditorGUI.Foldout(foldOutBox,property.isExpanded,label);
        if (property.isExpanded)
        {
            DrawNameProperty(position);
        }
        switch ((InteractEvent.eventCallTypes)callType.intValue)
        {
            case InteractEvent.eventCallTypes.animCall:
                break;
            case InteractEvent.eventCallTypes.timelineCall:
                DrawTimelineObj(position);
                break;
            case InteractEvent.eventCallTypes.dialogueCall:
                DrawDialogueKeyProperty(position);

                break;
            default:
                break;
        }
        EditorGUI.EndProperty();

    }

    //Da mas espacio
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int totalLine = 1;
        //Mas grande si dropdown
        if (property.isExpanded)
        {
            totalLine += 4;
        }

      return totalLine * EditorGUIUtility.singleLineHeight;
    }

    private void DrawNameProperty(Rect position)
    {
        EditorGUIUtility.labelWidth = 60;
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight;
        float width = position.size.x * .8f;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, callType, new GUIContent(callType.name));
        
    }
    private void DrawDialogueKeyProperty(Rect position)
    {
        EditorGUIUtility.labelWidth = 100;

        Rect drawArea = new Rect(position.min.x ,
             position.min.y + 2* EditorGUIUtility.singleLineHeight,
             position.size.x * .8f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, DialogueKey, new GUIContent(DialogueKey.name));
    }
    private void DrawTimelineObj(Rect position)
    {
        EditorGUIUtility.labelWidth = 100;

        Rect drawArea = new Rect(position.min.x,
             position.min.y + 2 * EditorGUIUtility.singleLineHeight,
             position.size.x * .8f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, timelineObj, new GUIContent(timelineObj.name));

    }
}
