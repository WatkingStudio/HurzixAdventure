using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class BasicEnemy
 * 
 * \brief This class hold the functionality for a basic enemy AI
 * 
 * \date 2019/15/10
 * 
 */
public class BasicEnemy : MonoBehaviour
{
	const float k_SpriteFlipOffset = .5f;
	private bool m_FacingRight = true;

	[SerializeField]
	private Animator m_Animator;
	private EnemyAction m_ActiveAction;
	[SerializeField, Tooltip("Which actions are available to this enemy")]
	private List<EnemyAction.Actions> m_AvailableActions;

	private void Start()
	{
		SetActiveAction(EnemyAction.Actions.EnemyMoveAction);
	}

	// Update is called once per frame
	void Update()
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

	public void IsWalking(bool walking)
	{
		m_Animator.SetBool("Walking", walking);
	}

	public void SetActiveAction(EnemyAction.Actions action)
	{
		if(CheckValidAction(action))
		{
			AssignValidAction(action);
		}
	}

	private bool CheckValidAction(EnemyAction.Actions action)
	{
		foreach (EnemyAction.Actions act in m_AvailableActions)
		{
			if (action == act)
				return true;
		}
		return false;
	}

	private void AssignValidAction(EnemyAction.Actions action)
	{
		switch(action)
		{
			case EnemyAction.Actions.EnemyMoveAction:
				m_ActiveAction = GetComponentInChildren<EnemyMoveAction>();
				break;
		}
	}
}
