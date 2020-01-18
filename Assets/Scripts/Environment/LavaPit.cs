using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LavaPit : MonoBehaviour
{
	[SerializeField]
	private AudioSource m_AudioSource;

    public void LavaCausesDamage()
	{
		if(m_AudioSource.clip != null && !m_AudioSource.isPlaying)
			m_AudioSource.Play();
	}
}
