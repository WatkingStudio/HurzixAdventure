using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveAction : EnemyAction
{
	[SerializeField]
	private Transform m_PointA;
	[SerializeField]
	private Transform m_PointB;
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
		if(m_BasicEnemy.transform.position.x > m_PointB.position.x)
		{
			m_WalkingDirection = -1.0f;
			m_BasicEnemy.Flip();
		}
		else if(m_BasicEnemy.transform.position.x < m_PointA.position.x)
		{
			m_WalkingDirection = 1.0f;
			m_BasicEnemy.Flip();
		}

		m_BasicEnemy.transform.Translate(m_WalkAmount);
	}
}
