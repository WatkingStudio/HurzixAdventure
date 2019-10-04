using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
	[SerializeField]
	private Transform m_PointA;
	[SerializeField]
	private Transform m_PointB;

	Vector3 m_WalkAmount;
	float walkingDirection = 1.0f;
	public float wallRight = 5.0f;
	public float walkSpeed = 2.0f;
	public float wallLeft = 0.0f;
	const float k_SpriteFlipOffset = .5f;
	private bool m_FacingRight = true;

	[SerializeField]
	private Animator m_Animator;

	// Update is called once per frame
	void Update()
    {
		m_WalkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
		if (transform.position.x > m_PointB.position.x)
		{
			walkingDirection = -1.0f;
			Flip();
		}
		else if (transform.position.x < m_PointA.position.x)
		{
			walkingDirection = 1.0f;
			Flip();
		}
			
		transform.Translate(m_WalkAmount);
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
