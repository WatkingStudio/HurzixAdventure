using System.Collections;
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
	private Transform m_WaypointA;
	[SerializeField]
	private Transform m_WaypointB;
	[SerializeField]
	private float m_WalkSpeed = 2.0f;

	private Vector3 m_WalkAmount;
	private float m_WalkingDirection = 1.0f;
	private bool m_IsWalking = false;

	[SerializeField]
	private BasicEnemy m_BasicEnemy;

	public override void PerformAction()
	{
		if (!m_IsWalking)
		{
			m_BasicEnemy.IsWalking(true);
			m_IsWalking = true;
		}

		m_WalkAmount.x = m_WalkingDirection * m_WalkSpeed * Time.deltaTime;
		if(m_BasicEnemy.transform.position.x > m_WaypointB.position.x)
		{
			m_WalkingDirection = -1.0f;
			m_BasicEnemy.Flip();
		}
		else if(m_BasicEnemy.transform.position.x < m_WaypointA.position.x)
		{
			m_WalkingDirection = 1.0f;
			m_BasicEnemy.Flip();
		}

		m_BasicEnemy.transform.Translate(m_WalkAmount);
	}
}
