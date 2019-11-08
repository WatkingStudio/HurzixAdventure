using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class LevelExit
 * 
 * \brief This class holds the code for the Level Exit.
 * 
 * \date 2019/23/10
 * 
 */
[RequireComponent(typeof(Animator))]
public class LevelExit : InteractableObjects
{
	[SerializeField]
	private List<LevelExitLock> m_DoorLocks;
	[SerializeField]
	private Inventory m_Inventory;
	[SerializeField]
	private Animator m_Animator;
	[SerializeField]
	private ItemAudio m_ItemAudio;

	private int m_NumberOfLocks;
	private int m_LocksOpened = 0;

	public void Start()
	{
		m_NumberOfLocks = m_DoorLocks.Count;
	}

	public override void Interact()
	{
		if(m_Inventory.HasItem(Item.ItemType.Key))
		{
			foreach (LevelExitLock exitLock in m_DoorLocks)
			{
				if (exitLock.Unlock() && !exitLock.IsLocked())
				{
					m_LocksOpened++;
					m_Inventory.DropItem(Item.ItemType.Key);
					break;
				}
			}

			if (m_LocksOpened == m_NumberOfLocks)
			{
				UnlockDoor();
			}
		}
		
		if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor"))
		{
			Debug.Log("Use Exit Door");
		}
	}

	public void UnlockDoor()
	{
		m_Animator.SetTrigger("Unlock");
		m_ItemAudio.PlayAudioClip();
	}
}
