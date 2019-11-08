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

	private bool m_IsLocked = true;

	public bool Unlock()
	{
		if(m_IsLocked)
		{
			m_LockAnimator.SetTrigger("Unlock");
			m_IsLocked = false;
			StartCoroutine(DisableLock());
			return true;
		}
		else
		{
			Debug.Log("Lock already unlocked");
			return false;
		}
	}

	IEnumerator DisableLock()
	{
		yield return new WaitForSeconds(1.15f);
		gameObject.SetActive(false);
	}

	public bool IsLocked()
	{
		return m_IsLocked;
	}
}
