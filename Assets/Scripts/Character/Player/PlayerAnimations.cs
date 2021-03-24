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
	[SerializeField]
	private Animator m_Animator;
	[SerializeField, Tooltip("The animator for the player while crouching")]
	private RuntimeAnimatorController m_CrouchingController;
	[SerializeField, Tooltip("The animator for the player while standing")]
	private RuntimeAnimatorController m_StandingController;

    void Start()
    {
		if (!m_StandingController)
		{
			Debug.LogError("No Animator Controller has been assigned to " + gameObject.name + " while standing");
		}

		if (!m_CrouchingController)
		{
			Debug.LogWarning("No Animator Controller has been assigned to " + gameObject.name + "while crouching");
		}

		if (!m_Animator)
		{
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		}

		m_Animator.runtimeAnimatorController = m_StandingController;
	}

	/// <summary>
	/// Tell the animator to perform an attack animation.
	/// </summary>
	public void PlayerAttack()
	{
		m_Animator.SetTrigger("Attack");
	}

	/// <summary>
	/// Tell the animator to crouch/uncrouch.
	/// </summary>
	/// <param name="isCrouching">Is the player crouching.</param>
	public void PlayerCrouching(bool isCrouching)
	{
		//When the player crouches/uncrouches, both of the animators need to know this. So before
		// changing controller the bool is set to true/false, and after the controller is set
		// the bool in the other controller is also set to true/false.

		m_Animator.SetBool("IsCrouching", isCrouching);

		if (isCrouching)
		{
			m_Animator.runtimeAnimatorController = m_CrouchingController;
		}
		else
		{
			m_Animator.runtimeAnimatorController = m_StandingController;
		}

		m_Animator.SetBool("IsCrouching", isCrouching);
	}

	/// <summary>
	/// Tell the animator to perform the dead animation.
	/// </summary>
	/// <param name="isDead">Is the player dead.</param>
	public void PlayerDead(bool isDead)
	{
		m_Animator.SetBool("IsDead", isDead);
	}

	/// <summary>
	/// Tell the animator to perform the falling animation
	/// </summary>
	/// <param name="isFalling">Is the player falling.</param>
	public void PlayerFalling(bool isFalling)
	{
		m_Animator.SetBool("IsFalling", isFalling);
	}

	/// <summary>
	/// Tell the animator to perform the hurt animation.
	/// </summary>
	public void PlayerHurt()
	{
		m_Animator.SetTrigger("Hurt");
	}

	/// <summary>
	/// Tell the animator to perform the idle animation.
	/// </summary>
	public void PlayerIdle()
	{
		m_Animator.SetTrigger("Idle");
	}

	/// <summary>
	/// Tell the animator if the player is jumping.
	/// </summary>
	/// <param name="isJumping">Is the player jumping.</param>
	public void PlayerJumping(bool isJumping)
	{
		m_Animator.SetBool("IsJumping", isJumping);
	}

	/// <summary>
	/// Tells the animator the players speed.
	/// </summary>
	/// <param name="playerSpeed">The speed of the player.</param>
	public void PlayerSpeed(float playerSpeed)
	{
		m_Animator.SetFloat("Speed", Mathf.Abs(playerSpeed));
	}

	/// <summary>
	/// Tells the animator if the player is sprinting.
	/// </summary>
	/// <param name="isSprinting">Is the player sprinting.</param>
	public void PlayerSprinting(bool isSprinting)
	{
		m_Animator.SetBool("IsSprinting", isSprinting);
	}
}
