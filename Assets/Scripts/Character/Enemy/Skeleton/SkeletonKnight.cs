using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class SkeletonKnight
 * 
 * \brief This class holds the functionality for a Skeleton Knight AI
 * 
 * \date 2019/20/12
 * 
 */
public class SkeletonKnight : BasicEnemy
{
	[SerializeField]
	private BoxCollider2D m_CollisionBox;
	[SerializeField]
	private EnemyMoveToPlayerAction m_EnemyMoveToPlayer;
	[SerializeField]
	private EnemyPlayerDetection m_EnemyPlayerDetection;
	[SerializeField]
	private Rigidbody2D m_RigidBody2D;
	[SerializeField, Tooltip("Which layers will this enemy target with attacks")]
	private LayerMask m_WhatIsTarget;

	private float m_DetectionTimerDefault = 0.5f;
	private float m_DetectionTimer;

    private void Start()
    {
		SetActiveAction(m_DefaultAction);
		m_DetectionTimer = m_DetectionTimerDefault;
		m_StartPosition = gameObject.transform.position;

		if (!m_Animator)
		{
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		}
		if (m_AvailableActions.Count == 0)
		{
			Debug.LogWarning("No actions have been assigned to " + gameObject.name);
		}
		if (!m_EnemyPlayerDetection)
		{
			Debug.LogError("No Enemy Player Detection script has been assigned to " + gameObject.name);
		}
		if (!m_EnemyMoveToPlayer)
		{
			Debug.LogError("No Enemy Move To Player script has been assigned to " + gameObject.name);
		}
		if (!m_CollisionBox)
		{
			Debug.LogError("No Collision Box has been assigned to " + gameObject.name);
		}
	}

    private void Update()
    {
		if (m_ActiveAction == null)
		{
			return;
		}
		m_ActiveAction.PerformAction();

		if(m_ActiveAction.Action == EnemyAction.Actions.EnemyMoveToPlayer)
		{
			m_DetectionTimer -= Time.deltaTime;
			if (m_DetectionTimer < 0)
			{
				m_DetectionTimer = m_DetectionTimerDefault;
				if (!m_EnemyPlayerDetection.CanPlayerBeSeen())
				{
					m_EnemyMoveToPlayer.StopEnemy();
					SetActiveAction(EnemyAction.Actions.EnemyPlayerDetection);
				}
			}

			if(m_CollisionBox.IsTouchingLayers(m_WhatIsTarget))
			{
				SetActiveAction(EnemyAction.Actions.EnemyMeleeAttack);
			}
		}
	}

	/// <summary>
	/// Reset the enemy.
	/// </summary>
	public override void ResetEnemy()
	{
		base.ResetEnemy();
	}

	/// <summary>
	/// Tells the animator to set the animation to left.
	/// </summary>
	/// <param name="speed">The speed of the character.</param>
	public void SetAnimationLeft(float speed = 0)
	{
		m_Animator.SetBool("Right", false);
		m_Animator.SetBool("Left", true);
		m_Animator.SetFloat("Speed", speed);
	}

	/// <summary>
	/// Tells the animator to set the animation to right.
	/// </summary>
	/// <param name="speed">The speed of the character.</param>
	public void SetAnimationRight(float speed = 0)
	{
		m_Animator.SetBool("Left", false);
		m_Animator.SetBool("Right", true);
		m_Animator.SetFloat("Speed", speed);
	}

	/// <summary>
	/// Stops the enemy movement.
	/// </summary>
	public override void StopEnemy()
	{
		m_RigidBody2D.velocity = new Vector2(0, m_RigidBody2D.velocity.y);
		m_Animator.SetFloat("Speed", 0);
	}
}
