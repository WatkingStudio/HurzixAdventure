using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

/**
 * \class EnemyMoveToPlayerAction
 * 
 * \brief This class holds the functionality of an enemys movement towards a player
 * 
 * \date 2019/20/12
 */ 
public class EnemyMoveToPlayerAction : EnemyAction
{
	[Serializable]
	public class SetAnimationEvent : UnityEvent<float>
	{ }

	[SerializeField]
	private Animator m_Animator;
	[SerializeField]
	private BasicEnemy m_BasicEnemy;
	[SerializeField]
	private Collider2D m_CollisionCheckerCollider;
	[SerializeField]
	private EnemyAudio m_EnemyAudio;
	[SerializeField]
	private EnemyPlayerDetection m_EnemyPlayerDetectionAction;
	[Range(0, .3f)]
	[SerializeField, Tooltip("How much to smooth out the movement")]
	private float m_MovementSmoothing = .05f;
	[SerializeField]
	private Rigidbody2D m_RigidBody2D;
	[SerializeField]
	private float m_Speed = 1f;
	[SerializeField, Tooltip("A mask determining what is ground to the character")]
	private LayerMask m_WhatIsGround;

	public SetAnimationEvent m_MoveLeft;
	public SetAnimationEvent m_MoveRight;
	public UnityEvent m_StopEnemy;

	private bool m_IsInitialised = false;
	private PlayerCharacter m_PlayerCharacter;
	private Vector3 m_Velocity = Vector3.zero;

	private void Start()
	{
		m_Action = Actions.EnemyMoveToPlayer;

		if (!m_EnemyPlayerDetectionAction)
		{
			Debug.LogError("No Enemy Player Detection script has been assigned to " + gameObject.name);
		}
		if (!m_RigidBody2D)
        {
			Debug.LogError("No RigidBody2D has been assigned to " + gameObject.name);
		}			
		if (!m_CollisionCheckerCollider)
		{
			Debug.LogError("No Collision Check Collider has been assigned to " + gameObject.name);
		}
		if (!m_BasicEnemy)
		{
			Debug.LogError("No Basic Enemy script has been assigned to " + gameObject.name);
		}
		if (!m_Animator)
		{
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		}
		if (!m_EnemyAudio)
		{
			Debug.LogError("No Enemy Audio has been assigned to " + gameObject.name);
		}
	}

	/// <summary>
	/// Sets the velocity of the enemy towards the player.
	/// </summary>
	public override void PerformAction()
	{
		if (!m_IsInitialised)
		{
			m_PlayerCharacter = m_EnemyPlayerDetectionAction.PlayerCharacter;
			m_IsInitialised = true;
		}

		Vector3 targetVelocity;
		if (m_PlayerCharacter.transform.position.x > transform.position.x)
		{
			m_MoveRight.Invoke(m_Speed);
			targetVelocity = new Vector2(m_Speed, m_RigidBody2D.velocity.y);
		}
		else
		{			
			m_MoveLeft.Invoke(m_Speed);
			targetVelocity = new Vector2(-m_Speed, m_RigidBody2D.velocity.y);
		}

		if(!CheckForCollision())
        {
			m_RigidBody2D.velocity = Vector3.SmoothDamp(m_RigidBody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		}

		m_EnemyAudio.PlayWalkAudioClip();
	}

	/// <summary>
	/// Invokes the stop enemy event.
	/// </summary>
	public void StopEnemy()
	{
		m_StopEnemy.Invoke();
	}

	/// <summary>
	/// Checks if the collision box collides with the floor.
	/// </summary>
	/// <returns>True if a collision happens, false if not.</returns>
	private bool CheckForCollision()
	{
		if (m_CollisionCheckerCollider.IsTouchingLayers(m_WhatIsGround))
		{
			StopEnemy();
			return true;
		}

		return false;
	}
}
