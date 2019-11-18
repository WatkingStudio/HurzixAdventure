using System.Collections;
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
	[SerializeField]
	private CharacterMovement2D m_Movement;
	[SerializeField] 
	private LayerMask m_WhatIsGround;
	[Tooltip("This transform is used to check if the player can stand or if the ceiling is too low.")] 
	public Transform m_CeilingCheck;
	[Tooltip("This transform is used to check if the player can crouch or if a wall is too close.")]
	public Transform m_WallCheck;
	[SerializeField]
	private float m_RunSpeed = 40f;

	private float m_HorizontalMove = 0f;
	private bool m_Jump = false;
	private bool m_Crouch = false;
	private bool m_Sprint = false;
	private bool m_DisableMovement = false;

	[Header("Colliders")]
	[SerializeField, Tooltip("The box collider for the player while standing")]
	private BoxCollider2D m_StandingBoxCollider;
	[SerializeField, Tooltip("The box collider for the player while crouching")]
	private BoxCollider2D m_CrouchingBoxCollider;
	[SerializeField, Tooltip("The circle collider for the player while standing")]
	private CircleCollider2D m_StandingCircleCollider;
	[SerializeField, Tooltip("The priority collider for this character")]
	private Collider2D m_PriorityCollider;

	[Header("Misc")]
	[SerializeField]
	private PlayerCharacter m_PlayerCharacter;
	[SerializeField]
	private Damager m_Damager;

	[Header("Animation")]
	[SerializeField]
	private PlayerAnimations m_Animator;
	[SerializeField]
	private AnimationClip m_AttackClip;

	public Collider2D PriorityCollider { get { return m_PriorityCollider; } }

	private bool m_IsAttacking = false;
	private bool m_IsGrounded = true;

	public void DisableMovement()
	{
		m_DisableMovement = true;
		m_Movement.ResetVelocity();
	}

	public void EnableMovement()
	{
		m_DisableMovement = false;
	}

	// Update is called once per frame
	void Update()
	{
		//If Movement has been disabled do not allow player to move.
		if (m_DisableMovement)
		{
			m_HorizontalMove = 0;
			return;
		}

		m_HorizontalMove = Input.GetAxisRaw("Horizontal") * m_RunSpeed;
		
		if(Input.GetButtonDown("Sprint"))
		{
			m_Animator.PlayerSprinting(true);
			m_Sprint = true;
		}
		else if(Input.GetButtonUp("Sprint"))
		{
			m_Animator.PlayerSprinting(false);
			m_Sprint = false;
		}

		if(Input.GetButtonDown("Attack"))
		{
			if(!m_IsAttacking)
			{
				m_Animator.PlayerAttack();
				StartCoroutine(AttackTimer());
			}			
		}

		if(Input.GetButtonDown("Jump"))
		{
			m_Jump = true;
			m_Animator.PlayerJumping(true);
			m_IsGrounded = false;
		}

		m_Animator.PlayerSpeed(m_HorizontalMove);

		if (Input.GetButtonDown("Crouch"))
		{
			//Crouch
			if (m_IsGrounded)
			{
				if (!m_Crouch)
				{
					if (!Physics2D.OverlapCircle(m_WallCheck.position, .2f, m_WhatIsGround))
					{
						m_Animator.PlayerCrouching(true);
						m_Crouch = true;
						SetCollidersCrouch();
					}
				}
				//Stand Up
				else
				{
					if (!Physics2D.OverlapCircle(m_CeilingCheck.position, .2f, m_WhatIsGround))
					{
						m_Animator.PlayerCrouching(false);
						m_Crouch = false;
						SetCollidersStand();
					}
				}
			}
		}

		if(Input.GetButtonDown("Interact"))
		{
			m_PlayerCharacter.InteractWithObject();
		}		
	}

	//This function is used to apply any code to the player when they land
	public void OnLanding()
	{
		m_Animator.PlayerJumping(false);
		m_IsGrounded = true;
	}

	public void StartFalling()
	{
		m_Animator.PlayerFalling(true);
		m_Animator.PlayerJumping(false);
		m_IsGrounded = false;
	}

	public void StopFalling()
	{
		m_Animator.PlayerFalling(false);
		m_IsGrounded = true;
	}

	//Use for Physics
	private void FixedUpdate()
	{
		//Move character
		m_Movement.Move(m_HorizontalMove * Time.fixedDeltaTime, m_Crouch, m_Jump, m_Sprint);
		m_Jump = false;
	}

	IEnumerator AttackTimer()
	{
		m_IsAttacking = true;
		m_Damager.EnableDamage();
		yield return new WaitForSeconds(m_AttackClip.length);
		m_Damager.DisableDamage();
		m_IsAttacking = false;
	}

	private void SetCollidersCrouch()
	{
		m_CrouchingBoxCollider.enabled = true;
		m_PriorityCollider = m_CrouchingBoxCollider;

		m_StandingBoxCollider.enabled = false;
		m_StandingCircleCollider.enabled = false;

		m_PlayerCharacter.SetColliders(m_CrouchingBoxCollider);
	}

	private void SetCollidersStand()
	{
		m_StandingBoxCollider.enabled = true;
		m_StandingCircleCollider.enabled = true;
		m_PriorityCollider = m_StandingBoxCollider;

		m_CrouchingBoxCollider.enabled = false;

		m_PlayerCharacter.SetColliders(m_StandingCircleCollider);
	}


}
