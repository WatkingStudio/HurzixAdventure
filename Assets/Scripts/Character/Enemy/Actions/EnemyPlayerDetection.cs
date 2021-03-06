﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class EnemyPlayerDetection
 * 
 * \brief  This class holds the functionality for the enemy to detect the player.
 * 
 *	This action will have the enemy standing still until they detect the player. They will then
 *	 move towards the player and attempt to attack them. They will move off ledges and be able to fall.
 *	After 1 second of the player not being in sight the enemy will return to idle. While idle every 4 
 *	 seconds they will turn around.
 * 
 * \date 2019/16/12
 */ 
public class EnemyPlayerDetection : EnemyAction
{
	[SerializeField]
	private BasicEnemy m_BasicEnemy;
	[SerializeField]
	private Transform m_LineOfSightEnd;
	[SerializeField]
	private float m_MaxLOSAngle = 150;
	[SerializeField]
	private float m_MinLOSAngle = 30;
	[SerializeField]
	private Transform m_Player;
	[SerializeField]
	private SpriteRenderer m_Sprite;

	public PlayerCharacter PlayerCharacter { get { return m_PlayerCharacter; } }
	public bool PlayerInRange { get { return m_PlayerInRange; } }

	//Is the player within the enemy's sight rangle collider
	private PlayerCharacter m_PlayerCharacter;
	private bool m_PlayerInRange = false;

	private void Start()
	{
		m_Action = Actions.EnemyPlayerDetection;

		if (!m_Sprite)
		{
			Debug.LogError("No Sprite Renderer has been assigned to " + gameObject.name);
		}
		if (!m_LineOfSightEnd)
		{
			Debug.LogError("No Line Of Sight End Transform has been assigned to " + gameObject.name);
		}
		if (!m_Player)
		{
			Debug.LogError("No Player Transform has been assigned to " + gameObject.name);
		}
		if (!m_BasicEnemy)
		{
			Debug.LogError("No Basic Enemy has been assigned to " + gameObject.name);
		}
	}

	/// <summary>
	/// Perform the player detection action.
	/// </summary>
	public override void PerformAction()
	{
		if (CanPlayerBeSeen())
		{
			m_BasicEnemy.SetActiveAction(Actions.EnemyMoveToPlayer);
		}
	}

	/// <summary>
	/// Can a player be seen.
	/// </summary>
	/// <returns>True if thee player can be seen, false if not.</returns>
	public bool CanPlayerBeSeen()
	{
		//Only need to check visibility if the player is within range of the enemy.
		if (m_PlayerInRange)
		{
            if (IsPlayerInFieldOfView())
            {
                return (!PlayerHiddenByObstacles());
            }
		}

		return false;
	}

	/// <summary>
	/// Trigger is a player is in the objects collision box.
	/// </summary>
	/// <param name="collision">The collider colliding with this object.</param>
	private void OnTriggerStay2D(Collider2D collision)
	{
		//If the collider is the player then they are in range.
		if (collision.GetComponentInParent<PlayerCharacter>())
		{
			m_PlayerInRange = true;
		}
	}

	/// <summary>
	/// Trigger if a player leaves this objects collision box.
	/// </summary>
	/// <param name="collision">The collider colliding with this object.</param>
	private void OnTriggerExit2D(Collider2D collision)
	{
        //If the collider is the player then they are no longer in range.
        if (collision.GetComponentInParent<PlayerCharacter>())
        {
            m_PlayerInRange = false;
        }
	}

	/// <summary>
	/// Is the player in this objects field of view.
	/// </summary>
	/// <returns>True if the player is in the objects field of view, false if not.</returns>
	private bool IsPlayerInFieldOfView()
	{
		Vector2 directionToPlayer = m_Player.position - transform.position;
		Debug.DrawLine(transform.position, m_Player.position, Color.magenta);
		
		Vector2 lineOfSight = m_LineOfSightEnd.position - transform.position;
		Debug.DrawLine(transform.position, m_LineOfSightEnd.position, Color.yellow);

		float angle = Vector2.SignedAngle(directionToPlayer, lineOfSight);

		if (angle > m_MinLOSAngle && angle < m_MaxLOSAngle)
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// Is the player hidden by obstacles.
	/// </summary>
	/// <returns>True if the player is hidden by obstacles, false if not.</returns>
	private bool PlayerHiddenByObstacles()
	{
		float distanceToPlayer = Vector2.Distance(transform.position, m_Player.position);
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, m_Player.position - transform.position, distanceToPlayer);
		Debug.DrawRay(transform.position, m_Player.position - transform.position, Color.blue);
		
		List<float> distances = new List<float>();

		foreach(RaycastHit2D hit in hits)
		{
			if (hit.transform.tag == "Enemy")
			{
				continue;
			}

			if (hit.transform.tag != "Player")
			{
				return true;
			}
			else
			{
				m_PlayerCharacter = hit.transform.gameObject.GetComponent<PlayerCharacter>();
			}
		}
		return false;
	}
}
