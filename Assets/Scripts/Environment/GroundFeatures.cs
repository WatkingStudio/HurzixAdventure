using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class GroundFeatures
 * 
 * \brief This class is used to define any features that the ground has which affects the game world.
 * 
 * \date 2020/20/01
 */ 
public class GroundFeatures : MonoBehaviour
{
	public enum Surface
	{
		DIRT,
		ROCK,
		WOOD,
		SNOW
	}

	[SerializeField]
	private Surface m_GroundSurface;

	// Execute When A Collider Collides With This Object.
	private void OnCollisionEnter2D(Collision2D collision)
	{
		CharacterMovement2D charMove2D = null;
		charMove2D = collision.gameObject.GetComponentInParent<CharacterMovement2D>();

		if (charMove2D != null)
		{
			charMove2D.SetSurface(m_GroundSurface);
		}
	}
	
}
