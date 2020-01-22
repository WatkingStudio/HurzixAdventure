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

	protected Vector3 m_StartPosition;
	protected EnemyAction m_ActiveAction;
	const float k_SpriteFlipOffset = .5f;
	protected bool m_FacingRight = true;

	public bool FacingRight { get { return m_FacingRight; } }

	private void Start()
	{
		SetActiveAction(m_DefaultAction);
		m_StartPosition = gameObject.transform.position;
		Debug.Log(m_StartPosition);

		if (!m_Animator)
			Debug.LogError("No Animator has been assigned to " + gameObject.name);

		if (m_AvailableActions.Count == 0)
			Debug.LogWarning("No actions have been assigned to " + gameObject.name);
	}

	// Update is called once per frame
	private void Update()
    {
		if (m_ActiveAction == null)
			return;
		m_ActiveAction.PerformAction();
	}

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
			pos.x -= k_SpriteFlipOffset;
		else
			pos.x += k_SpriteFlipOffset;
		transform.localPosition = pos;
	}

	public virtual void IsWalking(bool walking)
	{
		m_Animator.SetBool("Walking", walking);
	}

	public void SetActiveAction(EnemyAction.Actions action)
	{
		if(CheckValidAction(action))
		{
			AssignValidAction(action);
		}
		else
		{
			Debug.LogError("Invalid Enemy Action");
		}
	}

	public void SetDefaultAction()
	{
		AssignValidAction(m_DefaultAction);
	}

	protected bool CheckValidAction(EnemyAction.Actions action)
	{
		foreach (EnemyAction.Actions act in m_AvailableActions)
		{
			if (action == act)
				return true;
		}
		return false;
	}

	protected void AssignValidAction(EnemyAction.Actions action)
	{
		switch(action)
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
		}
	}

	public virtual void ResetEnemy()
	{
		transform.position = m_StartPosition;
		m_Animator.SetTrigger("Idle");
		m_Animator.SetFloat("Speed", 0);
	}
}
