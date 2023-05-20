using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute { }

#if UNITY_EDITOR

// Custom property drawer for the ReadOnly attribute
[UnityEditor.CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : UnityEditor.PropertyDrawer
{
    // Calculate the height of the property in the inspector
    public override float GetPropertyHeight(UnityEditor.SerializedProperty property, GUIContent label)
    {
        return UnityEditor.EditorGUI.GetPropertyHeight(property, label, true);
    }

    // Render the property in the inspector with GUI disabled
    public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // Disable the field

        // Render the property as a readonly field
        UnityEditor.EditorGUI.PropertyField(position, property, label, true);

        GUI.enabled = true; // Enable the GUI again
    }
}

#endif
