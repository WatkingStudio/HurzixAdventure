using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class EnemyMoveToPlayerAction
 * 
 * \brief This class holds the functionality of an enemys movement towards a player
 * 
 * \date 2019/20/12
 */ 
public class EnemyMoveToPlayerAction : EnemyAction
{
	[SerializeField]
	private EnemyPlayerDetection m_EnemyPlayerDetectionAction;
	[SerializeField]
	private Rigidbody2D m_RigidBody2D;
	[SerializeField, Tooltip("A mask determining what is ground to the character")]
	private LayerMask m_WhatIsGround;
	[SerializeField]
	private Collider2D m_CollisionCheckerCollider;
	[Range(0, .3f)]	[SerializeField, Tooltip("How much to smooth out the movement")]
	private float m_MovementSmoothing = .05f;
	[SerializeField]
	private BasicEnemy m_BasicEnemy;
	[SerializeField]
	private float m_Speed = 1f;
	[SerializeField]
	private Animator m_Animator;

	private PlayerCharacter m_PlayerCharacter;
	private bool m_IsInitialised = false;
	private Vector3 m_Velocity = Vector3.zero;

	private void Start()
	{
		m_Action = Actions.EnemyMoveToPlayer;
	}

	public override void PerformAction()
	{
		if (!m_IsInitialised)
		{
			m_PlayerCharacter = m_EnemyPlayerDetectionAction.PlayerCharacter;
			m_IsInitialised = true;
		}

		if(m_PlayerCharacter.transform.position.x > transform.position.x)
		{
			SetAnimationRight(m_Speed);
			Vector3 targetVelocity;
			targetVelocity = new Vector2(m_Speed, m_RigidBody2D.velocity.y);

			if (!CheckForCollision())
				m_RigidBody2D.velocity = Vector3.SmoothDamp(m_RigidBody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		}
		else
		{
			SetAnimationLeft(m_Speed);
			Vector3 targetVelocity;
			targetVelocity = new Vector2(-m_Speed, m_RigidBody2D.velocity.y);

			if (!CheckForCollision())
				m_RigidBody2D.velocity = Vector3.SmoothDamp(m_RigidBody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		}
	}

	private bool CheckForCollision()
	{
		if (m_CollisionCheckerCollider.IsTouchingLayers(m_WhatIsGround))
		{
			StopEnemy();
			return true;
		}

		return false;
	}

	public void StopEnemy()
	{
		Vector2 vec = new Vector2(0, m_RigidBody2D.velocity.y);
		m_RigidBody2D.velocity = vec;
		m_Animator.SetFloat("Speed", 0);
	}

	private void SetAnimationRight(float speed = 0)
	{
		m_Animator.SetBool("Left", false);
		m_Animator.SetBool("Right", true);
		m_Animator.SetFloat("Speed", speed);
	}

	private void SetAnimationLeft(float speed = 0)
	{
		m_Animator.SetBool("Right", false);
		m_Animator.SetBool("Left", true);
		m_Animator.SetFloat("Speed", speed);
	}
}
