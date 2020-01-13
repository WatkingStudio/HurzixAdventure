using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System;

[CustomEditor(typeof(EnemyMoveAction))]
public class EnemyMoveActionEditor : Editor
{
	EnemyMoveAction m_Action;

	private void OnEnable()
	{
		m_Action = (EnemyMoveAction)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
	}

	private void OnSceneGUI()
	{
		// https://docs.unity3d.com/540/Documentation/ScriptReference/Handles.PositionHandle.html 
		// Link to article on creating modifiable position handle

		//Make a copy of the waypoints
		Vector3 waypointA = m_Action.m_WaypointA;
		Vector3 waypointB = m_Action.m_WaypointB;

		//Begin check for a change
		EditorGUI.BeginChangeCheck();
		Vector3 positionHandleA = Handles.PositionHandle(waypointA, Quaternion.identity);
		Vector3 positionHandleB = Handles.PositionHandle(waypointB, Quaternion.identity);

		//If a change has occured, execute the following code
		if (EditorGUI.EndChangeCheck())
		{
			//Record a undo event, to allow the user to undo the change
			Undo.RecordObject(target, "Position Handle Movement");
			m_Action.m_WaypointA = positionHandleA;
			m_Action.m_WaypointB = positionHandleB;
		}
	}
}
