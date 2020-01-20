using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncaTribe : BasicEnemy
{
	[SerializeField]
	private BoxCollider2D m_FrontCollider;
	[SerializeField]
	private LayerMask m_AttackableLayers;
	[SerializeField]
	private LayerMask m_GroundLayers;
	[SerializeField]
	private EnemyPlayerDetection m_EnemyPlayerDetection;
	[SerializeField]
	private EnemyMoveToPlayerAction m_EnemyMoveToPlayer;

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
		if (m_ActiveAction == null)
			return;
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

	public override void ResetEnemy()
	{
		base.ResetEnemy();
	}

	public void PlayerDetected()
	{
		SetActiveAction(EnemyAction.Actions.EnemyMoveToPlayer);
	}
}
