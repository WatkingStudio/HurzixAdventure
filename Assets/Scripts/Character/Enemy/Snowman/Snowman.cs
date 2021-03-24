using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class Snowman
 * 
 * \brief This class holds the functionality for a Snowman AI
 * 
 * \date 2020/24/01
 */ 
public class Snowman : BasicEnemy
{
	[SerializeField]
	private EnemyPlayerDetection m_EnemyPlayerDetection;

	void Start()
    {
		if(!m_EnemyPlayerDetection)
        {
			Debug.LogError("No EnemyPlayerDetection script has been assigned to " + gameObject.name);
		}

		SetActiveAction(m_DefaultAction);
		m_StartPosition = gameObject.transform.position;
    }

    void Update()
    {
		if (m_ActiveAction == null)
		{
			return;
		}
		m_ActiveAction.PerformAction();

		if (m_ActiveAction.Action == EnemyAction.Actions.EnemyRangedAttack)
		{
			if (!m_EnemyPlayerDetection.CanPlayerBeSeen())
			{
				SetActiveAction(EnemyAction.Actions.EnemyPatrol);
			}
		}
    }

	/// <summary>
	/// Activate when a player has been detected.
	/// </summary>
	public void PlayerDetected()
	{
		SetActiveAction(EnemyAction.Actions.EnemyRangedAttack);
	}

	/// <summary>
	/// Reset the enemy.
	/// </summary>
	public override void ResetEnemy()
	{
		base.ResetEnemy();
	}

	/// <summary>
	/// Tell the animator if the snowman is walking or not.
	/// </summary>
	/// <param name="walking">If the snowman is walking or not.</param>
	public override void SetWalking(bool walking)
	{
		base.SetWalking(walking);
	}

	/// <summary>
	/// Stops the enemy.
	/// </summary>
	public override void StopEnemy()
	{
		
	}
}
