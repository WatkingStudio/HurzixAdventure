using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

/**
 * \class LevelExit
 * 
 * \brief This class holds the code for the Level Exit.
 * 
 * \date 2019/23/10
 * 
 */
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(LevelTransition))]
public class LevelExit : InteractableObjects
{
	[Serializable]
	public class DoorUnlockedEvent : UnityEvent<LevelExit>
	{ }

	[SerializeField]
	private List<LevelExitLock> m_DoorLocks;
	[Space]
	[SerializeField]
	private Inventory m_Inventory;
	[SerializeField]
	private ItemAudio m_ItemAudio;

	[Header("Animation")]
	[SerializeField]
	private Animator m_Animator;
	[SerializeField]
	private AnimationClip m_ExitDoorOpeningClip;

	[Header("Events")]
	[SerializeField]
	private DoorUnlockedEvent m_DoorUnlockedEvent;

	private float m_ClipLengthExtraDelay = 0.2f;
	private int m_LocksOpened = 0;
	private int m_NumberOfLocks;

	public void Start()
	{
		if (m_DoorLocks.Count == 0)
		{
			Debug.LogError("No Door Locks have been assigned to " + gameObject.name);
		}
		if (!m_Inventory)
		{
			Debug.LogError("No Inventory has been assigned to " + gameObject.name);
		}
		if (!m_ItemAudio)
		{
			Debug.LogError("No Item Audio has been assigned to " + gameObject.name);
		}
		if (!m_Animator)
		{
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		}
		if (!m_ExitDoorOpeningClip)
		{
			Debug.LogError("No Animation Clip has been assigned to " + gameObject.name);
		}

		m_NumberOfLocks = m_DoorLocks.Count;
	}

	// Execute This Code When the Level Exit is Interacted With.
	public override void Interact()
	{
		if(m_Inventory.HasItem(Item.ItemType.Key))
		{
			if(m_Inventory.NumberOfItem(Item.ItemType.Key) == m_NumberOfLocks)
			{
				foreach(LevelExitLock exitLock in m_DoorLocks)
				{
					if(exitLock.Unlock() && !exitLock.IsLocked())
					{
						m_LocksOpened++;
						m_Inventory.DropItem(Item.ItemType.Key);
					}
				}
				UnlockDoor();
				
			}
			else
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
		}
		
		if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor"))
		{
			Debug.Log("Use Exit Door");
		}
	}

	// Unlock the Door.
	public void UnlockDoor()
	{
		m_Animator.SetTrigger("Unlock");
		m_ItemAudio.PlayAudioClip();
		StartCoroutine(WaitForDoorToOpen());
	}

	// Wait for the Animations to Play Before Opening the Door.
	// @return The Current IEnumerator Step.
	IEnumerator WaitForDoorToOpen()
	{
		yield return new WaitForSeconds(m_ExitDoorOpeningClip.length + m_ClipLengthExtraDelay);
		m_DoorUnlockedEvent.Invoke(this);
	}
}
