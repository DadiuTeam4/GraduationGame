// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Walk))]
public class DynamicWalkFields : Editor 
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		Walk walk = target as Walk;
		walk.walkToWaypoint = GUILayout.Toggle(walk.walkToWaypoint, "Walk to waypoint");
		walk.walkInDirection = GUILayout.Toggle(walk.walkInDirection, "Walk in direction");

		// if (walk.walkToWaypoints)
		// {
		// 	EditorGUI.BeginChangeCheck();
		// 	SerializedProperty waypoints = serializedObject.FindProperty("waypoints");
		// 	EditorGUILayout.PropertyField(waypoints, true);
		// 	if (EditorGUI.EndChangeCheck())
		// 	{
        //     	serializedObject.ApplyModifiedProperties();
		// 	}
		// }
		if (walk.walkInDirection)
		{
			walk.direction = EditorGUILayout.Vector3Field("Direction in world space", walk.direction);
		}
	}
}
