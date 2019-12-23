﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class BasicEnemy
 * 
 * \brief This class holds the functionality for a Skeleton Knight AI
 * 
 * \date 2019/20/12
 * 
 */
public class SkeletonKnight : BasicEnemy
{
	[SerializeField]
	private EnemyPlayerDetection m_EnemyPlayerDetection;
	[SerializeField]
	private EnemyMoveToPlayerAction m_EnemyMoveToPlayer;
	[SerializeField]
	private BoxCollider2D m_CollisionBox;
	[SerializeField, Tooltip("Which layers will this enemy target with attacks")]
	private LayerMask m_WhatIsTarget;

	private float m_DetectionTimerDefault = 0.5f;
	private float m_DetectionTimer;

    // Start is called before the first frame update
    private void Start()
    {
		SetActiveAction(m_DefaultAction);
		m_DetectionTimer = m_DetectionTimerDefault;

		if (!m_Animator)
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		if (m_AvailableActions.Count == 0)
			Debug.LogWarning("No actions have been assigned to " + gameObject.name);
		if (!m_EnemyPlayerDetection)
			Debug.LogError("No Enemy Player Detection script has been assigned to " + gameObject.name);
		if (!m_EnemyMoveToPlayer)
			Debug.LogError("No Enemy Move To Player script has been assigned to " + gameObject.name);
	}

    // Update is called once per frame
    private void Update()
    {
		if (m_ActiveAction == null)
			return;
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
}
