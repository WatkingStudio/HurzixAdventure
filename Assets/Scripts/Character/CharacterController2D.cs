using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField, Tooltip("Amount of force added when the player jumps")]
	private float m_JumpForce = 400f;
	[Range(0, 1)] [SerializeField, Tooltip("Amount of maxSpeed applied to crouching movement. 1 = 100%")]
	private float m_CrouchSpeed = .36f;
	[Range(0, .3f)] [SerializeField, Tooltip("How much to smooth out the movement")]
	private float m_MovementSmoothing = .05f;
	[SerializeField, Tooltip("The multiplier applied to the speed of the player when sprinting")] 
	private float m_SprintSpeed = 1.5f;
	[SerializeField, Tooltip("Whether or not a player can steer while jumping")] 
	private bool m_AirControl = false;
	[SerializeField, Tooltip("A mask determining what is ground to the character")]
	private LayerMask m_WhatIsGround;
	[SerializeField, Tooltip("A position marking where to check if the player is grounded")]
	private Transform m_GroundCheck;
	[SerializeField, Tooltip("A collider that will be disabled when crouching")]
	private Collider2D m_CrouchDisableCollider;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	const float k_SpriteFlipOffset = .5f;
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;
	public UnityEvent OnStartFalling;
	public UnityEvent OnStopFalling;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnStartFalling == null)
			OnStartFalling = new UnityEvent();

		if (OnStopFalling == null)
			OnStopFalling = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

		if (m_Rigidbody2D.velocity.y < -0.1)
		{
			OnStartFalling.Invoke();
		}
		else
			OnStopFalling.Invoke();
	}


	public void Move(float move, bool crouch, bool jump, bool sprint)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			if (sprint && m_Grounded)
				move *= m_SprintSpeed;
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
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				// If sprinting increase the speed
				

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);			

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	private void Flip()
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
}
