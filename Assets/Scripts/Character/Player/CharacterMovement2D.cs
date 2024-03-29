using UnityEngine;
using UnityEngine.Events;

/** 
 * \class CharacterMovement2D
 * 
 * \brief This class is used to control a 2D Characters Movement
 * 
 * This class applies, controls and reacts to the movement of the character
 * 
 * \date 2019/13/10
 * 
 */
public class CharacterMovement2D : MonoBehaviour
{
	[Header("Movement Variables")]
	[SerializeField, Tooltip("Whether or not a player can steer while jumping")]
	private bool m_AirControl = false;
	[SerializeField, Tooltip("Amount of maxSpeed applied to crouching movement. 1 = 100%"), Range(0, 1)]
	private float m_CrouchSpeed = .36f;
	[SerializeField, Tooltip("Amount of force added when the player jumps")]
	private float m_JumpForce = 400f;
	[SerializeField, Tooltip("How much to smooth out the movement"), Range(0, .3f)]
	private float m_MovementSmoothing = .05f;
	[SerializeField, Tooltip("The multiplier applied to the speed of the player when sprinting")]
	private float m_SprintSpeed = 1.5f;

	[Header("Collider Variables")]
	[SerializeField]
	private Collider2D m_CollisionCheckerCollider;
	[SerializeField, Tooltip("A collider that will be disabled when crouching")]
	private Collider2D m_CrouchDisableCollider;
	[SerializeField, Tooltip("A position marking where to check if the player is grounded")]
	private Transform m_GroundCheck;
	[SerializeField, Tooltip("A mask determining what is ground to the character")]
	private LayerMask m_WhatIsGround;

	[Header("Audio")]
	[SerializeField]
	private PlayerAudio m_PlayerAudio;

	[Header("Events")]
	[Space]

	public UnityEvent OnJump;
	public UnityEvent OnLandEvent;
	public UnityEvent OnStartFalling;
	public UnityEvent OnStopFalling;

	public BoolEvent OnCrouchEvent;

	// Radius of the overlap circle to determine if the player can stand up.
	const float m_CeilingRadius = .2f; 
	const float m_CrouchSpriteFlipOffset = -.5f;
	// For determining which way the player is currently facing.
	private bool m_FacingRight = true;  
	// Whether or not the player is grounded.
	private bool m_Grounded;            
	// Radius of the overlap circle to determine if grounded.
	const float m_GroundedRadius = .05f; 
	private Rigidbody2D m_Rigidbody2D;
	const float m_SpriteFlipOffset = .5f;
	private Vector3 m_Velocity = Vector3.zero;
	private bool m_wasCrouching = false;

	// Surface Info.
	private GroundFeatures.Surface m_CurrentSurface;
	private float m_OnIceMovement = 0.0f;
	private float m_SurfaceFriction;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Start()
	{
		if (!m_GroundCheck)
		{
			Debug.LogError("No transform has been assigned to " + gameObject.name + " to check if the player is grounded");
		}

		if (!m_CrouchDisableCollider)
		{
			Debug.LogWarning("No collider has been assigned to " + gameObject.name + " to be disabled if the player crouches");
		}

		if (!m_PlayerAudio)
		{
			Debug.LogError("No Player Audio has been assigned to " + gameObject.name);
		}
	}

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
		{
			OnLandEvent = new UnityEvent();
		}

		if (OnStartFalling == null)
		{
			OnStartFalling = new UnityEvent();
		}

		if (OnStopFalling == null)
		{
			OnStopFalling = new UnityEvent();
		}

		if (OnJump == null)
		{
			OnJump = new UnityEvent();
		}

		if (OnCrouchEvent == null)
		{
			OnCrouchEvent = new BoolEvent();
		}
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
				{
					OnLandEvent.Invoke();
				}
			}
		}

		if (m_Rigidbody2D.velocity.y < -0.1 && !m_Grounded)
		{
			OnStartFalling.Invoke();
		}
		else
		{
			OnStopFalling.Invoke();
		}
	}

	/// <summary>
	/// Check for a collision between the character and the ground.
	/// </summary>
	/// <returns>True if there is a collision with the ground, false if not.</returns>
	private bool CheckForGroundCollision()
	{
		if (m_CollisionCheckerCollider.IsTouchingLayers(m_WhatIsGround))
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// Flip the character
	/// </summary>
	/// <param name="xOffset">The offset of the character sprite.</param>
	private void Flip(float xOffset)
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
			pos.x -= xOffset;
		}
		else
		{
			pos.x += xOffset;
		}
		transform.localPosition = pos;
	}

	/// <summary>
	/// Perform a move on the character.
	/// </summary>
	/// <param name="move">The horizonal movement of the character.</param>
	/// <param name="crouch">Is the character crouching.</param>
	/// <param name="jump">Is the character jumping.</param>
	/// <param name="sprint">Is the character sprinting.</param>
	public void Move(float move, bool crouch, bool jump, bool sprint)
	{
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			m_PlayerAudio.PlayJumpingAudioClip();
			OnJump.Invoke();
		}
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			if (m_SurfaceFriction != 1)
			{
				move *= m_SurfaceFriction;
			}
			if (sprint && m_Grounded)
			{
				move *= m_SprintSpeed;
			}
			
			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
				{
					m_CrouchDisableCollider.enabled = false;
				}
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
				{
					m_CrouchDisableCollider.enabled = true;
				}

				// If sprinting increase the speed
				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			if (m_CurrentSurface == GroundFeatures.Surface.SNOW)
			{
				if (move == 0)
				{
					move = Mathf.Lerp(m_OnIceMovement, m_OnIceMovement * 0.95f, 0.95f);
				}
				m_OnIceMovement = move;
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity;
			targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

			// And then smoothing it out and applying it to the character
			if (!CheckForGroundCollision())
			{
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}
				
			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				if (!crouch)
				{
					Flip(m_SpriteFlipOffset);
				}
				else
				{
					Flip(m_CrouchSpriteFlipOffset);
				}
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				if (!crouch)
				{
					Flip(m_SpriteFlipOffset);
				}
				else
				{
					Flip(m_CrouchSpriteFlipOffset);
				}
			}

			if (move >= 0.1 || move <= -0.1)
			{
				if (m_Grounded)
				{

					if (sprint)
					{
						m_PlayerAudio.PlaySprintAudioClip();
					}
					else
					{
						m_PlayerAudio.PlayWalkAudioClip();
					}
				}
			}
		}		
	}

	/// <summary>
	/// Reset the velocity of the character.
	/// </summary>
	public void ResetVelocity()
	{
		m_Rigidbody2D.velocity = Vector2.one;
	}

	/// <summary>
	/// Set the current surface the character is walking on.
	/// </summary>
	/// <param name="newSurface">The surface now being walked on.</param>
	public void SetSurface(GroundFeatures.Surface newSurface)
	{
		m_CurrentSurface = newSurface;

		switch (m_CurrentSurface)
		{
			case GroundFeatures.Surface.DIRT:
				m_SurfaceFriction = 1;
				m_PlayerAudio.SetDirtMovement();
				break;
			case GroundFeatures.Surface.SNOW:
				m_SurfaceFriction = 1.2f;
				m_OnIceMovement = 0;
				m_PlayerAudio.SetSnowMovement();
				break;
			case GroundFeatures.Surface.ROCK:
				m_SurfaceFriction = 1;
				m_PlayerAudio.SetConcreteMovement();
				break;
			case GroundFeatures.Surface.WOOD:
				m_SurfaceFriction = 1;
				break;
		}
	}
}
