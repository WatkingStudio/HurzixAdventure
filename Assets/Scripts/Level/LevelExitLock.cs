﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class LevelExitLock
 * 
 * \brief This class holds the code for a lock on the level exit
 * 
 * \date 2019/23/10
 * 
 */
[RequireComponent(typeof(Animator))]
public class LevelExitLock : MonoBehaviour
{
	[SerializeField]
	private Animator m_LockAnimator;
	[SerializeField]
	private AnimationClip m_LockOpeningClip;
	[SerializeField]
	private ItemAudio m_ItemAudio;

	private bool m_IsLocked = true;
	private float m_ClipLengthExtraDelay = 0.2f;

	private void Start()
	{
		if (!m_LockAnimator)
		{
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		}
		if (!m_LockOpeningClip)
		{
			Debug.LogError("No Animation Clip has been assigned to " + gameObject.name);
		}
        if (!m_ItemAudio)
        {
            Debug.LogError("No Item Audio has been assigned to " + gameObject.name);
        }
	}

	// Disable This Lock.
	// @return The Current IEnumerator Step.
	IEnumerator DisableLock()
	{
		m_ItemAudio.PlayAudioClip();
		yield return new WaitForSeconds(m_LockOpeningClip.length + m_ClipLengthExtraDelay);
		gameObject.SetActive(false);
	}

	// Check if this Lock is Locked.
	// @return True if the Lock is Locked, False if Not.
	public bool IsLocked()
	{
		return m_IsLocked;
	}

	// Unlocks the Lock.
	// @return True if the Unlock is Successful, False if Not,
	public bool Unlock()
	{
		if (m_IsLocked)
		{
			m_LockAnimator.SetTrigger("Unlock");
			m_IsLocked = false;
			StartCoroutine(DisableLock());
			return true;
		}

		return false;
	}
}
