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

    // Start is called before the first frame update
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

	// Tell the Animator to Perform an Attack Animation.
	public void PlayerAttack()
	{
		m_Animator.SetTrigger("Attack");
	}

	// Tell the Animator to Crouch/Uncrouch.
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

	// Tell the Animator to Perform the Dead Animation.
	public void PlayerDead(bool isDead)
	{
		m_Animator.SetBool("IsDead", isDead);
	}

	// Tell the Animator to Perform the Falling Animation.
	public void PlayerFalling(bool isFalling)
	{
		m_Animator.SetBool("IsFalling", isFalling);
	}

	// Tell the Animator to Perform the Hurt Animation.
	public void PlayerHurt()
	{
		m_Animator.SetTrigger("Hurt");
	}

	// Tell the Animator to Perform the Idle Animation.
	public void PlayerIdle()
	{
		m_Animator.SetTrigger("Idle");
	}

	// Tell the Animator if the Player is Jumping.
	public void PlayerJumping(bool isJumping)
	{
		m_Animator.SetBool("IsJumping", isJumping);
	}

	// Tells the Animator the Players Speed.
	public void PlayerSpeed(float playerSpeed)
	{
		m_Animator.SetFloat("Speed", Mathf.Abs(playerSpeed));
	}

	// Tells the Animator if the Player is Sprinting.
	public void PlayerSprinting(bool isSprinting)
	{
		m_Animator.SetBool("IsSprinting", isSprinting);
	}
}
