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
	public Vector3 m_WaypointA;
	[SerializeField]
	public Vector3 m_WaypointB;
	[SerializeField]
	private float m_WalkSpeed = 2.0f;

	private Vector3 m_WalkAmount;
	private float m_WalkingDirection = 1.0f;
	private bool m_IsWalking = false;

	[SerializeField]
	private BasicEnemy m_BasicEnemy;

	private EnemyAudio m_EnemyAudio;

	private void Start()
	{
		//This is done instead of assigning the value through a SerializeField to ensure it is the EnemyAudio script
		// attached to the same GameObject as the BasicEnemy script
		m_EnemyAudio = m_BasicEnemy.GetComponent<EnemyAudio>();
	}

	public override void PerformAction()
	{
		if (!m_IsWalking)
		{
			m_BasicEnemy.IsWalking(true);
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
}
