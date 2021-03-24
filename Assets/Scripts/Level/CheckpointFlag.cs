using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class CheckpointFlag
 * 
 * \brief This class is used for the Checkpoint Flags in the game. Colliding with one will 
 *		   reset the players active checkpoint.
 * 
 * \date 2019/16/12
 */ 
[RequireComponent(typeof(Collider2D))]
public class CheckpointFlag : MonoBehaviour
{
	[SerializeField]
	private int m_ID;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponentInParent<PlayerController>())
		{
			if (!collision.GetComponentInParent<PlayerController>().IsPriorityCollider(collision))
			{
				return;
			}
		}
		else
		{
			return;
		}

		collision.GetComponentInParent<PlayerCharacter>().UpdateCheckpoint(transform, m_ID);
	}

	// Get the Transform of This Checkpoint.
	// @return This Objects Transform.
	public Transform GetCheckpoint()
	{
		return transform;
	}
}
