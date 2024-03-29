﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class EnemyMoveAction
 * 
 * \brief This class holds the functionality of an enemy move action
 * 
 * \date 2019/15/10
 * 
 */
public class EnemyMoveAction : EnemyAction
{
	[SerializeField]
	private BasicEnemy m_BasicEnemy;
	[Header("Enemy Data")]
	[SerializeField]
	private float m_WalkSpeed = 2.0f;
	[Header("Waypoints")]
	[SerializeField]
	public Vector3 m_WaypointA;
	[SerializeField]
	public Vector3 m_WaypointB;

	private EnemyAudio m_EnemyAudio;
	private bool m_IsWalking = false;
	private Vector3 m_WalkAmount;
	private float m_WalkingDirection = 1.0f;

	private void Start()
	{
		if (!m_BasicEnemy)
		{
			Debug.LogError("No Basic Enemy script attached to " + gameObject.name);
		}

		if (m_WaypointA.Equals(m_WaypointB))
		{
			Debug.LogWarning("Both waypoints for " + gameObject.name + " are equal.");
		}
		else if (m_WaypointB.x < m_WaypointA.x)
		{
			Debug.LogError("Invalid Waypoint positions. Waypoint B cannot be positioned before Waypoint A");
		}

		m_EnemyAudio = m_BasicEnemy.GetComponent<EnemyAudio>();
		m_Action = Actions.EnemyMoveAction;
	}

	/// <summary>
	/// Sets up the move action.
	/// </summary>
	public override void PerformAction()
	{
		if (!m_IsWalking)
		{
			m_BasicEnemy.SetWalking(true);
			m_IsWalking = true;
		}

		m_WalkAmount.x = m_WalkingDirection * m_WalkSpeed * Time.deltaTime;
		if(m_BasicEnemy.transform.position.x > m_WaypointB.x)
		{
			m_WalkingDirection = -1.0f;
			m_BasicEnemy.Flip();
		}
		else if(m_BasicEnemy.transform.position.x < m_WaypointA.x)
		{
			m_WalkingDirection = 1.0f;
			m_BasicEnemy.Flip();
		}

		m_EnemyAudio.PlayWalkAudioClip();
		m_BasicEnemy.transform.Translate(m_WalkAmount);
	}

	/// <summary>
	/// Draws interactable gizmos in the editor.
	/// </summary>
	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 0f, 0f, 1f);
		Gizmos.DrawLine(m_WaypointA, m_WaypointB);
	}
}
