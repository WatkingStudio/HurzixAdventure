using System.Collections;
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

	/// <summary>
	/// Disable this lock.
	/// </summary>
	/// <returns>The current ienumerator step.</returns>
	IEnumerator DisableLock()
	{
		m_ItemAudio.PlayAudioClip();
		yield return new WaitForSeconds(m_LockOpeningClip.length + m_ClipLengthExtraDelay);
		gameObject.SetActive(false);
	}

	/// <summary>
	/// Check if this lock is locked.
	/// </summary>
	/// <returns>True if the lock is locked, false if not.</returns>
	public bool IsLocked()
	{
		return m_IsLocked;
	}

	/// <summary>
	/// Unlocks the lock.
	/// </summary>
	/// <returns>True if the unlock is successful, false if not.</returns>
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
