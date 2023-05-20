using UnityEngine;
using UnityEditor;

/// <summary>
/// Attribute to mark a serialized field as a required reference.
/// </summary>
public class RequiredReferenceAttribute : PropertyAttribute
{
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RequiredReferenceAttribute))]
public class RequiredReferenceDrawer : PropertyDrawer
{
    private readonly Color32 brightRed = new Color32(255, 100, 100, 255); 
    // Additional height for the label
    private const float RequiredLabelHeight = 20f;

    // Calculate the height of the property including the required label
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        bool hasReference = property.objectReferenceValue != null;

        if (!hasReference)
        {
            return EditorGUIUtility.singleLineHeight + RequiredLabelHeight;
        }

        return EditorGUI.GetPropertyHeight(property, label);
    }

    // Draw the property field and the required label
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        bool hasReference = property.objectReferenceValue != null;
        Color originalColor = GUI.color;

        Rect labelPosition = position;
        labelPosition.height = RequiredLabelHeight;

        if (!hasReference)
        {
            GUIStyle boldLabelStyle = new GUIStyle(EditorStyles.label);
            boldLabelStyle.fontStyle = FontStyle.Bold;
            boldLabelStyle.normal.textColor = brightRed;

            GUI.color = brightRed;

            // Display the required label above the property field
            EditorGUI.LabelField(labelPosition, new GUIContent("Reference is required"), boldLabelStyle);

            position.y += RequiredLabelHeight;
            position.height -= RequiredLabelHeight;

            // Display the property field below the required label
            EditorGUI.PropertyField(position, property, label, true);
        }
        else
        {
            // Display the property field normally
            EditorGUI.PropertyField(position, property, label, true);
        }

        EditorGUI.EndProperty();
        GUI.color = originalColor;

        // Adjust height when the reference is added or removed
        if (GUI.changed)
        {
            var newHeight = GetPropertyHeight(property, label);
            position.height = newHeight;
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
