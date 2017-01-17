using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PowerUp)), CanEditMultipleObjects]
public class PowerUpEditor : Editor {

	public SerializedProperty PowerUpName_Prop, PowerUpID_Prop, PowerUpD_Prop, PowerUpT_Prop, AddItem_Prop, GiveItem_Prop;

	void OnEnable() {

		PowerUpName_Prop = serializedObject.FindProperty ("PowerUpName");
		PowerUpID_Prop = serializedObject.FindProperty ("PowerUpID");
		PowerUpD_Prop = serializedObject.FindProperty ("PowerUpD");
		AddItem_Prop = serializedObject.FindProperty ("AddItem");
		GiveItem_Prop = serializedObject.FindProperty ("GiveItem");

	}
		
	public override void OnInspectorGUI() {
		
		serializedObject.Update ();

		EditorGUILayout.PropertyField( PowerUpName_Prop );
		EditorGUILayout.PropertyField( PowerUpID_Prop );

		EditorGUILayout.PropertyField( PowerUpD_Prop, new GUIContent("Power Up Type:") );

		PowerUpDictionary d = (PowerUpDictionary) PowerUpD_Prop.enumValueIndex;

		switch (d) {

			case PowerUpDictionary.BOMB:

				EditorGUILayout.PropertyField (AddItem_Prop, new GUIContent("Add Value:"));
				break;

			case PowerUpDictionary.VITAL:

				EditorGUILayout.PropertyField (AddItem_Prop, new GUIContent("Add Value:"));
				break;

		}

		serializedObject.ApplyModifiedProperties ();

	}

}