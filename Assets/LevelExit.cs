using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : InteractableObjects
{
	[SerializeField]
	private List<GameObject> m_DoorLocks;

	private int m_NumberOfLocks;
	private int m_LocksOpened = 0;

	public void Start()
	{
		m_NumberOfLocks = m_DoorLocks.Count;
	}

	public override void Interact()
	{
		Debug.Log("Interact with Level Exit");

		if(m_LocksOpened == m_NumberOfLocks)
		{
			UnlockDoor();
		}
	}

	public void UnlockDoor()
	{
		Debug.Log("Unlock Door");
	}
}
