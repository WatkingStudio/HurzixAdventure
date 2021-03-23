using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

/*
 * \class EnemyPatrolAction
 * 
 * \brief This class holds the functionality of an enemy patrol action
 * 
 * This class is similar to the EnemyMoveAction class, however it is designed for an 
 *  enemy which will not always patrol the same area.
 * 
 * \date 2020/20/01
 */
public class EnemyPatrolAction : EnemyAction
{
	[SerializeField]
	private Animator m_Animator;
	[SerializeField]
	private BasicEnemy m_BasicEnemy;
	[SerializeField]
	private EnemyPlayerDetection m_EnemyPlayerDetection;
	[SerializeField]
	private Collider2D m_FloorCheckCollider;
	[SerializeField]
	private float m_PatrolCooldownTime = 1.0f;
	[SerializeField]
	private float m_WalkSpeed = 2.0f;
	[SerializeField]
	private Collider2D m_WallCheckCollider;
	[SerializeField]
	private LayerMask m_WhatIsGround;

	public UnityEvent m_PlayerDetected;

	private EnemyAudio m_EnemyAudio;
	private bool m_IsWalking = false;
	private bool m_PatrolCooldown = false;
	private Vector3 m_WalkAmount;
	private float m_WalkingDirection = 1.0f;

    void Start()
    {
		if(!m_Animator)
        {
			Debug.LogError("No Animator script has been assigned to " + gameObject.name);
		}
		if(!m_BasicEnemy)
		{
			Debug.LogError("No Basic Enemy script has been assigned to " + gameObject.name);
		}
		if(!m_EnemyPlayerDetection)
        {
			Debug.LogError("No Enemy Player Detection script has been assigned to " + gameObject.name);
		}
		if(!m_FloorCheckCollider)
        {
			Debug.LogError("No Floor Check Collider2D has been assigned to " + gameObject.name);
		}
		if(!m_WallCheckCollider)
        {
			Debug.LogError("No Wall Check Collider2D has been assigned to " + gameObject.name);
		}

		m_EnemyAudio = m_BasicEnemy.GetComponent<EnemyAudio>();
		m_Action = Actions.EnemyPatrol;
	}

	// Initialise the Enemy Patrol Action.
	public override void InitialiseAction()
	{
		base.InitialiseAction();
		m_PatrolCooldown = true;
		StartCoroutine(PatrolCooldown());
	}

	// Performs the Patrol Action.
	public override void PerformAction()
	{
		if(!m_IsWalking)
		{
			m_BasicEnemy.SetWalking(true);
			m_IsWalking = true;
			m_Animator.SetBool("IsWalking", true);
			if (m_WalkingDirection == 1 && !m_BasicEnemy.FacingRight)
			{
				m_BasicEnemy.Flip();
			}
			else if (m_WalkingDirection == -1 && m_BasicEnemy.FacingRight)
			{
				m_BasicEnemy.Flip();
			}
		}

		m_WalkAmount.x = m_WalkingDirection * m_WalkSpeed * Time.deltaTime;
		
		if (m_WallCheckCollider.IsTouchingLayers(m_WhatIsGround))
		{
			m_WalkingDirection = -m_WalkingDirection;
			m_BasicEnemy.Flip();
		}
		else if(!m_FloorCheckCollider.IsTouchingLayers(m_WhatIsGround))
		{
			m_WalkingDirection = -m_WalkingDirection;
			m_BasicEnemy.Flip();
		}

		m_EnemyAudio.PlayWalkAudioClip();
		m_BasicEnemy.transform.Translate(m_WalkAmount);
		
		if (m_EnemyPlayerDetection.CanPlayerBeSeen() && !m_PatrolCooldown)
		{
			m_PlayerDetected.Invoke();
			m_IsWalking = false;
		}
	}

	// Start a Patrol Cooldown Period.
	private IEnumerator PatrolCooldown()
	{
		yield return new WaitForSeconds(m_PatrolCooldownTime);
		m_PatrolCooldown = false;
	}
}
