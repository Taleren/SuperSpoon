using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ExposedMono))]

public class EExposedMono : Editor
{
    private SerializedProperty exposedSo;
    private SerializedObject assetSo;

    private void OnEnable()
    {
        exposedSo = serializedObject.FindProperty(nameof(exposedSo));
        SetAssetSo();
    }

    private void SetAssetSo()
    {
        if (exposedSo.objectReferenceValue != null)
        {
            assetSo = new SerializedObject(exposedSo.objectReferenceValue, serializedObject.targetObject);
        }
        else
        {
            assetSo = null;
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(exposedSo, new GUIContent("Target Asset"));

        if (EditorGUI.EndChangeCheck())
        {
            SetAssetSo();
        }

        if (assetSo != null)
        {
            SerializedProperty sp = assetSo.GetIterator();
            bool enterChild = true;
            while (sp.NextVisible(enterChild))
            {
                enterChild = false;
                EditorGUILayout.PropertyField(sp, true);
            }
            assetSo.ApplyModifiedProperties();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
