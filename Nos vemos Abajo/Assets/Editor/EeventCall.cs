using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(eventCalls))]

public class EeventCall : PropertyDrawer
{
    SerializedProperty callType;
    SerializedProperty nameKey;
    SerializedProperty timelineObj;
    SerializedProperty obj;
    SerializedProperty animator;
    SerializedProperty interactObject;
    SerializedProperty newState;
    SerializedProperty Boolean;
    SerializedProperty PlayWithBefore;
    SerializedProperty Transform;
    private void OnEnable()
    {
        //callType = serializedObject.FindProperty("callType");

    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //
        callType = property.FindPropertyRelative("callType");
        nameKey = property.FindPropertyRelative("nameKey");
        timelineObj = property.FindPropertyRelative("timelineObj");
        obj = property.FindPropertyRelative("obj");
        Transform = property.FindPropertyRelative("Transform");
        PlayWithBefore = property.FindPropertyRelative("PlayWithBefore");

        Boolean = property.FindPropertyRelative("Boolean");
        animator = property.FindPropertyRelative("animator");
        newState = property.FindPropertyRelative("newState");
        interactObject = property.FindPropertyRelative("interactObject");

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
                DrawAnimProperty(position);
                DrawStringProperty(position, "Animation Key");
                DrawCallWithBefore(position, 4.8f);

                break;
            case InteractEvent.eventCallTypes.timelineCall:
                DrawTimelineObj(position);
                DrawCallWithBefore(position, 3.2f);

                break;
            case InteractEvent.eventCallTypes.dialogueCall:
                DrawStringProperty(position,"Dialogue Key");
                DrawCallWithBefore(position, 4.8f);

                break;
            case InteractEvent.eventCallTypes.changeObjectState:
                DrawNewState(position);
                DrawInteractObject(position);
                DrawCallWithBefore(position, 4.8f);

                break;
            case InteractEvent.eventCallTypes.activateObject:
                DrawGameObject(position,"Objeto");
                DrawCallWithBefore(position, 3.2f);

                break;
            case InteractEvent.eventCallTypes.soundState:
                DrawGameObject(position, "Objeto");
                DrawStringProperty(position, "Sound Key");
                DrawPosition(position);
                DrawCallWithBefore(position, 6);
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
            totalLine += 6;
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
    private void DrawAnimProperty(Rect position)
    {
        EditorGUIUtility.labelWidth = 100;

        Rect drawArea = new Rect(position.min.x,
             position.min.y + 2 * EditorGUIUtility.singleLineHeight,
             position.size.x * .8f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, animator, new GUIContent(animator.name));
    }
    private void DrawStringProperty(Rect position,string name)
    {
        EditorGUIUtility.labelWidth = 100;

        Rect drawArea = new Rect(position.min.x ,
             position.min.y + 3.2f* EditorGUIUtility.singleLineHeight,
             position.size.x * .8f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, nameKey, new GUIContent(name));
    }
    private void DrawCallWithBefore(Rect position,float xpos)
    {
        EditorGUIUtility.labelWidth = 100;

        Rect drawArea = new Rect(position.min.x,
             position.min.y + xpos * EditorGUIUtility.singleLineHeight,
             position.size.x * .8f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, PlayWithBefore, new GUIContent(PlayWithBefore.name));
    }
    private void DrawTimelineObj(Rect position)
    {
        EditorGUIUtility.labelWidth = 100;

        Rect drawArea = new Rect(position.min.x,
             position.min.y + 2 * EditorGUIUtility.singleLineHeight,
             position.size.x * .8f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, timelineObj, new GUIContent(timelineObj.name));

    }
    private void DrawBoolean(Rect position)
    {
        EditorGUIUtility.labelWidth = 100;

        Rect drawArea = new Rect(position.min.x,
             position.min.y + 3.2f * EditorGUIUtility.singleLineHeight,
             position.size.x * .8f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, Boolean, new GUIContent(Boolean.name));
    }
    private void DrawGameObject(Rect position,string name)
    {
        EditorGUIUtility.labelWidth = 100;

        Rect drawArea = new Rect(position.min.x,
             position.min.y + 2 * EditorGUIUtility.singleLineHeight,
             position.size.x * .8f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea,obj, new GUIContent(name));
    }
    private void DrawPosition(Rect position)
    {
        EditorGUIUtility.labelWidth = 100;

        Rect drawArea = new Rect(position.min.x,
             position.min.y + 4.4f * EditorGUIUtility.singleLineHeight,
             position.size.x * .8f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, Transform, new GUIContent(Transform.name));
    }
    private void DrawInteractObject(Rect position)
    {
        EditorGUIUtility.labelWidth = 100;

        Rect drawArea = new Rect(position.min.x,
             position.min.y + 2 * EditorGUIUtility.singleLineHeight,
             position.size.x * .8f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, interactObject, new GUIContent(interactObject.name));
    }
    private void DrawNewState(Rect position)
    {
        EditorGUIUtility.labelWidth = 100;

        Rect drawArea = new Rect(position.min.x,
             position.min.y + 3.2f * EditorGUIUtility.singleLineHeight,
             position.size.x * .8f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, newState, new GUIContent(newState.name));
    }
}

