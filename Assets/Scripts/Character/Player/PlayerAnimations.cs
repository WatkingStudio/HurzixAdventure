using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class PlayerAnimations
 * 
 * \brief This class is used to hold and control the animations of the player.
 * 
 * \date 2019/15/10
 * 
 */ 
public class PlayerAnimations : MonoBehaviour
{
	[Header("Animator Controllers")]
	[SerializeField, Tooltip("The animator for the player while standing")]
	private RuntimeAnimatorController m_StandingController;
	[SerializeField, Tooltip("The animator for the player while crouching")]
	private RuntimeAnimatorController m_CrouchingController;
	[SerializeField]
	private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
		if (!m_StandingController)
			Debug.LogError("No Animator Controller has been assigned to " + gameObject.name + " while standing");

		if (!m_CrouchingController)
			Debug.LogWarning("No Animator Controller has been assigned to " + gameObject.name + "while crouching");

		if (!m_Animator)
			Debug.LogError("No Animator has been assigned to " + gameObject.name);

		m_Animator.runtimeAnimatorController = m_StandingController;
    }

	public void PlayerDead(bool isDead)
	{
		m_Animator.SetBool("IsDead", isDead);
	}

	public void PlayerHurt()
	{
		m_Animator.SetTrigger("Hurt");
	}

	public void PlayerIdle()
	{
		m_Animator.SetTrigger("Idle");
	}

	public void PlayerSprinting(bool isSprinting)
	{
		m_Animator.SetBool("IsSprinting", isSprinting);
	}

	public void PlayerAttack()
	{
		m_Animator.SetTrigger("Attack");
	}

	public void PlayerJumping(bool isJumping)
	{
		m_Animator.SetBool("IsJumping", isJumping);
	}

	//When the player crouches/uncrouches, both of the animators need to know this. So before
	// changing controller the bool is set to true/false, and after the controller is set
	// the bool in the other controller is also set to true/false
	public void PlayerCrouching(bool isCrouching)
	{
		m_Animator.SetBool("IsCrouching", isCrouching);

		if (isCrouching)
			m_Animator.runtimeAnimatorController = m_CrouchingController;
		else
			m_Animator.runtimeAnimatorController = m_StandingController;

		m_Animator.SetBool("IsCrouching", isCrouching);
	}

	public void PlayerFalling(bool isFalling)
	{
		m_Animator.SetBool("IsFalling", isFalling);
	}

	public void PlayerSpeed(float playerSpeed)
	{
		m_Animator.SetFloat("Speed", Mathf.Abs(playerSpeed));
	}
}
