using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponentInParent<PlayerController>())
		{
			if (!collision.GetComponentInParent<PlayerController>().IsPriorityCollider(collision))
				return;
		}
		else
			return;

		collision.GetComponentInParent<PlayerCharacter>().UpdateCheckpoint(transform);
	}

	public Transform GetCheckpoint()
	{
		return transform;
	}
}
