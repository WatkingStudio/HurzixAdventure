using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System;

[CustomEditor(typeof(Fireball))]
public class FireballEditor : Editor
{
	Fireball m_Fireball;

	private void OnEnable()
	{
		m_Fireball = (Fireball)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
	}

	private void OnSceneGUI()
	{
		//Make a copy of the waypoints
		Vector3 basePoint = m_Fireball.m_BasePoint;

		//Begin check for a change
		EditorGUI.BeginChangeCheck();
		Vector3 positionHandleA = Handles.PositionHandle(basePoint, Quaternion.identity);

		//If a change has occured, execute the following code
		if (EditorGUI.EndChangeCheck())
		{
			//Record a undo event, to allow the user to undo the change
			Undo.RecordObject(target, "Base Point Handle Movement");
			m_Fireball.m_BasePoint = basePoint;
		}
	}
}
