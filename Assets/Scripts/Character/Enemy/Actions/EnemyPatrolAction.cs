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
	private Collider2D m_WallCheckCollider;
	[SerializeField]
	private Collider2D m_FloorCheckCollider;
	[SerializeField]
	private LayerMask m_WhatIsGround;
	[SerializeField]
	private float m_WalkSpeed = 2.0f;
	[SerializeField]
	private BasicEnemy m_BasicEnemy;
	[SerializeField]
	private EnemyPlayerDetection m_EnemyPlayerDetection;

	public UnityEvent m_PlayerDetected;

	private Vector3 m_WalkAmount;
	private float m_WalkingDirection = 1.0f;
	private bool m_IsWalking = false;
	private EnemyAudio m_EnemyAudio;

    // Start is called before the first frame update
    void Start()
    {
		m_EnemyAudio = m_BasicEnemy.GetComponent<EnemyAudio>();
		m_Action = Actions.EnemyPatrol;
    }

	public override void PerformAction()
	{
		if(!m_IsWalking)
		{
			m_BasicEnemy.IsWalking(true);
			m_IsWalking = true;
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

		//m_EnemyAudio.PlayWalkAudioClip();
		m_BasicEnemy.transform.Translate(m_WalkAmount);
		
		if (m_EnemyPlayerDetection.CanPlayerBeSeen())
		{
			m_PlayerDetected.Invoke();
		}
	}
}
