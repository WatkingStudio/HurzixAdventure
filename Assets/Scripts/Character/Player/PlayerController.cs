﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * \class PlayerController
 * 
 * \brief This class is used to take the input of the player and apply it to the character
 * 
 * \date 2019/15/10
 * 
 */
public class PlayerController : MonoBehaviour
{
	[Header("Movement")]
	[Tooltip("This transform is used to check if the player can stand or if the ceiling is too low.")]
	public Transform m_CeilingCheck;
	[SerializeField]
	private CharacterMovement2D m_Movement;
	[SerializeField]
	private float m_RunSpeed = 40f;
	[Tooltip("This transform is used to check if the player can crouch or if a wall is too close.")]
	public Transform m_WallCheck;
	[SerializeField] 
	private LayerMask m_WhatIsGround;

	[Header("Colliders")]
	[SerializeField, Tooltip("The box collider for the player while crouching")]
	private BoxCollider2D m_CrouchingBoxCollider;
	[SerializeField, Tooltip("The priority colliders for this character")]
	private List<Collider2D> m_PriorityCollider;
	[SerializeField, Tooltip("The circle collider for the player while standing")]
	private CircleCollider2D m_StandingCircleCollider;
	[SerializeField, Tooltip("The circle collider for the player while standing")]
	private CircleCollider2D m_StandingCircleCollider2;
	[SerializeField, Tooltip("The box collider for the player while standing")]
	private BoxCollider2D m_StandingBoxCollider;
	[SerializeField, Tooltip("The second box collider for the player while standing")]
	private BoxCollider2D m_StandingBoxCollider2;

	[Header("Misc")]
	[SerializeField]
	private Damager m_CrouchingDamager;
	[SerializeField]
	private GameObject m_GameMenu;
	[SerializeField]
	private GameObject m_OptionsMenu;
	[SerializeField]
	private PlayerCharacter m_PlayerCharacter;
	[SerializeField]
	private Damager m_StandingDamager;

	[Header("Animation")]
	[SerializeField]
	private PlayerAnimations m_Animator;
	[SerializeField]
	private AnimationClip m_AttackClip;

	private Damager m_ActiveDamager;
	private bool m_Crouch = false;
	private bool m_DisableMovement = false;
	private float m_HorizontalMove = 0f;
	private bool m_IsAttacking = false;
	private bool m_IsGrounded = true;
	private bool m_Jump = false;
	//If equal to 0 crouch, if equal to 1 stand up, if equal to 2 ignore
	private int m_MakeCrouched = 2;
	private bool m_Sprint = false;

	private void Start()
	{
        if (!m_Movement)
        {
            Debug.LogError("No Character Movement has been assigned to " + gameObject.name);
        }
		if (!m_CeilingCheck)
		{
			Debug.LogError("No Transform has been assigned to " + gameObject.name + " to check for ceiling collisions");
		}
		if (!m_WallCheck)
		{
			Debug.LogError("No Transform has been assigned to " + gameObject.name + " to check if infront of a wall");
		}
		if (!m_StandingBoxCollider)
		{
			Debug.LogError("No Collider has been assigned to " + gameObject.name + " for the standing box collider");
		}
		if (!m_StandingBoxCollider2)
		{
			Debug.LogError("No secondary Collider has been assigned to " + gameObject.name + " for the standing box collider 2");
		}
		if (!m_CrouchingBoxCollider)
		{
			Debug.LogWarning("No Collider has been assigned to " + gameObject.name + " for the crouching box collider");
		}
		if (!m_StandingCircleCollider)
		{
			Debug.LogError("No Collider has been assigned to " + gameObject.name + " for the standing circle collider");
		}
		if (!m_StandingCircleCollider2)
		{
			Debug.LogError("No Collider has been assigned to " + gameObject.name + " for the standing circle collider");
		}
		if (m_PriorityCollider.Count <= 0)
		{
			Debug.LogError("Number of priority colliders for " + gameObject.name + " is set to 0");
		}
		if (!m_PlayerCharacter)
		{
			Debug.LogError("No Player Character has been assigned to " + gameObject.name);
		}
		if (!m_StandingDamager)
		{
			Debug.LogError("No Standing Damager has been assigned to " + gameObject.name);
		}
		if (!m_CrouchingDamager)
		{
			Debug.LogError("No Crouching Damager has been assigned to " + gameObject.name);
		}
		if (!m_Animator)
		{
			Debug.LogError("No PlayerAnimations script has been assigned to " + gameObject.name);
		}
		if (!m_AttackClip)
		{
			Debug.LogWarning("No Attack Animation Clip has been assigned to " + gameObject.name);
		}

		m_ActiveDamager = m_StandingDamager;
	}

	void Update()
	{
		//If Movement has been disabled do not allow player to move.
		if (m_DisableMovement)
		{
			m_HorizontalMove = 0;
			return;
		}

		m_HorizontalMove = Input.GetAxisRaw("Horizontal") * m_RunSpeed;

		if (Input.GetButtonDown("Sprint"))
		{
			m_Animator.PlayerSprinting(true);
			m_Sprint = true;
		}
		else if (Input.GetButtonUp("Sprint"))
		{
			m_Animator.PlayerSprinting(false);
			m_Sprint = false;
		}

		if (Input.GetButtonDown("Attack"))
		{
			if (!m_IsAttacking && m_IsGrounded)
			{
				m_Animator.PlayerAttack();
				StartCoroutine(AttackTimer());
			}
		}

		if (Input.GetButtonDown("Jump"))
		{
			bool jump = true;
			if (m_Crouch)
			{
				if (Physics2D.OverlapCircle(m_CeilingCheck.position, .2f, m_WhatIsGround))
				{
					jump = false;
				}
			}

			if (jump)
			{
				m_Jump = true;
				m_Animator.PlayerJumping(true);
				m_IsGrounded = false;
			}
		}

		m_Animator.PlayerSpeed(m_HorizontalMove);

		if (Input.GetButtonDown("Crouch") && m_IsGrounded)
		{
			m_MakeCrouched = 0;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			m_MakeCrouched = 1;
		}

		if (Input.GetButtonDown("Interact"))
		{
			m_PlayerCharacter.InteractWithObject();
		}

		if (Input.GetButtonDown("Menu"))
		{
			if (m_GameMenu.activeSelf)
			{
				m_GameMenu.SetActive(false);
			}
			else if (!m_GameMenu.activeSelf && !m_OptionsMenu.activeSelf)
			{
				m_GameMenu.SetActive(true);
			}
		}
	}

	private void FixedUpdate()
	{
		if (m_IsGrounded)
		{
			if (m_MakeCrouched == 0 && !m_Crouch)
			{
				if (!Physics2D.OverlapCircle(m_WallCheck.position, .2f, m_WhatIsGround))
				{
					m_Animator.PlayerCrouching(true);
					m_Crouch = true;
					SetCollidersCrouch();
				}

				m_MakeCrouched = 2;
			}
			else if (m_MakeCrouched == 1 && m_Crouch)
			{
				if (!Physics2D.OverlapCircle(m_CeilingCheck.position, .2f, m_WhatIsGround))
				{
					m_Animator.PlayerCrouching(false);
					m_Crouch = false;
					SetCollidersStand();
					m_MakeCrouched = 2;
				}
			}
		}

		//Move character
		m_Movement.Move(m_HorizontalMove * Time.fixedDeltaTime, m_Crouch, m_Jump, m_Sprint);
		m_Jump = false;
	}

	/// <summary>
	/// Performs an attack.
	/// </summary>
	/// <returns>The current ienumerator step.</returns>
	IEnumerator AttackTimer()
	{
		m_IsAttacking = true;
		m_ActiveDamager.EnableDamage();
		yield return new WaitForSeconds(m_AttackClip.length);
		m_ActiveDamager.DisableDamage();
		m_IsAttacking = false;
	}

	/// <summary>
	/// Disable the players movement.
	/// </summary>
	public void DisableMovement()
	{
		m_DisableMovement = true;
		m_Movement.ResetVelocity();
	}

	/// <summary>
	/// Enable the players movement.
	/// </summary>
	public void EnableMovement()
	{
		m_DisableMovement = false;
	}

	/// <summary>
	/// Check if the passed collider is a priority collider.
	/// </summary>
	/// <param name="collider">The collider to check.</param>
	/// <returns>True if the collider is a priority collider, false if not.</returns>
	public bool IsPriorityCollider(Collider2D collider)
	{
		foreach (var col in m_PriorityCollider)
		{
			if (col == collider)
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Start a jump.
	/// </summary>
	public void JumpStart()
	{
		m_IsGrounded = false;
	}

	/// <summary>
	/// Perform this code when the player lands.
	/// </summary>
	public void OnLanding()
	{
		m_Animator.PlayerJumping(false);
		m_IsGrounded = true;
	}

	/// <summary>
	/// Set the players colliders when they crouch.
	/// </summary>
	private void SetCollidersCrouch()
	{
		m_CrouchingBoxCollider.enabled = true;
		m_PriorityCollider.Clear();
		m_PriorityCollider.Add(m_CrouchingBoxCollider);

		m_StandingBoxCollider.enabled = false;
		m_StandingBoxCollider2.enabled = false;
		m_StandingCircleCollider.enabled = false;
		m_StandingCircleCollider2.enabled = false;

		m_PlayerCharacter.SetColliders(m_CrouchingBoxCollider);

		m_ActiveDamager = m_CrouchingDamager;
	}

	/// <summary>
	/// Set the players colliders when they stand.
	/// </summary>
	private void SetCollidersStand()
	{
		m_StandingBoxCollider.enabled = true;
		m_StandingBoxCollider2.enabled = true;
		m_StandingCircleCollider.enabled = true;
		m_StandingCircleCollider2.enabled = true;
		m_PriorityCollider.Clear();
		m_PriorityCollider.Add(m_StandingCircleCollider);
		m_PriorityCollider.Add(m_StandingCircleCollider2);

		m_CrouchingBoxCollider.enabled = false;

		m_PlayerCharacter.SetColliders(m_StandingCircleCollider);

		m_ActiveDamager = m_StandingDamager;
	}

	/// <summary>
	/// The player has started falling.
	/// </summary>
	public void StartFalling()
	{
		m_Animator.PlayerFalling(true);
		m_Animator.PlayerJumping(false);
		m_IsGrounded = false;
	}

	/// <summary>
	/// The player has stopped falling.
	/// </summary>
	public void StopFalling()
	{
		m_Animator.PlayerFalling(false);
		m_IsGrounded = true;
	}
}
