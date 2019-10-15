using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class PlayerAnimations
 * 
 * \brief This class is used to hold and control the animations of the player.
 * 
 * \date 2019/14/10
 * 
 */ 
public class PlayerAnimations : MonoBehaviour
{
	[SerializeField, Tooltip("The animator for the player while standing")]
	private RuntimeAnimatorController m_StandingController;
	[SerializeField, Tooltip("The animator for the player while crouching")]
	private RuntimeAnimatorController m_CrouchingController;
	[SerializeField]
	private Animator m_ActiveAnimator;

    // Start is called before the first frame update
    void Start()
    {
		m_ActiveAnimator.runtimeAnimatorController = m_StandingController;
    }

	public void PlayerDead(bool isDead)
	{
		m_ActiveAnimator.SetBool("IsDead", isDead);
	}

	public void PlayerHurt()
	{
		m_ActiveAnimator.SetTrigger("Hurt");
	}

	public void PlayerIdle()
	{
		m_ActiveAnimator.SetTrigger("Idle");
	}

	public void PlayerSprinting(bool isSprinting)
	{
		m_ActiveAnimator.SetBool("IsSprinting", isSprinting);
	}

	public void PlayerAttack()
	{
		m_ActiveAnimator.SetTrigger("Attack");
	}

	public void PlayerJumping(bool isJumping)
	{
		m_ActiveAnimator.SetBool("IsJumping", isJumping);
	}

	public void PlayerCrouching(bool isCrouching)
	{
		m_ActiveAnimator.SetBool("IsCrouching", isCrouching);

		if (isCrouching)
			m_ActiveAnimator.runtimeAnimatorController = m_CrouchingController;
		else
			m_ActiveAnimator.runtimeAnimatorController = m_StandingController;

		m_ActiveAnimator.SetBool("IsCrouching", isCrouching);
	}

	public void PlayerFalling(bool isFalling)
	{
		m_ActiveAnimator.SetBool("IsFalling", isFalling);
	}

	public void PlayerSpeed(float playerSpeed)
	{
		m_ActiveAnimator.SetFloat("Speed", Mathf.Abs(playerSpeed));
	}
}
