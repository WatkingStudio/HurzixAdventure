using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class BasicEnemy
 * 
 * \brief This class holds the functionality for a basic enemy AI
 * 
 * \date 2019/15/10
 * 
 */
public class BasicEnemy : MonoBehaviour
{
	[SerializeField]
	protected Animator m_Animator;
	[SerializeField, Tooltip("Which actions are available to this enemy")]
	protected List<EnemyAction.Actions> m_AvailableActions;
	[SerializeField]
	protected EnemyAction.Actions m_DefaultAction;

	protected EnemyAction m_ActiveAction;
	protected bool m_FacingRight = true;
	protected Vector3 m_StartPosition;

	const float m_SpriteFlipOffset = .5f;

	public bool FacingRight { get { return m_FacingRight; } }

	private void Start()
	{
		SetActiveAction(m_DefaultAction);
		m_StartPosition = gameObject.transform.position;
		Debug.Log(m_StartPosition);

		if (!m_Animator)
		{
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		}

		if (m_AvailableActions.Count == 0)
		{
			Debug.LogWarning("No actions have been assigned to " + gameObject.name);
		}
	}

	private void Update()
    {
		if (m_ActiveAction == null)
		{
			return;
		}
		m_ActiveAction.PerformAction();
	}

	/// <summary>
	/// Assign the specified action to the enemy.
	/// </summary>
	/// <param name="action">Action to assign.</param>
	protected void AssignValidAction(EnemyAction.Actions action)
	{
		switch (action)
		{
			case EnemyAction.Actions.EnemyMoveAction:
				m_ActiveAction = GetComponentInChildren<EnemyMoveAction>();
				break;
			case EnemyAction.Actions.EnemyPlayerDetection:
				m_ActiveAction = GetComponentInChildren<EnemyPlayerDetection>();
				break;
			case EnemyAction.Actions.EnemyMoveToPlayer:
				m_ActiveAction = GetComponentInChildren<EnemyMoveToPlayerAction>();
				break;
			case EnemyAction.Actions.EnemyMeleeAttack:
				m_ActiveAction = GetComponentInChildren<EnemyMeleeAttack>();
				break;
			case EnemyAction.Actions.EnemyPatrol:
				m_ActiveAction = GetComponentInChildren<EnemyPatrolAction>();
				break;
			case EnemyAction.Actions.EnemyRangedAttack:
				m_ActiveAction = GetComponentInChildren<EnemyRangedAttack>();
				break;
		}
		m_ActiveAction.InitialiseAction();
	}

	/// <summary>
	/// Check that this enemy can use the specified action.
	/// </summary>
	/// <param name="action">The action to check.</param>
	/// <returns>True if the action is valid, false if not.</returns>
	protected bool CheckValidAction(EnemyAction.Actions action)
	{
		foreach (EnemyAction.Actions act in m_AvailableActions)
		{
			if (action == act)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Flips the character
	/// </summary>
	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

		//Reposition - This is used so that the sprite doesn't turn and warp into a wall.
		//This is an issue with the sprite and could be fixed by editing the sprite.
		Vector3 pos = transform.localPosition;
		if (!m_FacingRight)
		{
			pos.x -= m_SpriteFlipOffset;
		}
		else
		{
			pos.x += m_SpriteFlipOffset;
		}
		transform.localPosition = pos;
	}

	/// <summary>
	/// Resets the enemy
	/// </summary>
	public virtual void ResetEnemy()
	{
		gameObject.SetActive(true);
		transform.position = m_StartPosition;
		m_Animator.SetTrigger("Idle");
		m_Animator.SetFloat("Speed", 0);
	}

	/// <summary>
	/// Set the action to the specified action.
	/// </summary>
	/// <param name="action">The desired action.</param>
	public void SetActiveAction(EnemyAction.Actions action)
	{
		if (CheckValidAction(action))
		{
			AssignValidAction(action);
		}
		else
		{
			Debug.LogError("Invalid Enemy Action");
		}
	}

	/// <summary>
	/// Set the enemies action to the default action.
	/// </summary>
	public void SetDefaultAction()
	{
		AssignValidAction(m_DefaultAction);
	}

	/// <summary>
	/// Tell the animator if the character is walking or not.
	/// </summary>
	/// <param name="walking">If the character is walking</param>
	public virtual void SetWalking(bool walking)
	{
		m_Animator.SetBool("Walking", walking);
	}

	/// <summary>
	/// Stop the enemy.
	/// </summary>
	public virtual void StopEnemy()
	{

	}
}
