using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class IncaTribe
 * 
 * \brief This class holds the functionality for a Inca Tribe AI
 * 
 * \date 2020/22/01
 */ 
public class IncaTribe : BasicEnemy
{
	[SerializeField]
	private LayerMask m_AttackableLayers;
	[SerializeField]
	private EnemyMoveToPlayerAction m_EnemyMoveToPlayer;
	[SerializeField]
	private EnemyPlayerDetection m_EnemyPlayerDetection;
	[SerializeField]
	private BoxCollider2D m_FrontCollider;
	[SerializeField]
	private LayerMask m_GroundLayers;

	private float m_DetectionTimerDefault = 0.5f;
	private float m_DetectionTimer;

    // Start is called before the first frame update
    void Start()
    {
		SetActiveAction(m_DefaultAction);
		m_DetectionTimer = m_DetectionTimerDefault;
		m_StartPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		if(!m_EnemyMoveToPlayer)
        {
			Debug.LogError("No Enemy Move To Player Action script attached to " + gameObject.name);
		}
		if(!m_EnemyPlayerDetection)
        {
			Debug.LogError("No Enemy Player Detection script attached to " + gameObject.name);
		}
		if(!m_FrontCollider)
        {
			Debug.LogError("No Box Collider 2D attached to " + gameObject.name);
		}

		if (m_ActiveAction == null)
		{
			return;
		}
		m_ActiveAction.PerformAction();

		if(m_FrontCollider.IsTouchingLayers(m_AttackableLayers))
		{
			SetActiveAction(EnemyAction.Actions.EnemyMeleeAttack);
		}

		if(m_ActiveAction.Action == EnemyAction.Actions.EnemyMoveToPlayer)
		{
			m_DetectionTimer -= Time.deltaTime;
			if(m_DetectionTimer < 0)
			{
				m_DetectionTimer = m_DetectionTimerDefault;
				if(!m_EnemyPlayerDetection.CanPlayerBeSeen())
				{
					m_EnemyMoveToPlayer.StopEnemy();
					SetActiveAction(EnemyAction.Actions.EnemyPatrol);
				}
			}
		}
	}

	// Tells the Animator to Set the IsWalking Bool (Hard Coded For False).
	public override void SetWalking(bool walking)
	{
		m_Animator.SetBool("IsWalking", false);
	}

	// Performs the Code in Response to Detecting the Player.
	public void PlayerDetected()
	{
		SetActiveAction(EnemyAction.Actions.EnemyMoveToPlayer);
	}

	// Resets the Enemy.
	public override void ResetEnemy()
	{
		base.ResetEnemy();
	}

	// Sets the Animation to Left.
	public void SetAnimationLeft()
	{
		m_Animator.SetBool("IsWalking", true);
		if (m_FacingRight)
		{
			Flip();
		}
	}

	// Sets the Animation to Right.
	public void SetAnimationRight()
	{
		m_Animator.SetBool("IsWalking", true);
		if (!m_FacingRight)
		{
			Flip();
		}
	}

	// Tells the Animator the Enemy has Stopped.
	public override void StopEnemy()
	{
		m_Animator.SetBool("IsWalking", false);
		m_Animator.SetTrigger("Idle");
	}
}
