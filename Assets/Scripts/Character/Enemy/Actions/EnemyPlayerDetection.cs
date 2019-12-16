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
	//Is the player within the enemy's sight rangle collider
	private bool m_PlayerInRange = false;

	[SerializeField]
	private SpriteRenderer m_Sprite;
	[SerializeField]
	private Transform m_LineOfSightEnd;
	[SerializeField]
	private Transform m_Player;

	public bool PlayerInRange { get { return m_PlayerInRange; } }

	public override void PerformAction()
	{
		if (CanPlayerBeSeen())
			m_Sprite.color = Color.green;
		else
			m_Sprite.color = Color.red;
	}

	private bool CanPlayerBeSeen()
	{
		//Only need to check visibility if the player is within range of the enemy
		if(m_PlayerInRange)
			if (PlayerInFieldOfView())
				return (!PlayerHiddenByObstacles());

		return false;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		//If the collider is the player then they are in range.
		if (collision.GetComponentInParent<PlayerCharacter>())
			m_PlayerInRange = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//If the collider is the player then they are no longer in range.
		if (collision.GetComponentInParent<PlayerCharacter>())
			m_PlayerInRange = false;
	}

	private bool PlayerInFieldOfView()
	{
		Vector2 directionToPlayer = m_Player.position - transform.position;
		Debug.DrawLine(transform.position, m_Player.position, Color.magenta);

		Vector2 lineOfSight = m_LineOfSightEnd.position - transform.position;
		Debug.DrawLine(transform.position, m_LineOfSightEnd.position, Color.yellow);

		float angle = Vector2.Angle(directionToPlayer, lineOfSight);

		if (angle < 65)
			return true;

		return false;
	}

	private bool PlayerHiddenByObstacles()
	{
		float distanceToPlayer = Vector2.Distance(transform.position, m_Player.position);
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, m_Player.position - transform.position, distanceToPlayer);
		Debug.DrawRay(transform.position, m_Player.position - transform.position, Color.blue);

		List<float> distances = new List<float>();

		foreach(RaycastHit2D hit in hits)
		{
			if (hit.transform.tag == "Enemy")
				continue;

			if (hit.transform.tag != "Player")
				return true;
		}

		return false;
	}
}
